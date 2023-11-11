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
            //while ((currentSecond() - current) < 3)
            //{
                Tree bestNode = select(root);
                if(bestNode.isEnded() == false)
                {
                    expand(bestNode);
                }
                //root.setni(1);
                //root.setNi(1);
                //root.setwi(1);
                //Console.WriteLine(root.getValue() + " " + bestNode.getValue());
            //}

            Tree nodeToExplore = bestNode;
            if(bestNode.Children.Count > 0)
                nodeToExplore = bestNode.getRandomChild();

            simulate(nodeToExplore);
            backPropogation(nodeToExplore);
        }

        private void backPropogation(Tree nodeToExplore)
        {
            
        }

        private void simulate(Tree nodeToExplore)
        {
            
        }

        public void expand(Tree bestNode)
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

        public Tree select(Tree root)
        {
            Tree node = root;

            if (root.Children == null || root.Children.Count == 0)
                return node;

            double max = root.Children.First().getValue();

            for (int i = 1; i < root.Children.Count; i++)
            {
                if (max < root.Children[i].getValue())
                {
                    max = root.Children[i].getValue();
                    node = root.Children[i];
                }
            }

            return node;
        }
    }
}
