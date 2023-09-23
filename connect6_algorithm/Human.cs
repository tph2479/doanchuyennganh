using System;

namespace Connect6_MTCT
{
    public class Human : Player
    {
        public Human()
        {
            player = -1;
        }

        public override void SetPlayerInd(int p)
        {
            player = p;
        }

        public override Tuple<int, double[]> GetAction(Board board)
        {
            try
            {
                while (true)
                {
                    Console.Write("Your move: "); // Prompt for move input
                    string locationStr = Console.ReadLine();
                    string[] locationParts = locationStr.Split(",");
                    int[] location = Array.ConvertAll(locationParts, int.Parse);
                    int move = board.LocationToMove((location[0], location[1]));

                    if (move == -1 || !board.Availables.Contains(move))
                    {
                        Console.WriteLine("Invalid move");
                    }
                    else
                    {
                        return Tuple.Create(move, new double[0]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid move");
                return GetAction(board); // Retry for valid move
            }
        }

        public override void ResetPlayer()
        {
            // No need for implementation in this case
        }

        public override string ToString()
        {
            return $"Human {player}";
        }
    }
}
