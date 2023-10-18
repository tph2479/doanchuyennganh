using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect6
{
    
    public class UCT
    {
        
        private Node SelectPromisingNode(Node rootNode)
        {
            Node node = rootNode;
            while (node.GetChildArray().Count != 0)
            {
                node = UCT.FindBestNodeWithUCT(node);
            }
            return node;
        }
       private void ExpandNode(Node node)
{
    List<State> possibleStates = node.GetState().GetAllPossibleStates();
    foreach (State state in possibleStates)
    {
        Node newNode = new Node(state);
        newNode.SetParent(node);
        newNode.GetState().SetPlayerNo(node.GetState().GetOpponent());
        node.GetChildArray().Add(newNode);
    }
}

        public static double UctValue(int totalVisit, double nodeWinScore, int nodeVisit)
        {
            if (nodeVisit == 0)
            {
                return double.MaxValue;
            }
            return (nodeWinScore / (double)nodeVisit) + 1.41 * Math.Sqrt(Math.Log(totalVisit) / (double)nodeVisit);
        }

        public static Node FindBestNodeWithUCT(Node node)
        {
            int parentVisit = node.GetState().GetVisitCount();
            return node.GetChildArray().OrderByDescending(c => UctValue(parentVisit, c.GetState().GetWinScore(), c.GetState().GetVisitCount())).First();
        }
        

       
    }
    
}
