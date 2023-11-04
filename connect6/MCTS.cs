using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect6
{
    public class MCTS
    {
        private Tree root;
        
        public MCTS()
        {
            root = new Tree(null, 0, null);
        }

        public int currentSecond()
        {
            TimeSpan t = (DateTime.UtcNow - new DateTime(1970, 1, 1));
            return (int)t.TotalSeconds;
        }

        public void playout()
        {
            Tree node = root;
            int current = currentSecond();
            Console.WriteLine(current);
            while ((currentSecond() - current) < 3)
            {
                Tree bestNode = root.Select();             
            }
        }
    }
}
