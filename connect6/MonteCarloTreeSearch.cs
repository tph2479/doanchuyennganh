using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect6
{
    public class MonteCarloTreeSearch
    {
        private const int WIN_SCORE = 10;
        private int level;
        private int opponent;

        public Board FindNextMove(Board board, int playerNo)
        {
            // Định nghĩa thời gian kết thúc dự kiến ở đây để làm điều kiện dừng

            opponent = 3 - playerNo;
            Tree tree = new Tree();
            
            Node rootNode = tree.Root;
            rootNode.State.Board = board;
            rootNode.State.PlayerNo = opponent;
            DateTime end = DateTime.Now.AddMinutes(5);
            while (DateTime.Now < end) // Chú ý: Thay đổi System.currentTimeMillis() thành DateTime.Now
            {
                Node promisingNode = SelectPromisingNode(rootNode);
                if (promisingNode.State.Board.CheckStatus() == Board.IN_PROGRESS)
                {
                    ExpandNode(promisingNode);
                }
                Node nodeToExplore = promisingNode;
                if (promisingNode.ChildArray.Count > 0)
                {
                    nodeToExplore = promisingNode.GetRandomChildNode();
                }
                int playoutResult = SimulateRandomPlayout(nodeToExplore);
                BackPropagation(nodeToExplore, playoutResult);
            }

            Node winnerNode = rootNode.GetChildWithMaxScore();
            tree.Root = winnerNode;
            return winnerNode.State.Board;
        }
        private void BackPropagation(Node nodeToExplore, int playerNo)
        {
            Node tempNode = nodeToExplore;
            while (tempNode != null)
            {
                tempNode.State.IncrementVisit();
                if (tempNode.State.PlayerNo == playerNo)
                {
                    tempNode.State.AddScore(WIN_SCORE);
                }
                tempNode = tempNode.Parent;
            }
        }
        private void ExpandNode(Node node)
        {
            List<State> possibleStates = node.State.GetAllPossibleStates();
            possibleStates.ForEach(state =>
            {
                Node newNode = new Node(state);
                newNode.Parent = node;
                newNode.State.PlayerNo = node.State.Opponent;
                node.ChildArray.Add(newNode);
            });
        }
        private Node SelectPromisingNode(Node rootNode)
        {
            // Thực hiện logic của phương thức SelectPromisingNode
            return rootNode;
        }

        private int SimulateRandomPlayout(Node node)
        {
            State state = new State();
            Node tempNode = new Node(state);
            State tempState = tempNode.State;
            int boardStatus = tempState.Board.CheckStatus();
            if (boardStatus == opponent)
            {
                tempNode.Parent.State.WinScore = int.MinValue;
                return boardStatus;
            }
            while (boardStatus == Board.IN_PROGRESS)
            {
                tempState.TogglePlayer();
                tempState.RandomPlay();
                boardStatus = tempState.Board.CheckStatus();
            }
            return boardStatus;
        }
        // Các phương thức khác có thể được thêm vào ở đây.
    }


}
