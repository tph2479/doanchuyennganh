import java.util.ArrayList;
import java.util.List;

public class State {
    private Board board;
    PlayerType playerType; // 1 = Player, 2 = PC
    int visitCount;
    double winScore;
    
    public State() {
	}

    public State(Board board, int visitCount, double winScore) {
    	this.board = board;
    	this.visitCount = visitCount;
    	this.winScore = winScore;
	}

	public State(State state) {
		this.playerType = state.getPlayerType();
		this.visitCount = state.getVisitCount();
		this.winScore = state.getWinScore();
		this.board = new Board(board);
	}

	public List<State> getAllPossibleStates() {
    	List<State> states = new ArrayList<>();
    	Board dummyBoard = new Board(board);
    	for(var state : board.generateMoves()) {
    		dummyBoard.addStoneNoGUI(state[0], state[1], true);
    		states.add(new State(dummyBoard, visitCount, winScore));
    		dummyBoard = new Board(board);
    	}
		return states;
        // constructs a list of all possible states from current state
    }
    public void randomPlay() {
        /* get a list of all possible positions on the board and 
           play a random move */
    }

	public Board getBoard() {
		return board;
	}

	public void setBoard(Board board) {
		this.board = board;
	}

	public PlayerType getPlayerType() {
		return playerType;
	}

	public void setPlayerType(PlayerType al) {
		this.playerType = al;
	}

	public int getVisitCount() {
		return visitCount;
	}

	public void setVisitCount(int visitCount) {
		this.visitCount = visitCount;
	}

	public double getWinScore() {
		return winScore;
	}

	public void setWinScore(double winScore) {
		this.winScore = winScore;
	}

	public PlayerType getOpponent() {
		return playerType == PlayerType.AL ? PlayerType.PLAYER : PlayerType.AL;
	}
}