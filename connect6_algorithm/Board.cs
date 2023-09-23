using System;
using System.Collections.Generic;

namespace Connect6_MTCT
{
    public class Board
    {
        private int width;
        private int height;
        private Dictionary<int, int> states;
        private int n_in_row;
        private List<int> players;
        private int chesses;
        private List<int> last_moves;
        private List<int> curr_moves;
        private List<int> availables;
        private int current_player;
        private int last_move;

        public Board(int width = 19, int height = 19, int n_in_row = 6)
        {
            this.width = width;
            this.height = height;
            this.states = new Dictionary<int, int>();
            this.n_in_row = n_in_row;
            this.players = new List<int> { 1, 2 };
            this.chesses = 1;
            this.last_moves = new List<int>();
            this.curr_moves = new List<int>();
            this.availables = new List<int>();
            this.current_player = 0;
            this.last_move = -1;
        }

        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public int NInRow { get { return n_in_row; } }
        public List<int> Players { get { return players; } }
        public Dictionary<int, int> States { get { return states; } set { states = value; } }
        public List<int> Availables { get { return availables; } set { availables = value; } }
        public int CurrentPlayer { get { return current_player; } set { current_player = value; } }
        public int LastMove { get { return last_move; } set { last_move = value; } }
        public int Chesses { get { return chesses; } set { chesses = value; } }

        public void InitBoard(int start_player = 0)
        {
            if (width < n_in_row || height < n_in_row)
            {
                throw new Exception($"Board width and height cannot be less than {n_in_row}");
            }
            current_player = players[start_player];
            availables = Enumerable.Range(0, width * height).ToList();
            states.Clear();
            last_move = -1;
        }

        public void DoMove(int move)
        {
            states[move] = current_player;
            availables.Remove(move);
            last_move = move;
            curr_moves.Add(move);
            chesses--;
            if (chesses == 0)
            {
                ChangeTurn();
                chesses = 2;
            }
        }

        public void ChangeTurn()
        {
            current_player = (current_player == players[1]) ? players[0] : players[1];
            last_moves = new List<int>(curr_moves);
            curr_moves.Clear();
        }

        public bool HasAWinner(out int winner)
        {
            int n = n_in_row;
            int width = this.width;
            int height = this.height;

            List<int> moved = new List<int>();

            for (int i = 0; i < width * height; i++)
            {
                if (!availables.Contains(i))
                {
                    moved.Add(i);
                }
            }

            if (moved.Count < n + 2)
            {
                winner = -1;
                return false;
            }

            foreach (int m in moved)
            {
                int h = m / width;
                int w = m % width;
                int player = states[m];

                bool horizontalWin = true;
                for (int i = 0; i < n; i++)
                {
                    int pos = m + i;
                    if (states.GetValueOrDefault(pos, -1) != player)
                    {
                        horizontalWin = false;
                        break;
                    }
                }

                if (w >= 0 && w < width - n + 1 && horizontalWin)
                {
                    winner = player;
                    return true;
                }

                bool verticalWin = true;
                for (int i = 0; i < n * width; i += width)
                {
                    int pos = m + i;
                    if (states.GetValueOrDefault(pos, -1) != player)
                    {
                        verticalWin = false;
                        break;
                    }
                }

                if (h >= 0 && h < height - n + 1 && verticalWin)
                {
                    winner = player;
                    return true;
                }

                bool diagonalWin1 = true;
                for (int i = 0; i < n * (width + 1); i += width + 1)
                {
                    int pos = m + i;
                    if (states.GetValueOrDefault(pos, -1) != player)
                    {
                        diagonalWin1 = false;
                        break;
                    }
                }

                if (w >= 0 && w < width - n + 1 && h >= 0 && h < height - n + 1 && diagonalWin1)
                {
                    winner = player;
                    return true;
                }

                bool diagonalWin2 = true;
                for (int i = 0; i < n * (width - 1); i += width - 1)
                {
                    int pos = m + i;
                    if (states.GetValueOrDefault(pos, -1) != player)
                    {
                        diagonalWin2 = false;
                        break;
                    }
                }

                if (w >= n - 1 && w < width && h >= 0 && h < height - n + 1 && diagonalWin2)
                {
                    winner = player;
                    return true;
                }
            }

            winner = -1;
            return false;
        }

        public bool GameEnd(out int winner)
        {
            bool win = HasAWinner(out winner);
            if (win)
            {
                return true;
            }
            else if (availables.Count == 0)
            {
                winner = -1;
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetCurrentPlayer()
        {
            return current_player;
        }

        public bool IsStart()
        {
            return availables.Count == (width * height);
        }

        public override string ToString()
        {
            return height + "_" + width + "_" + n_in_row;
        }

        public (int, int) MoveToLocation(int move)
        {
            int h = move / width;
            int w = move % width;
            return (h, w);
        }

        public int LocationToMove((int, int) location)
        {
            int h = location.Item1;
            int w = location.Item2;
            return h * width + w;
        }

        public int[,] CurrentState()
        {
            int[,] square_state = new int[width, height];
            if (states.Count > 0)
            {
                var moves = states.Keys.ToArray();
                var players = states.Values.ToArray();
                var move_curr = moves.Where((move, index) => players[index] == current_player).ToArray();
                var move_oppo = moves.Where((move, index) => players[index] != current_player).ToArray();
                foreach (int move in move_curr)
                {
                    int h = move / height;
                    int w = move % height;
                    square_state[h, w] = 1; // Represent current player's moves as 1
                }
                foreach (int move in move_oppo)
                {
                    int h = move / height;
                    int w = move % height;
                    square_state[h, w] = -1; // Represent opponent's moves as -1
                }
                foreach (int move in last_moves)
                {
                    int h = move / height;
                    int w = move % height;
                    square_state[h, w] = 2; // Represent last moves as 2
                }
                foreach (int move in curr_moves)
                {
                    int h = move / height;
                    int w = move % height;
                    square_state[h, w] = 3; // Represent current moves as 3
                }
            }
            return square_state;
        }
    }
}