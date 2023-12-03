import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ThreadLocalRandom;

public class Node {
	State state;
	Node parent;
	List<Node> childArray;

	public Node() {
		state = new State();
		childArray = new ArrayList<>();
	}

	public Node(State state) {
		this.state = state;
		childArray = new ArrayList<>();
	}

	public Node(Node node) {
		this.parent = node.getParent();
		this.childArray = node.getChildArray();
		this.state = new State(node.getState());
	}

	public State getState() {
		return state;
	}

	public void setState(State state) {
		this.state = state;
	}

	public Node getParent() {
		return parent;
	}

	public void setParent(Node parent) {
		this.parent = parent;
	}

	public List<Node> getChildArray() {
		return childArray;
	}

	public void setChildArray(List<Node> childArray) {
		this.childArray = childArray;
	}

	public Node getRandomChildNode() {
		return childArray.get(ThreadLocalRandom.current().nextInt(childArray.size()));
	}

	public Node getChildWithMaxScore() {
		if (childArray.size() == 0)
			return null;
		Node node = childArray.get(0);
		for (int i = 1; i < childArray.size(); i++)
			if (node.getState().getVisitCount() < childArray.get(i).getState().getVisitCount())
				node = childArray.get(i);
		return node;
	}

	@Override
	public String toString() {
		return "Node [state=" + state + "]";
	}
}
