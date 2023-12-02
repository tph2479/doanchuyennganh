import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;

public class Game {

	private Board board;
	private boolean isPlayersTurn = true;
	private boolean gameFinished = false;
	private int MCTSDepth = 3;
	private boolean aiStarts = true; // AI makes the first move
	private MCTS ai;
	public static final String cacheFile = "score_cache.ser";
	private int winner; // 0: There is no winner yet, 1: AI Wins, 2: Human Wins
	private boolean isFirst = true; // first start of game
	private int turnCount = 1; // reached turnCount = 2, switch turn, reset to 1

	public Game(Board board) {
		this.board = board;
		ai = new MCTS(board, aiStarts);
		winner = 0;
	}

	/*
	 * Loads the cache and starts the game, enabling human player interactions.
	 */
	public void start() {
		// If the AI is making the first move, place a white stone in the middle of the
		// board.
		if (aiStarts)
			playMove(board.getBoardSize() / 2, board.getBoardSize() / 2, !aiStarts);
		// Now it's human player's turn.

		// Make the board start listening for mouse clicks.
		board.startListening(new MouseListener() {

			public void mouseClicked(MouseEvent arg0) {
				if (isPlayersTurn) {
					isPlayersTurn = false;
					// Handle the mouse click in another thread, so that we do not held the event
					// dispatch thread busy.
					Thread mouseClickThread = new Thread(new MouseClickHandler(arg0));
					mouseClickThread.start();
				}
			}

			public void mouseEntered(MouseEvent arg0) {
				// TODO Auto-generated method stub

			}

			public void mouseExited(MouseEvent arg0) {
				// TODO Auto-generated method stub

			}

			public void mousePressed(MouseEvent arg0) {
				// TODO Auto-generated method stub

			}

			public void mouseReleased(MouseEvent arg0) {
				// TODO Auto-generated method stub

			}

		});
	}

	/*
	 * Sets the depth of the MCTS tree. (i.e. how many moves ahead should the AI
	 * calculate.)
	 */
	public void setAIDepth(int depth) {
		this.MCTSDepth = depth;

	}

	public void setAIStarts(boolean aiStarts) {
		this.aiStarts = aiStarts;
		this.ai.setAIStarts(aiStarts);
	}

	public class MouseClickHandler implements Runnable {
		MouseEvent e;

		public MouseClickHandler(MouseEvent e) {
			this.e = e;
		}

		public void run() {
			if (gameFinished)
				return;

			// Find out which cell of the board do the clicked coordinates belong to.

			int posX = board.getRelativePos(e.getX());
			int posY = board.getRelativePos(e.getY());

			if (!playMove(posX, posY, aiStarts)) {
				// If the cell is already populated, do nothing.
				isPlayersTurn = true;
				return;
			}

			// Check if the last move ends the game.
			// kiểm tra bước đánh của người chơi có thắng không
			winner = checkWinner(board);

			if (winner == (aiStarts ? 2 : 1)) {
				System.out.println("Player WON!");
				board.printWinner(winner, aiStarts);
				gameFinished = true;
				return;
			}

			if (isFirst) {
//				System.out.println("First time run");
				isPlayersTurn = true;
			} else if (turnCount < 2) {
//				System.out.println(isPlayersTurn ? "Player" : "Al" + " turn");
				turnCount++;
				isPlayersTurn = true;
				return;
			}

			turnCount = 1; // switch turn

			// Make the AI instance calculate a move.
			int[] aiMove = ai.calculateNextMove(1, isFirst);

			if (aiMove == null) {
//				System.out.println("No possible moves left. Game Over.");
				board.printWinner(0, false); // Prints "TIED!"
				gameFinished = true;
				return;
			}

			// Place a black stone to the found cell.
			playMove(aiMove[0], aiMove[1], !aiStarts);

			// System.out.println("Black: " + MCTS.getScore(board,true,true) + " White: " +
			// MCTS.getScore(board,false,true));

			// kiểm tra nước đánh của máy có thắng không
			winner = checkWinner(board);

			if (winner == (aiStarts ? 1 : 2)) {
//				System.out.println("AI WON!");
				board.printWinner(winner, aiStarts);
				gameFinished = true;
				return;
			}

			if (board.generateMoves().size() == 0) {
//				System.out.println("No possible moves left. Game Over.");
				board.printWinner(0, false); // Prints "TIED!"
				gameFinished = true;
				return;
			}

			if (isFirst) {
				isFirst = false;
				if(!aiStarts)
					return;
			}

			// NƯỚC ĐÁNH THỨ 2
			// Make the AI instance calculate a move.
			int[] aiMove2 = ai.calculateNextMove(2, false);

			if (aiMove2 == null) {
//				System.out.println("No possible moves left. Game Over.");
				board.printWinner(0, false); // Prints "TIED!"
				gameFinished = true;
				return;
			}

			// Place a black stone to the found cell.
			playMove(aiMove2[1], aiMove2[0], !aiStarts);

			// System.out.println("Black: " + MCTS.getScore(board,true,true) + " White: " +
			// MCTS.getScore(board,false,true));

			// kiểm tra nước thứ 2 máy đánh có thắng không
			winner = checkWinner(board);

			if (winner == (aiStarts ? 1 : 2)) {
//				System.out.println("AI WON!");
				board.printWinner(winner, aiStarts);
				gameFinished = true;
				return;
			}

			if (board.generateMoves().size() == 0) {
//				System.out.println("No possible moves left. Game Over.");
				board.printWinner(0, aiStarts); // Prints "TIED!"
				gameFinished = true;
				return;
			}

			isPlayersTurn = true;
		}

	}

	public static int checkWinner(Board board) {
		// kiểm tra đen (người chơi), lượt của đen
		if (MCTS.getScore(board, true, false) >= MCTS.getWinScore())
			return 2;

		// kiểm tra trắng (máy) lượt của trắng
		if (MCTS.getScore(board, false, true) >= MCTS.getWinScore())
			return 1;
		return 0;
	}

	private boolean playMove(int posX, int posY, boolean black) {
		boolean ret = board.addStone(posX, posY, black);
		if(ret) {
			ai.setBoard(board);
		}
		return ret; 
	}
}
