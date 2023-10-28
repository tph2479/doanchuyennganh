using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect6
{
    public class State
    {
        public Board Board { get; set; }
        public int PlayerNo { get; set; }
        public int VisitCount { get; set; }
        public double WinScore { get; set; }
        public State()
        {

        }
        public State(State other)
        {
            this.Board = new Board(other.Board);
            this.PlayerNo = other.PlayerNo;
            this.VisitCount = other.VisitCount;
            this.WinScore = other.WinScore;
        }
        // Copy constructor, getters, and setters can be defined here.

        public List<State> GetAllPossibleStates()
        {
            List<State> possibleStates = new List<State>();

            // Logic to generate possible states from the current state.
            // Add these states to the 'possibleStates' list.

            return possibleStates;
        }

        public void RandomPlay()
        {
            /* Get a list of all possible positions on the board and 
               play a random move. */
        }
        public void SetPlayerNo(int playerNo)
        {
            this.PlayerNo = playerNo;
        }
      public int GetVisitCount()
        {
            return this.VisitCount;
        }
        public void IncrementVisit()
        {
            VisitCount++;
        }
        public void AddScore(double score)
        {
            WinScore += score;
        }
        public int Opponent
        {
            
            get
            {
                return 3 - PlayerNo; 
            }
        }
        public void TogglePlayer()
        {
            PlayerNo = 3 - PlayerNo; 
        }
        public int GetOpponent()
        {
            return 3 - PlayerNo; 
        }
        public double GetWinScore()
        {
            return WinScore;
        }
    }

}
