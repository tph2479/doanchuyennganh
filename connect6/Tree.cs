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

        public void expand()
        {
            //foreach (var actionprior in actionpriors)
            //{
            //    int action = actionprior.item1;
            //    double prior = actionprior.item2;
            //    if (!children.containskey(action))
            //    {
            //        children[action] = new treenode(this, prior);
            //    }
            //}
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
