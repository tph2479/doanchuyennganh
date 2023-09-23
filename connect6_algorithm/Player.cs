namespace Connect6_MTCT
{
    public abstract class Player
    {
        protected int player;

        public int GetPlayer { get { return player; } }

        public abstract void SetPlayerInd(int playerInd);

        public abstract Tuple<int, double[]> GetAction(Board board);

        public abstract void ResetPlayer();
    }
}