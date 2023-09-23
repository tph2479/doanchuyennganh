namespace Connect6_MTCT
{
    public class TreeNode
    {
        private TreeNode parent;
        private Dictionary<int, TreeNode> children;
        private int nVisits;
        private double q;
        private double u;
        private double p;

        public TreeNode(TreeNode parent, double priorP)
        {
            this.parent = parent;
            children = new Dictionary<int, TreeNode>();
            nVisits = 0;
            q = 0;
            u = 0;
            p = priorP;
        }

        public TreeNode Parent { get { return parent; } set { parent = value; } }

        public Dictionary<int, TreeNode> Children { get { return children; } }

        public int NVisits { get { return nVisits; } }

        public double Q { get { return q; } }

        public double U { get { return u; } }

        public double P { get { return p; } }

        public void Expand(List<Tuple<int, double>> actionPriors)
        {
            foreach (var actionPrior in actionPriors)
            {
                int action = actionPrior.Item1;
                double prior = actionPrior.Item2;
                if (!children.ContainsKey(action))
                {
                    children[action] = new TreeNode(this, prior);
                }
            }
        }

        public Tuple<int, TreeNode> Select(double cPuct)
        {
            double maxValue = double.MinValue;
            Tuple<int, TreeNode> selectedChild = null;

            foreach (var child in children)
            {
                double ucb = child.Value.GetUCB(cPuct);
                if (ucb > maxValue)
                {
                    maxValue = ucb;
                    selectedChild = Tuple.Create(child.Key, child.Value);
                }
            }

            return selectedChild;
        }


        public void Update(double leafValue)
        {
            nVisits++;
            q += (leafValue - q) / nVisits;
        }

        public void UpdateRecursive(double leafValue, bool flag)
        {
            if (parent != null)
            {
                if (flag)
                {
                    parent.UpdateRecursive(-leafValue, false);
                }
                else
                {
                    parent.UpdateRecursive(leafValue, true);
                }
            }
            Update(leafValue);
        }

        public double GetUCB(double cPuct)
        {
            u = cPuct * p * Math.Sqrt(parent.NVisits) / (1 + nVisits);
            return q + u;
        }

        public bool IsLeaf()
        {
            return children.Count == 0;
        }
    }
}