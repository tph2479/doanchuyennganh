using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect6
{
    class Tree
    {
        private Tree parent;
        private List<Tree> children;
        private Piece[,] Pieces;

        public Tree(Tree parent, double priorP, Piece[,] piece)
        {
            this.parent = parent;
        }

        public Tree Parent { get { return parent; } set { parent = value; } }

        public List<Tree> Children { get { return children; } }

        public bool IsLeaf()
        {
            return children.Count == 0;
        }

        //public double GetUCB(double cPuct)
        //{
        //    u = cPuct * p * Math.Sqrt(parent.NVisits) / (1 + nVisits);
        //    return q + u;
        //}

    }
}
