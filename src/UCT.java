import java.util.Collections;
import java.util.Comparator;

public class UCT {
	public static double uctValue(int totalVisit, double nodeWinScore, int nodeVisit) {
		if (nodeVisit == 0) {
			return Integer.MAX_VALUE;
		}
		double value = (nodeWinScore / nodeVisit)
				+ 1.41 * Math.sqrt(Math.log(totalVisit) / nodeVisit);
		return value;
	}

	public static Node findBestNodeWithUCT(Node node) {
		int parentVisit = node.getState().getVisitCount();
		return Collections.max(node.getChildArray(), Comparator
				.comparing(c -> uctValue(parentVisit, c.getState().getWinScore(), c.getState().getVisitCount())));
	}
}
