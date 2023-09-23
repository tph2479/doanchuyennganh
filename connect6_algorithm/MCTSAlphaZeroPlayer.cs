namespace Connect6_MTCT
{
    public class MCTSAlphaZeroPlayer : Player
    {
        private MCTSAlphaZero mcts;
        private bool _isSelfPlay;
        private Random random;

        public MCTSAlphaZeroPlayer(Func<Board, Tuple<List<Tuple<int, double>>, double>> PolicyValueFn, double cPuct = 5, int nPlayout = 2000, bool isSelfPlay = false)
        {
            _isSelfPlay = isSelfPlay;
            mcts = new MCTSAlphaZero(PolicyValueFn, cPuct, nPlayout);
            random = new Random();
        }

        public override void SetPlayerInd(int playerInd)
        {
            player = playerInd;
        }

        public override Tuple<int, double[]> GetAction(Board board)
        {
            List<int> sensibleMoves = board.Availables;
            double[] moveProbs = new double[board.Width * board.Height];

            if (sensibleMoves.Count > 0)
            {
                Tuple<int[], double[]> moveInfo = mcts.GetMoveProbs(board);
                var acts = moveInfo.Item1;
                var probs = moveInfo.Item2;

                Dictionary<int, double> moveProbsDict = new Dictionary<int, double>();
                for (int i = 0; i < acts.Length; i++)
                {
                    moveProbsDict[acts[i]] = probs[i];
                }

                foreach (int move in acts)
                {
                    moveProbs[move] = moveProbsDict[move];
                }

                if (_isSelfPlay)
                {
                    // Add Dirichlet Noise for exploration (needed for self-play training)
                    double[] noise = Enumerable.Repeat(0.3, probs.Length).Select(x => x * 0.25).ToArray();
                    double[] dirichlet = Dirichlet(noise);
                    double[] weightedProbs = probs.Select((p, i) => p * 0.75 + dirichlet[i]).ToArray();
                    int chosenMove = ChooseWeighted(acts, weightedProbs);

                    mcts.UpdateWithMove(chosenMove);
                    return Tuple.Create(chosenMove, moveProbs);
                }
                else
                {
                    int chosenMove = ChooseWeighted(acts, probs);
                    mcts.UpdateWithMove(-1);
                    return Tuple.Create(chosenMove, moveProbs);
                }
            }
            else
            {
                Console.WriteLine("WARNING: the board is full");
                return null;
            }
        }

        // public override Tuple<int, double[]> GetAction(Board board)
        // {
        //     List<int> sensibleMoves = board.Availables;
        //     if (sensibleMoves.Count > 0)
        //     {
        //         Tuple<int, double[]> moveProbs = mcts.GetMove(board);
        //         mcts.UpdateWithMove(moveProbs.Item1);
        //         return moveProbs;
        //     }
        //     else
        //     {
        //         Console.WriteLine("WARNING: the board is full");
        //         return null;
        //     }
        // }

        private int ChooseWeighted(int[] acts, double[] probs)
        {
            double[] probabilities = probs.ToArray();
            int[] actions = acts.ToArray();
            int chosenIndex = actions[ChooseIndexWithProbabilities(probabilities)];
            return chosenIndex;
        }

        private int ChooseIndexWithProbabilities(double[] probabilities)
        {
            double randomValue = random.NextDouble();
            double cumulativeProbability = 0.0;

            for (int i = 0; i < probabilities.Length; i++)
            {
                cumulativeProbability += probabilities[i];
                if (randomValue < cumulativeProbability)
                {
                    return i;
                }
            }

            return probabilities.Length - 1;
        }

        private double[] Dirichlet(double[] alpha)
        {
            double[] randomValues = new double[alpha.Length];
            double sum = 0.0;
            Random random = new Random();

            for (int i = 0; i < alpha.Length; i++)
            {
                randomValues[i] = random.NextDouble();
                sum += randomValues[i];
            }

            for (int i = 0; i < alpha.Length; i++)
            {
                randomValues[i] /= sum;
            }

            return randomValues;
        }


        public override void ResetPlayer()
        {
            mcts.UpdateWithMove(-1);
        }
    }
}