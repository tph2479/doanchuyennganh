import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;


public class MCTS {
	// This variable is used to track the number of evaluations for benchmarking purposes.
	public static int evaluationCount = 0;
	// Board instance is responsible for board mechanics
	private Board board;
	private boolean aiStarts;
	// Win score should be greater than all possible board scores
	private static final int WIN_SCORE = 100_000_000;
	private int moveCount = 1;

	// Constructor
	public MCTS(Board board, boolean aiStarts) {
		this.board = board;
		this.aiStarts = aiStarts;
	}
	
	public void setAIStarts(boolean aiStarts) {
		this.aiStarts = aiStarts;
	}
	
	// Getter function for the winScore 
	public static int getWinScore() {
		return WIN_SCORE;
	}

	// This function calculates the relative score of the white player against the black.
	// (i.e. how likely is white player to win the game before the black player)
	// This value will be used as the score in the MCTS algorithm.
	public static double evaluateBoardForWhite(Board board, boolean blacksTurn) {
		evaluationCount++; 
		
		// Get board score of both players.
		double blackScore = getScore(board, true, blacksTurn);
		double whiteScore = getScore(board, false, blacksTurn);
		
		if(blackScore == 0) blackScore = 1.0;
		
		// Calculate relative score of white against black
		return whiteScore / blackScore;
	}

	// This function calculates the board score of the specified player.
	// (i.e. How good a player's general standing on the board by considering how many 
	//  consecutive 2's, 3's, 4's it has, how many of them are blocked etc...)
	public static int getScore(Board board, boolean forBlack, boolean blacksTurn) {
		// Read the board
		int[][] boardMatrix = board.getBoardMatrix();

		// Calculate score for each of the 3 directions
		return evaluateHorizontal(boardMatrix, forBlack, blacksTurn) +
				evaluateVertical(boardMatrix, forBlack, blacksTurn) +
				evaluateDiagonal(boardMatrix, forBlack, blacksTurn);
	}
	
	// This function is used to get the next intelligent move to make for the AI.
	public int[] calculateNextMove(int moveCount, boolean isFirst) {
		// Block the board for AI to make a decision.
		board.thinkingStarted();

		int[] move = new int[2];

		// Used for benchmarking purposes only.
//		long startTime = System.currentTimeMillis();

		// Check if any available move can finish the game to make sure the AI always
		// takes the opportunity to finish the game.
		// NOTE: sửa lại chỗ này thành kiểm tra cho connect6 (2 bước liên tiến)
		Object[] bestMove = searchWinningMove(board);

		if(bestMove != null) {
			// Finishing move is found.
			move[0] = (Integer)(bestMove[1]);
			move[1] = (Integer)(bestMove[2]);
		} else {
			// If there is no such move, search the MCTS tree with specified depth.
			this.moveCount = moveCount;
			bestMove = MCTSSearch(isFirst);
			if(bestMove[1] == null) {
				move = null;
			} else {
				move[0] = (Integer)(bestMove[1]);
				move[1] = (Integer)(bestMove[2]);
			}
		}
//		System.out.println("Cases calculated: " + evaluationCount + " Calculation time: " + (System.currentTimeMillis() - startTime) + " ms");
		board.thinkingFinished();
		
		evaluationCount=0;
		
		return move;
	}
	
	
	/*
	 * returns: {score, move[0], move[1]}
	 * */
	private Object[] MCTSSearch(boolean isFirst) {
		Object[] bestMove = {0, null, null};
		
		Tree tree = new Tree();
		Node rootNode = tree.getRoot();
		rootNode.getState().setBoard(board);
		rootNode.getState().setPlayerType(PlayerType.PLAYER);
        // root là người chơi đánh
		// nếu ai đánh trước thì board là player
		// nếu ai đánh sau thì board là người chơi
		
        long end = System.currentTimeMillis() + 1000;
        int i = 0;
		while (System.currentTimeMillis() < end) {
			i++;
//        while (i-- > 0) {
            Node promisingNode = selectPromisingNode(rootNode);
            
//            print(promisingNode.getState().getBoard());
//            System.out.println("DEBUG UCT: " + UCT.uctValue(1, 0, 1));
//            if (promisingNode.getState().getBoard().checkStatus() 
//              == Board.IN_PROGRESS) {
                expandNode(promisingNode, isFirst);
                isFirst = false;
//            }
            //print(selectPromisingNode(rootNode).getState().getBoard());
            Node nodeToExplore = promisingNode;
            if (promisingNode.getChildArray().size() > 0) {
                nodeToExplore = promisingNode.getRandomChildNode();
            }
            print(nodeToExplore.getState().getBoard());
            int playoutResult = simulateRandomPlayout(nodeToExplore);
//            System.out.println("DK: " + playoutResult);
            if(aiStarts)
            	backPropogation(nodeToExplore, playoutResult == 1 ? PlayerType.AL : PlayerType.PLAYER);
            else
            	backPropogation(nodeToExplore, playoutResult == 1 ? PlayerType.PLAYER : PlayerType.AL);
        }

		System.out.println("Playout: " + i);
        Node winnerNode = rootNode.getChildWithMaxScore();
        tree.setRoot(winnerNode);
        print(winnerNode.getState().getBoard());
//        return winnerNode.getState().getBoard();
		
		// Return the best move found
        
        bestMove[0] = winnerNode.getState().getWinScore();
//        bestMove[1] = winnerNode.getState().getBoard().getLastMove()[0];
//        bestMove[2] = winnerNode.getState().getBoard().getLastMove()[1];
        int[] copy = getLastMove(winnerNode);
        bestMove[1] = copy[0];
        bestMove[2] = copy[1];
		return bestMove;
	}

