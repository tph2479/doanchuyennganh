using System;
using System.Collections.Generic;

namespace connect6
{
    class ComputerPlayer
    {
        private GameManagement game;
        private Board board;
        public ComputerPlayer(GameManagement game, Board board)
        {
            this.game = game;
            this.board = board;
        }

        public void MakeMove()
        {
            if (game.CurrentPlayer == PieceType.WHITE)
            {
                // Nếu là lượt của máy tính (PieceType.WHITE), bạn có thể triển khai thuật toán Minimax ở đây để tìm nước đi tốt nhất.
                // Dưới đây là một ví dụ đơn giản cho mục đích minh họa.
                List<Move> possibleMoves = GetPossibleMoves();

                Move bestMove = null;
                int bestScore = int.MinValue;

                foreach (Move move in possibleMoves)
                {
                    // Thử từng nước đi và đánh giá điểm số cho nó sử dụng Minimax hoặc một thuật toán đánh giá khác.
                    int score = Minimax(move, 4, int.MinValue, int.MaxValue, true);

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = move;
                    }
                }

                // Đánh quân cờ tại vị trí tốt nhất mà máy tính đã tính toán được.
                if (bestMove != null)
                {
                    game.IsPlaceAPiece(bestMove.X, bestMove.Y);
                }
            }
        }

        // Hàm đánh giá nước đi sử dụng thuật toán Minimax
        public  int Minimax(Move move, int depth, int alpha, int beta, bool isMaximizingPlayer)
        {
            // Điểm số cho trường hợp kết thúc trò chơi hoặc đạt độ sâu tối đa đánh giá.
            if (depth == 0 || game.Winner != PieceType.NON)
            {
                return EvaluateBoard(board,PieceType.WHITE); // Hàm đánh giá tình hình bàn cờ.
            }

            List<Move> possibleMoves = GetPossibleMoves();

            if (isMaximizingPlayer)
            {
                int maxEval = int.MinValue;
                foreach (Move possibleMove in possibleMoves)
                {
                    // Đánh giá nước đi và lựa chọn nước đi tốt nhất cho máy tính.
                    int eval = Minimax(possibleMove, depth - 1, alpha, beta, false);
                    maxEval = Math.Max(maxEval, eval);
                    alpha = Math.Max(alpha, eval);
                    if (beta <= alpha)
                        break;
                }
                return maxEval;
            }
            else
            {
                int minEval = int.MaxValue;
                foreach (Move possibleMove in possibleMoves)
                {
                    // Đánh giá nước đi và lựa chọn nước đi tốt nhất cho đối thủ.
                    int eval = Minimax(possibleMove, depth - 1, alpha, beta, true);
                    minEval = Math.Min(minEval, eval);
                    beta = Math.Min(beta, eval);
                    if (beta <= alpha)
                        break;
                }
                return minEval;
            }
        }

        // Hàm đánh giá tình hình bàn cờ (cần triển khai dựa trên quy tắc của trò chơi Connect6).
        public int EvaluateBoard(Board board, PieceType playerType)
        {
            int score = 0;

            // Đánh giá dựa trên số quân cờ của người chơi
            int playerPieces = board.CountPieces(playerType);

            // Đánh giá dựa trên sự kết hợp của quân cờ
            int playerCombos = board.CalculateCombos(board, playerType);

            // Đánh giá dựa trên vị trí của quân cờ trên bàn cờ
            int playerPosition =board.CalculatePosition(board, playerType);

            // Cộng điểm lại với nhau, bạn có thể điều chỉnh trọng số của từng yếu tố tại đây
            score = playerPieces * 100 + playerCombos * 50 + playerPosition * 10;

            return score;
        }

        // Hàm lấy danh sách các nước đi có thể thực hiện
        public List<Move> GetPossibleMoves()
        {
            List<Move> moves = new List<Move>();

            for (int x = 0; x < 19; x++)
            {
                for (int y = 0; y < 19; y++)
                {
                    if (game.CanBePlace(x, y))
                    {
                        moves.Add(new Move(x, y));
                    }
                }
            }

            return moves;
        }
    }

    class Move
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Move(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
