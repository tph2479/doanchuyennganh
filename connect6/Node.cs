using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect6
{
    public class Node
    {
        public State State { get; set; }
        public Node Parent { get; set; }
        public List<Node> ChildArray { get; set; }
        public List<Node> nodeList = new List<Node>();
       
        public Node()
        {

        }
        public Node(State state)
        {
            this.State = state;
            this.Parent = null;
            this.ChildArray = new List<Node>();
        }
        public State GetState()
        {
            return State;
        }
        public Node GetParent()
        {
            return this.Parent;
        }
        public void SetParent(Node parent)
        {
            this.Parent = parent;
        }

        public List<Node> GetChildArray()
        {

            return ChildArray;
        }

        public void SetChildArray(List<Node> childArray)
        {
            this.ChildArray = childArray;
        }
        public Node GetChildWithMaxScore()
        {
        
            if (ChildArray.Count == 0)
            {
                return null;
            }

            Node maxScoreNode = ChildArray[0];
            double maxScore = maxScoreNode.State.WinScore;

            foreach (var childNode in ChildArray)
            {
                if (childNode.State.WinScore > maxScore)
                {
                    maxScoreNode = childNode;
                    maxScore = childNode.State.WinScore;
                }
            }

            return maxScoreNode;
        }
        public Node GetRandomChildNode()
        {
            // Thực hiện logic để lấy một con ngẫu nhiên từ danh sách các con
            Random random = new Random();
            int randomIndex = random.Next(0, ChildArray.Count);
            return ChildArray[randomIndex];
        }


    }
}
