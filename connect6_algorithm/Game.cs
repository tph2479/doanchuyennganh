namespace Connect6_MTCT
{
    public class Game
    {
        private Board board;

        public Game(Board board)
        {
            this.board = board;
        }

        public void Graphic(int player1, int player2)
        {
            Console.WriteLine($"Player {player1} with X".PadLeft(11));
            Console.WriteLine($"Player {player2} with O".PadLeft(11));
            Console.WriteLine();

            for (int x = 0; x < board.Width; x++)
            {
                if (x == 0)
                {
                    Console.Write($"    {x} ");
                }
                else
                {
                    Console.Write($" {x} ");
                }

            }
            Console.WriteLine();

            for (int i = 0; i <= board.Height - 1; i++)
            {
                if (i < 10)
                {
                    Console.Write($"{i}   ");
                }
                else
                {
                    Console.Write($"{i}  ");
                }

                for (int j = 0; j < board.Width; j++)
                {
                    int loc = i * board.Width + j;
                    int p = board.States.GetValueOrDefault(loc, -1);
                    if (j > 10)
                    {
                        Console.Write(" ");
                    }
                    if (p == player1)
                    {
                        Console.Write("X  ");
                    }
                    else if (p == player2)
                    {
                        Console.Write("O  ");
                    }
                    else
                    {
                        Console.Write(".  ");
                    }
                }
                Console.WriteLine();
            }
        }

        public int StartPlay(Player player1, Player player2, int startPlayer = 0, bool isShown = true)
        {
            if (startPlayer != 0 && startPlayer != 1)
            {
                throw new Exception("startPlayer should be either 0 (player1 first) or 1 (player2 first)");
            }

            board.InitBoard(startPlayer);
            player1.SetPlayerInd(board.Players[0]);
            player2.SetPlayerInd(board.Players[1]);
            int turn = 0;
            //int step = 1;

            // if (isShown)
            // {
            //     Graphic(player1.GetPlayer, player2.GetPlayer);
            // }

            while (true)
            {
                //step++;
                int currentPlayer = board.CurrentPlayer;
                Player playerInTurn = player1;
                playerInTurn = (currentPlayer == player1.GetPlayer) ? player1 : player2;
                //Console.WriteLine($"Player: {playerInTurn.GetPlayer} move with Current Player: {currentPlayer} and Player1.GetPlayer: {player1.GetPlayer}");

                var move = playerInTurn.GetAction(board);
                board.DoMove(move.Item1);

                if (isShown)
                {
                    Graphic(player1.GetPlayer, player2.GetPlayer);
                }
                turn++;

                bool end = board.GameEnd(out int winner);
                if (end)
                {
                    if (isShown)
                    {
                        if (winner != -1)
                        {
                            Console.WriteLine($"Game end. Winner is player: {winner} with Turn: {turn}");
                        }
                        else
                        {
                            Console.WriteLine("Game end. Tie");
                        }
                    }
                    return winner;
                }
            }
        }

        public int StartSelfPlay(Player player, bool isShown = false, double temp = 1e-3)
        {
            board.InitBoard();
            int p1 = board.Players[0];
            int p2 = board.Players[1];

            List<int[,]> states = new List<int[,]>();
            List<double[]> mctsProbs = new List<double[]>();
            List<int> currentPlayers = new List<int>();

            while (true)
            {
                var value = player.GetAction(board);
                var move = value.Item1;
                var moveProbs = value.Item2;
                states.Add(board.CurrentState());
                mctsProbs.Add(moveProbs);
                currentPlayers.Add(board.CurrentPlayer);

                board.DoMove(move);

                if (isShown)
                {
                    Graphic(p1, p2);
                }

                bool end = board.GameEnd(out int winner);
                if (end)
                {
                    double[] winnersZ = new double[currentPlayers.Count];
                    if (winner != -1)
                    {
                        for (int i = 0; i < currentPlayers.Count; i++)
                        {
                            winnersZ[i] = (currentPlayers[i] == winner) ? 1.0 : -1.0;
                        }
                    }
                    player.ResetPlayer();

                    if (isShown)
                    {
                        if (winner != -1)
                        {
                            Console.WriteLine($"Game end. Winner is player: {winner}");
                        }
                        else
                        {
                            Console.WriteLine("Game end. Tie");
                        }
                    }

                    return winner;
                }
            }
        }

    }
}