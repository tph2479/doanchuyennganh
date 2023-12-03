import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ThreadLocalRandom;

public class State {
	private Board board;
	PlayerType playerType; // 1 = Player, 2 = PC
	int visitCount;
	double winScore;

	public State(Board board, PlayerType playerType) {
		this.board = board;
		this.playerType = playerType;
	}

	public State(State state) {
		this.playerType = state.getPlayerType();
		this.visitCount = state.getVisitCount();
		this.winScore = state.getWinScore();
		this.board = new Board(state.board);
	}

	public State() {
		// TODO Auto-generated constructor stub
	}

	@Override
	public String toString() {
		return "State [playerType=" + playerType + ", visitCount=" + visitCount + ", winScore=" + winScore + "]";
	}

	public List<State> getAllPossibleStates() {
		List<State> states = new ArrayList<>();
		for (var state : board.generateMoves()) {
			Board dummyBoard = new Board(board);
//			if (dummyBoard.getBoardMatrix()[state[1]][state[0]] != 0) {
//				continue;
//			}
			dummyBoard.setLastMove(state);
			dummyBoard.addStoneNoGUI(state[1], state[0], playerType == PlayerType.PLAYER);

			states.add(new State(dummyBoard, playerType));
		}
		return states;
		// constructs a list of all possible states from current state
	}

	public boolean randomPlay() {
		/*
		 * get a list of all possible positions on the board and play a random move
		 */
		List<State> states = getAllPossibleStates();
		// System.out.println("DEBUG states size: " + states.size());
		if (states.size() > 0) {
			State randomMove = states.get(ThreadLocalRandom.current().nextInt(states.size()));
			this.board = new Board(randomMove.getBoard());
			return false;
		}

		int[][] boardMatrix = board.getBoardMatrix();
		for (int i = 0; i < boardMatrix.length; i++) {
			for (int j = 0; j < boardMatrix.length; j++) {
				if (boardMatrix[i][j] == 0) {
					boardMatrix[i][j] = playerType == PlayerType.PLAYER ? 1 : 2;
					return false;
				}
			}
		}

		return true;
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

	public void addWinScore(double winScore) {
		this.winScore += winScore;
	}

	public PlayerType getOpponent() {
		return playerType == PlayerType.AL ? PlayerType.PLAYER : PlayerType.AL;
	}

	public void togglePlayer() {
		playerType = getOpponent();
	}

	public void incrementVisit() {
		this.visitCount++;
	}
}