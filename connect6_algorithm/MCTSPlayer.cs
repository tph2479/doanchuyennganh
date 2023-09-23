namespace Connect6_MTCT
{
    public class MCTSPlayer : Player
    {
        private MCTS mcts;

        public MCTSPlayer(Func<Board, Tuple<List<Tuple<int, double>>, double>> PolicyValueFn, double cPuct = 5, int nPlayout = 2000)
        {
            mcts = new MCTS(PolicyValueFn, cPuct, nPlayout);
        }

        public override void SetPlayerInd(int playerInd)
        {
            player = playerInd;
        }

        public override Tuple<int, double[]> GetAction(Board board)
        {
            List<int> sensibleMoves = board.Availables;
            if (sensibleMoves.Count > 0)
            {
                Tuple<int, double[]> moveProbs = mcts.GetMove(board);
                mcts.UpdateWithMove(moveProbs.Item1);
                return moveProbs;
            }
            else
            {
                Console.WriteLine("WARNING: the board is full");
                return null;
            }
        }

        public override void ResetPlayer()
        {
            mcts.UpdateWithMove(-1);
        }
    }
}