	private void backPropogation(Node nodeToExplore, PlayerType playerType) {
	    Node tempNode = nodeToExplore;
	    while (tempNode != null) {
	        tempNode.getState().incrementVisit();
	        if (tempNode.getState().getPlayerType() == playerType) {
	            tempNode.getState().addWinScore(1);
	        }
	        tempNode = tempNode.getParent();
	    }
	}
	
	private int simulateRandomPlayout(Node node) {
		Node tempNode = new Node(node);
	    State tempState = tempNode.getState();
//	    int boardStatus = tempState.getBoard().checkStatus();
	    int boardStatus = Game.checkWinner(tempState.getBoard());
	    // 0 chưa kết luận, 1 máy thắng, 2 người chơi thắng
	    if(boardStatus == (aiStarts ? 2 : 1)) {
	    	// nếu người chơi thắng thì không tính nước này nữa
	    	tempNode.getParent().getState().setWinScore(Integer.MIN_VALUE);
	    	return boardStatus;
	    }
	    
//	    if (boardStatus == opponent) {
//	        tempNode.getParent().getState().setWinScore(Integer.MIN_VALUE);
//	        return boardStatus;
//	    }
	    Scanner reader = new Scanner(System.in);
	    int turnCount = 1;
	    boolean filled = false;
	    
//	    print(tempNode.getState().getBoard());
	    tempState.togglePlayer();
	    
	    moveCount = 3 - moveCount;
	    while (boardStatus == 0) {	    	
	    	filled = tempState.randomPlay();
	    	if(filled == true)
	    		break;
//	    	print(tempState.getBoard());
//	    	reader.nextLine();
	    	boardStatus = Game.checkWinner(tempState.getBoard());
//	    	System.out.println("movecount: " + moveCount);
	    	
	    	if(moveCount == 2) {
	    		tempState.togglePlayer();
	    		moveCount = 1;
	    	} else {
	    		moveCount++;
	    	}
	    }
//	    while (boardStatus == Board.IN_PROGRESS) {
//	        tempState.togglePlayer();
//	        tempState.randomPlay();
//	        boardStatus = tempState.getBoard().checkStatus();
//	    }
	    return boardStatus;
	}

	private void expandNode(Node node, boolean isFirst) {
//		System.out.println("isfirst: " + isFirst);
		List<State> possibleStates = node.getState().getAllPossibleStates();
	    possibleStates.forEach(state -> {
	        Node newNode = new Node(state);
	        newNode.setParent(node);
	        if(isFirst || moveCount == 2)
	        	newNode.getState().setPlayerType(node.getState().getOpponent());
	        else
	        	newNode.getState().setPlayerType(node.getState().getPlayerType());
	        node.getChildArray().add(newNode);
	    });
	}

	private Node selectPromisingNode(Node rootNode) {
		Node node = rootNode;
		while (node.getChildArray().size() != 0) {
			node = UCT.findBestNodeWithUCT(node);
		}
		return node;
	}

	private void print(Board board2) {
		for(int i = 0; i < board2.getBoardSize(); i++) {
			for(int j = 0; j < board2.getBoardSize(); j++) {
				int b = board2.getBoardMatrix()[i][j];
				System.out.print((b != 0 ? b : "_") + " ");
			}
			System.out.println();
		}
	}
	
	private int[] getLastMove(Node winnerNode) {
		int[] r = new int[2];
		for(int i = 0; i < board.getBoardSize(); i++) {
			for(int j = 0; j < board.getBoardSize(); j++) {
				if(board.getBoardMatrix()[i][j] != winnerNode.getState().getBoard().getBoardMatrix()[i][j]) {
					r[0] = i;
					r[1] = j;
				}
			}
		}
		return r;
	}

