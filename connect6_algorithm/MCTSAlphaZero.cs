namespace Connect6_MTCT
{
    public class MCTSAlphaZero
    {
        private TreeNode root;
        private Func<Board, Tuple<List<Tuple<int, double>>, double>> policy;
        private double cPuct;
        private int nPlayout;

        public MCTSAlphaZero(Func<Board, Tuple<List<Tuple<int, double>>, double>> policy_value_fn, double c_puct = 5, int n_playout = 10000)
        {
            this.root = new TreeNode(null, 1.0);
            this.policy = policy_value_fn;
            this.cPuct = c_puct;
            this.nPlayout = n_playout;
        }

        private void Playout(Board board)
        {
            TreeNode node = root;
            while (true)
            {
                if (node.IsLeaf())
                {
                    break;
                }

                Tuple<int, TreeNode> selected = node.Select(cPuct);
                int action = selected.Item1;
                node = selected.Item2;
                board.DoMove(action);
            }

            List<Tuple<int, double>> actionProbs;
            double value;
            (actionProbs, value) = policy(board);
            int currentPlayer = board.CurrentPlayer;

            if (board.GameEnd(out int winner))
            {
                if (winner != -1)
                {
                    value = (currentPlayer == winner) ? 1.0 : -1.0;
                }
                else
                {
                    value = 0.0;
                }
            }

            node.Expand(actionProbs);

            if (board.Chesses == 2)
            {
                node.UpdateRecursive(-value, false);
            }
            else
            {
                node.UpdateRecursive(value, true);
            }
        }

        // private double[] Log(int[] arr)
        // {
        //     return arr.Select(x => Math.Log(x)).ToArray();
        // }

        //Tính toán logarithm cơ số tự nhiên của mỗi phần tử trong mảng visits
        private double[] LogScaled(int[] arr, double scale)
        {
            return arr.Select(x => Math.Log(x / scale)).ToArray();
        }

        // Tính tổng của tất cả các giá trị trong mảng p.
        // Tạo một số ngẫu nhiên từ 0 đến tổng p.
        // Lặp qua từng phần tử trong mảng p, tính tổng tích lũy và so sánh với số ngẫu nhiên đã tạo.
        // Nếu tổng tích lũy vượt quá hoặc bằng số ngẫu nhiên, trả về chỉ số tương ứng của phần tử đó.
        private int RandomChoice(double[] p)
        {
            double sum_p = p.Sum();
            double target = new Random().NextDouble() * sum_p;
            double cumulative_p = 0.0;
            for (int i = 0; i < p.Length; i++)
            {
                cumulative_p += p[i];
                if (cumulative_p >= target)
                {
                    return i;
                }
            }
            throw new InvalidOperationException("Invalid random choice");
        }

        // Trả về một nước đi ngẫu nhiên dựa trên xác suất di chuyển được tính toán bằng hàm Softmax
        public Tuple<int, double[]> GetMove(Board board, double temp = 1e-3)
        {
            for (int n = 0; n < nPlayout; n++)
            {
                Board stateCopy = new Board(width: board.Width, height: board.Height, n_in_row: board.NInRow);
                stateCopy.InitBoard();
                stateCopy.States = new Dictionary<int, int>(board.States);
                stateCopy.Availables = new List<int>(board.Availables);
                stateCopy.CurrentPlayer = board.CurrentPlayer;
                stateCopy.LastMove = board.LastMove;

                Playout(stateCopy);
            }

            var act_visits = root.Children.Select(kv => (kv.Key, kv.Value.NVisits)).ToList();
            var acts = act_visits.Select(av => av.Item1).ToArray();
            var visits = act_visits.Select(av => av.Item2).ToArray();
            // Áp dụng hàm Softmax và LogScaled để tính toán các xác suất di chuyển cho từng hành động.
            var actProbs = Softmax(LogScaled(visits, temp));

            //Dùng hàm RandomChoice để chọn một hành động ngẫu nhiên dựa trên xác suất di chuyển.
            var bestMove = acts[RandomChoice(actProbs)];
            var moveProbs = new double[board.Width * board.Height];
            for (int i = 0; i < acts.Length; i++)
            {
                moveProbs[acts[i]] = actProbs[i];
            }

            return Tuple.Create(bestMove, moveProbs);
        }

        public Tuple<int[], double[]> GetMoveProbs(Board board, double temp = 1e-3)
        {
            for (int n = 0; n < nPlayout; n++)
            {
                Board stateCopy = new Board(width: board.Width, height: board.Height, n_in_row: board.NInRow);
                stateCopy.InitBoard();
                stateCopy.States = new Dictionary<int, int>(board.States);
                stateCopy.Availables = new List<int>(board.Availables);
                stateCopy.CurrentPlayer = board.CurrentPlayer;
                stateCopy.LastMove = board.LastMove;

                Playout(stateCopy);
            }

            var act_visits = root.Children.Select(kv => (kv.Key, kv.Value.NVisits)).ToList();
            var acts = act_visits.Select(av => av.Item1).ToArray();
            var visits = act_visits.Select(av => av.Item2).ToArray();
            // Áp dụng hàm Softmax và LogScaled để tính toán các xác suất di chuyển cho từng hành động.
            var actProbs = Softmax(LogScaled(visits, temp));

            return Tuple.Create(acts, actProbs);
        }

        private double[] Softmax(double[] x)
        {
            var max_x = x.Max();
            var exps = x.Select(e => Math.Exp(e - max_x));
            var sum_exps = exps.Sum();
            return exps.Select(e => e / sum_exps).ToArray();
        }

        public void UpdateWithMove(int lastMove)
        {
            if (root.Children.ContainsKey(lastMove))
            {
                root = root.Children[lastMove];
                root.Parent = null;
            }
            else
            {
                root = new TreeNode(null, 1.0);
            }
        }
    }
}