using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect6
{
    public class Tree
    {
        private Tree parent;
        private List<Tree> children = new List<Tree>();
        private Piece[,] Pieces;
        private int Ni, wi, ni;
        private Boolean ended = false;

        public Tree(Tree parent, double priorP, Piece[,] piece)
        {
            this.parent = parent;
        }

        public Tree Parent { get { return parent; } set { parent = value; } }

        public void setni(int value)
        {
            ni = value;
        }

        public void setNi(int value)
        {
            ni = value;
        }

        public void setwi(int value)
        {
            wi = value;
        }

        public List<Tree> Children { get { return children; } }

        public bool IsLeaf()
        {
            return children.Count == 0;
        }

        internal bool isEnded()
        {
            return ended;
        }

        //public double GetUCB(double cPuct)
        //{
        //    u = cPuct * p * Math.Sqrt(parent.NVisits) / (1 + nVisits);
        //    return q + u;
        //}

        public double getValue()
        {
            if (ni == 0)
                return 2147483647;
            return (double)wi / ni + 1.41 * Math.Sqrt((double)Math.Log(1)/(double)ni);
        }

        internal Tree getRandomChild()
        {
            int max = Children.Count;
            int index = new Random().Next(0, max);

            return Children.ElementAt(index);
        }

        //public Tuple<int, TreeNode> Select(double cPuct)
        //{
        //    double maxValue = double.MinValue;
        //    Tuple<int, TreeNode> selectedChild = null;

        //    foreach (var child in children)
        //    {
        //        double ucb = child.GetUCB(cPuct);
        //        if (ucb > maxValue)
        //        {
        //            maxValue = ucb;
        //            selectedChild = Tuple.Create(child.Key, child.Value);
        //        }
        //    }

        //    return selectedChild;
        //}


        //public void Update(double leafValue)
        //{
        //    nVisits++;
        //    q += (leafValue - q) / nVisits;
        //}

        //public void UpdateRecursive(double leafValue, bool flag)
        //{
        //    if (parent != null)
        //    {
        //        if (flag)
        //        {
        //            parent.UpdateRecursive(-leafValue, false);
        //        }
        //        else
        //        {
        //            parent.UpdateRecursive(leafValue, true);
        //        }
        //    }
        //    Update(leafValue);
        //}
    }
}