	// This function looks for a move that can instantly win the game.
	private Object[] searchWinningMove(Board board) {
		ArrayList<int[]> allPossibleMoves = board.generateMoves();
		Object[] winningMove = new Object[3];
		
		// Iterate for all possible moves
		for(int[] move : allPossibleMoves) {
			evaluationCount++;
			// Create a temporary board that is equivalent to the current board
			Board dummyBoard = new Board(board);
			// Play the move on that temporary board without drawing anything
			dummyBoard.addStoneNoGUI(move[1], move[0], !aiStarts);
			
			// If the white player has a winning score in that temporary board, return the move.
			if(getScore(dummyBoard,aiStarts,false) >= WIN_SCORE) {
				winningMove[1] = move[0];
				winningMove[2] = move[1];
				return winningMove;
			}
		}
		return null;
	}

	// This function calculates the score by evaluating the stone positions in horizontal direction
	public static int evaluateHorizontal(int[][] boardMatrix, boolean forBlack, boolean playersTurn ) {

		int[] evaluations = {0, 2, 0}; // [0] -> consecutive count, [1] -> block count, [2] -> score
		// blocks variable is used to check if a consecutive stone set is blocked by the opponent or
		// the board border. If the both sides of a consecutive set is blocked, blocks variable will be 2
		// If only a single side is blocked, blocks variable will be 1, and if both sides of the consecutive
		// set is free, blocks count will be 0.
		// By default, first cell in a row is blocked by the left border of the board.
		// If the first cell is empty, block count will be decremented by 1.
		// If there is another empty cell after a consecutive stones set, block count will again be 
		// decremented by 1.
		// Iterate over all rows
		for(int i=0; i<boardMatrix.length; i++) {
			// Iterate over all cells in a row
			for(int j=0; j<boardMatrix[0].length; j++) {
				// Check if the selected player has a stone in the current cell
				evaluateDirections(boardMatrix,i,j,forBlack,playersTurn,evaluations);
			}
			evaluateDirectionsAfterOnePass(evaluations, forBlack, playersTurn);
		}

		return evaluations[2];
	}
	
	// This function calculates the score by evaluating the stone positions in vertical direction
	// The procedure is the exact same of the horizontal one.
	public static  int evaluateVertical(int[][] boardMatrix, boolean forBlack, boolean playersTurn ) {

		int[] evaluations = {0, 2, 0}; // [0] -> consecutive count, [1] -> block count, [2] -> score
		
		for(int j=0; j<boardMatrix[0].length; j++) {
			for(int i=0; i<boardMatrix.length; i++) {
				evaluateDirections(boardMatrix,i,j,forBlack,playersTurn,evaluations);
			}
			evaluateDirectionsAfterOnePass(evaluations,forBlack,playersTurn);
			
		}
		return evaluations[2];
	}

