using System;
using System.Collections.Generic;
using System.Linq;

namespace Connect6_MTCT
{
    class Program
    {
        static void Main(string[] args)
        {
            int height = 19;
            int width = 19;
            int n_in_row = 6;
            int n_playout = 2000;
            //TODO : Apply reinforcelearning 
            //bool use_gpu = false;
            //string model_file = "model/10_10_6_best_policy_3.model";
            bool ai_first = true;

            Board board = new Board(width, height, n_in_row);
            Game game = new Game(board);

            // human VS AI
            MCTSPlayer mcts_player = new MCTSPlayer(PolicyValueFn, cPuct: 5, nPlayout: n_playout);
            Human human = new Human();

            game.StartPlay(human, mcts_player, startPlayer: ai_first ? 1 : 0, isShown: true);

        }

        // Nhận đối tượng bàn cờ board và trả về một Tuple chứa danh sách các Tuple<int, double> đại diện cho 
        // các hành động và xác suất tương ứng, cùng với một giá trị kiểu double.
        // Tính toán xác suất đồng đều cho các hành động có thể thực hiện trên bàn cờ.
        // Tạo danh sách các Tuple<int, double> bằng cách ghép cặp các hành động và xác suất tương ứng.
        // Trả về danh sách các Tuple<int, double> và một giá trị 0.0.
        public static Tuple<List<Tuple<int, double>>, double> PolicyValueFn(Board board)
        {
            // Tính toán xác suất thống nhất cho các hành động có sẵn
            double[] actionProbs = Enumerable.Repeat(1.0 / board.Availables.Count, board.Availables.Count).ToArray();

            // Tạo danh sách các bộ (hành động, xác suất)
            List<Tuple<int, double>> actionProbsList = board.Availables.Zip(actionProbs, (action, prob) => Tuple.Create(action, prob)).ToList();

            // Trả về danh sách các bộ (hành động, xác suất) và điểm bằng 0
            return Tuple.Create(actionProbsList, 0.0);
        }
    }
}