
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torch;
using Torch.nn;
using Torch.optim;
using Torch.nn.functional;
using Torch.autograd;
using Numpy = np;

namespace Connect6_MTCT
{
    class PolicyValueNet
    {
        private bool use_gpu;
        private int board_width;
        private int board_height;
        private double l2_const;
        private Net policy_value_net;
        private Optimizer optimizer;

        public PolicyValueNet(int board_width, int board_height, string model_file = null, bool use_gpu = false)
        {
            this.use_gpu = use_gpu;
            this.board_width = board_width;
            this.board_height = board_height;
            l2_const = 1e-4;

            policy_value_net = use_gpu ? new Net(board_width, board_height).Cuda() : new Net(board_width, board_height);
            optimizer = new Adam(policy_value_net.Parameters(), weight_decay: l2_const);

            if (model_file != null)
            {
                LoadModel(model_file);
            }
        }

        public (Numpy.ndarrays, Numpy.ndarrays) PolicyValue(Numpy.ndarrays state_batch)
        {
            Tensor state_batch_tensor = use_gpu ? Variable.FromNumpy(state_batch).Cuda() : Variable.FromNumpy(state_batch);
            var (log_act_probs, value) = policy_value_net.Forward(state_batch_tensor);
            var act_probs = np.exp(log_act_probs.Data<float>());
            return (act_probs, value.Data<float>());
        }

        public (List<(int, double)>, double) PolicyValueFn(Board board)
        {
            var legal_positions = board.Availables;
            var current_state = Numpy.ascontiguousarray(board.CurrentState().Reshape(-1, 4, board_width, board_height));
            Tensor current_state_tensor = use_gpu ? Variable.FromNumpy(current_state).Cuda().Float() : Variable.FromNumpy(current_state).Float();
            var (log_act_probs, value) = policy_value_net.Forward(current_state_tensor);
            var act_probs = np.exp(log_act_probs.Data<float>().Flatten());
            var act_probs_zip = legal_positions.Zip(act_probs[legal_positions]);
            double value_data = value.Data<float>()[0][0];
            return (act_probs_zip.ToList(), value_data);
        }

        public (double, double) TrainStep(Numpy.ndarrays state_batch, Numpy.ndarrays mcts_probs, Numpy.ndarrays winner_batch, double lr)
        {
            Tensor state_batch_tensor = use_gpu ? Variable.FromNumpy(state_batch).Cuda() : Variable.FromNumpy(state_batch);
            Tensor mcts_probs_tensor = use_gpu ? Variable.FromNumpy(mcts_probs).Cuda() : Variable.FromNumpy(mcts_probs);
            Tensor winner_batch_tensor = use_gpu ? Variable.FromNumpy(winner_batch).Cuda() : Variable.FromNumpy(winner_batch);

            optimizer.ZeroGrad();
            SetLearningRate(optimizer, lr);

            var (log_act_probs, value) = policy_value_net.Forward(state_batch_tensor);

            var value_view = value.View(-1);
            var value_loss = nn.MSELoss().Forward(value_view, winner_batch_tensor);
            var policy_loss = -torch.mean(torch.sum(mcts_probs_tensor * log_act_probs, 1));
            var loss = value_loss + policy_loss;

            loss.Backward();
            optimizer.Step();

            var entropy = -torch.mean(torch.sum(torch.exp(log_act_probs) * log_act_probs, 1));

            return (loss.Item<double>(), entropy.Item<double>());
        }

        public Dictionary<string, Tensor> GetPolicyParam()
        {
            return policy_value_net.StateDict();
        }

        public void SaveModel(string model_file)
        {
            var net_params = GetPolicyParam();
            torch.save(net_params, model_file);
        }

        public void LoadModel(string model_file)
        {
            var net_params = torch.load(model_file);
            policy_value_net.LoadStateDict(net_params);
        }
    }

    // Usage example
    // PolicyValueNet policyValueNet = new PolicyValueNet(board_width: 6, board_height: 6);
    // var (act_probs, value) = policyValueNet.PolicyValue(state_batch);
    // policyValueNet.TrainStep(state_batch, mcts_probs, winner_batch, lr);
    // policyValueNet.SaveModel(model_file);
}