	// This function calculates the score by evaluating the stone positions in diagonal directions
	// The procedure is the exact same of the horizontal calculation.
	public static  int evaluateDiagonal(int[][] boardMatrix, boolean forBlack, boolean playersTurn ) {

		int[] evaluations = {0, 2, 0}; // [0] -> consecutive count, [1] -> block count, [2] -> score
		// From bottom-left to top-right diagonally
		for (int k = 0; k <= 2 * (boardMatrix.length - 1); k++) {
		    int iStart = Math.max(0, k - boardMatrix.length + 1);
		    int iEnd = Math.min(boardMatrix.length - 1, k);
		    for (int i = iStart; i <= iEnd; ++i) {
		        evaluateDirections(boardMatrix,i,k-i,forBlack,playersTurn,evaluations);
		    }
		    evaluateDirectionsAfterOnePass(evaluations,forBlack,playersTurn);
		}
		// From top-left to bottom-right diagonally
		for (int k = 1-boardMatrix.length; k < boardMatrix.length; k++) {
		    int iStart = Math.max(0, k);
		    int iEnd = Math.min(boardMatrix.length + k - 1, boardMatrix.length-1);
		    for (int i = iStart; i <= iEnd; ++i) {
				evaluateDirections(boardMatrix,i,i-k,forBlack,playersTurn,evaluations);
		    }
			evaluateDirectionsAfterOnePass(evaluations,forBlack,playersTurn);
		}
		return evaluations[2];
	}
	public static void evaluateDirections(int[][] boardMatrix, int i, int j, boolean isBot, boolean botsTurn, int[] eval) {
		// Check if the selected player has a stone in the current cell
		if (boardMatrix[i][j] == (isBot ? 2 : 1)) {
			// Increment consecutive stones count
			eval[0]++;
		}
		// Check if cell is empty
		else if (boardMatrix[i][j] == 0) {
			// Check if there were any consecutive stones before this empty cell
			if (eval[0] > 0) {
				// Consecutive set is not blocked by opponent, decrement block count
				eval[1]--;
				// Get consecutive set score
				eval[2] += getConsecutiveSetScore(eval[0], eval[1], isBot == botsTurn);
				// Reset consecutive stone count
				eval[0] = 0;
				// Current cell is empty, next consecutive set will have at most 1 blocked side.
			}
			// No consecutive stones.
			// Current cell is empty, next consecutive set will have at most 1 blocked side.
			eval[1] = 1;
		}
		// Cell is occupied by opponent
		// Check if there were any consecutive stones before this empty cell
		else if (eval[0] > 0) {
			// Get consecutive set score
			eval[2] += getConsecutiveSetScore(eval[0], eval[1], isBot == botsTurn);
			// Reset consecutive stone count
			eval[0] = 0;
			// Current cell is occupied by opponent, next consecutive set may have 2 blocked sides
			eval[1] = 2;
		} else {
			// Current cell is occupied by opponent, next consecutive set may have 2 blocked sides
			eval[1] = 2;
		}
	}
	private static void evaluateDirectionsAfterOnePass(int[] eval, boolean isBot, boolean playersTurn) {
		// End of row, check if there were any consecutive stones before we reached right border
		if (eval[0] > 0) {
			eval[2] += getConsecutiveSetScore(eval[0], eval[1], isBot == playersTurn);
		}
		// Reset consecutive stone and blocks count
		eval[0] = 0;
		eval[1] = 2;
	}

	// This function returns the score of a given consecutive stone set.
	// count: Number of consecutive stones in the set
	// blocks: Number of blocked sides of the set (2: both sides blocked, 1: single side blocked, 0: both sides free)
	public static  int getConsecutiveSetScore(int count, int blocks, boolean currentTurn) {
		final int winGuarantee = 1_000_000;
		// If both sides of a set is blocked, this set is worthless return 0 points.
		if(blocks == 2 && count < 6) return 0;

		switch(count) {
//		case 6: {
//			return WIN_SCORE;
//		}
		case 5: {
			// 5 consecutive wins the game
			if(currentTurn) return WIN_SCORE;
			else {
				// Opponent's turn
				// If neither side is blocked, 4 consecutive stones guarantees a win in the next turn.
				if(blocks == 0) return winGuarantee/4;
				// If only a single side is blocked, 4 consecutive stones limits the opponents move
				// (Opponent can only place a stone that will block the remaining side, otherwise the game is lost
				// in the next turn). So a relatively high score is given for this set.
				else return 300;
			}
		}
		case 4: {
			// 4 consecutive stones in the user's turn guarantees a win.
			// (User can win the game by placing the 5th stone after the set)
			if(blocks == 0) {
				// Neither side is blocked.
				// If it's the current player's turn, a win is guaranteed in the next 2 turns.
				// (User places another stone to make the set 4 consecutive, opponent can only block one side)
				// However the opponent may win the game in the next turn therefore this score is lower than win
				// guaranteed scores but still a very high score.
				if(currentTurn) return 50_000;
				// If it's the opponent's turn, this set forces opponent to block one of the sides of the set.
				// So a relatively high score is given for this set.
				else return 250;
			}
			else {
				// One of the sides is blocked.
				// Playmaker scores
				if(currentTurn) return 20;
				else return 15;
			}
		}
		case 3: {
			// 3 consecutive stones
			if(blocks == 0) {
				// Neither side is blocked.
				// If it's the current player's turn, a win is guaranteed in the next 2 turns.
				// (User places another stone to make the set 4 consecutive, opponent can only block one side)
				// However the opponent may win the game in the next turn therefore this score is lower than win
				// guaranteed scores but still a very high score.
				if(currentTurn) return 25_000;
				// If it's the opponent's turn, this set forces opponent to block one of the sides of the set.
				// So a relatively high score is given for this set.
				else return 200;
			}
			else {
				// One of the sides is blocked.
				// Playmaker scores
				if(currentTurn) return 10;
				else return 5;
			}
		}
		case 2: {
			// 2 consecutive stones
			// Playmaker scores
			if(blocks == 0) {
				if(currentTurn) return 7;
				else return 5;
			}
			else {
				return 3;
			}
		}
		case 1: {
			return 1;
		}
		}

		// More than 6 consecutive stones? 
		return WIN_SCORE*2;
	}
}
