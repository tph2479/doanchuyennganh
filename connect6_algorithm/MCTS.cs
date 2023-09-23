namespace Connect6_MTCT
{
    public class MCTS
    {
        private TreeNode root;
        private Func<Board, Tuple<List<Tuple<int, double>>, double>> policy;
        private double cPuct;
        private int nPlayout;

        public MCTS(Func<Board, Tuple<List<Tuple<int, double>>, double>> policy, double cPuct, int nPlayout)
        {
            root = new TreeNode(null, 1.0);
            this.policy = policy;
            this.cPuct = cPuct;
            this.nPlayout = nPlayout;
        }

        // Vòng lặp for để thực hiện quá trình playout nPlayout lần.

        // Trong mỗi lần playout, tạo một bản sao của board (stateCopy) và 
        // thực hiện playout trên bản sao đó.

        // Sau khi hoàn thành quá trình playout, 
        // chọn nước đi tốt nhất từ các nút con của nút gốc (root) dựa trên số lần thăm viếng (NVisits) của các nút con.

        // Tính toán các xác suất di chuyển (moveProbs) cho từng ô trên bàn cờ bằng phương thức CalculateMoveProbs.

        // Trả về một Tuple gồm nước đi tốt nhất (bestMove) và mảng moveProbs.
        public Tuple<int, double[]> GetMove(Board board)
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

            int bestMove = root.Children.MaxBy(n => n.Value.NVisits).Key;
            double[] moveProbs = CalculateMoveProbs(board);

            return Tuple.Create(bestMove, moveProbs);
        }


        // Tính tổng số lượt thăm viếng(NVisits) của tất cả các nút con của nút gốc.

        // Khởi tạo một mảng moveProbs có độ dài bằng số ô trên bàn cờ.

        // Với mỗi nút con của nút gốc, tính xác suất di chuyển cho ô tương ứng dựa trên số lần 

        // có mặt của nút con và tổng số lượt thăm viếng của tất cả các nút con.

        // Trả về mảng moveProbs chứa các xác suất di chuyển.
        private double[] CalculateMoveProbs(Board board)
        {
            int totalVisits = root.Children.Sum(n => n.Value.NVisits);
            double[] moveProbs = new double[board.Width * board.Height];

            foreach (var child in root.Children)
            {
                int move = child.Key;
                int visits = child.Value.NVisits;
                moveProbs[move] = (double)visits / totalVisits;
            }

            return moveProbs;
        }


        // Khởi tạo một biến node để lưu trữ nút hiện tại và gán cho nó giá trị là nút gốc(root).

        // Trong vòng lặp while, kiểm tra xem nút hiện tại có phải là lá(không có nút con) hay không.
        // Nếu là lá, thoát khỏi vòng lặp.

        // Sử dụng phương thức Select của nút hiện tại để chọn một hành động (action) dựa trên hệ số cPuct.
        // Trả về một Tuple gồm hành động và nút con được chọn.

        // Thực hiện hành động trên bàn cờ (board) bằng phương thức DoMove.

        // Tiếp tục vòng lặp để di chuyển đến nút con được chọn và tiếp tục quá trình.

        // Sau khi thoát khỏi vòng lặp while, thực hiện các bước sau:
        //      Gọi phương thức policy truyền vào board để lấy danh sách xác suất di chuyển cho 
        //      các hành động và giá trị value.
        //      Lưu trữ người chơi hiện tại (currentPlayer) từ board.
        //      Kiểm tra xem trò chơi đã kết thúc hay chưa bằng phương thức GameEnd của board.
        //      Nếu kết thúc, lấy ra người chiến thắng (winner) và cập nhật giá trị value tương ứng.
        //      Mở rộng nút hiện tại (root) với danh sách xác suất di chuyển actionProbs.
        //      Cập nhật cây MCTS bằng phương thức UpdateRecursive của nút gốc (root) dựa trên giá trị value và 
        //      trạng thái của bàn cờ.
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

        // Nhận một nước đi cuối cùng(lastMove) làm tham số đầu vào.

        // Kiểm tra xem nút gốc có chứa nút con tương ứng với nước đi cuối cùng hay không.
        // Nếu có, gán nút con đó cho nút gốc và đặt parent của nút con là null.

        // Nếu không, tạo một nút gốc mới với trọng số là 1.0.
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