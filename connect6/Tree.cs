using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect6
{
    public class Tree
    {
        public int X;
        public List<Tree> Branch = new List<Tree>();
        private int v;

        public Tree(int X)
        {
            this.X = X;
                }

        public Tree()
        {

        }
        public void Print(Tree tree)
        {
            foreach(var item in tree.Branch)
            {
                Console.WriteLine("\n" + item.X + "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                if(item.X!=0)
                {
                    Print(item);
                }
            }
        }
    }
}
