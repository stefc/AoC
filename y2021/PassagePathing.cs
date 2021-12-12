using MoreLinq;

namespace advent.of.code.y2021.day12;

// http://adventofcode.com/2021/day/12

public class PassagePathing : IPuzzle
{	
	public long Silver(IEnumerable<string> input) 
	=> Parse(input).FindAllPathsOnce();
	
	public long Gold(IEnumerable<string> input) 
	=> Parse(input).FindAllPathsTwice();

	public record struct Node (string Id) {
		public bool IsSmall => Id.First() >= 'a';
	}

	public record struct Edge (Node start, Node end) {}

	public record Graph (ImmutableList<Node> Nodes, ImmutableList<Edge> Edges) {

		public Graph(IEnumerable<string> values) : this(ImmutableList<Node>.Empty, ImmutableList<Edge>.Empty)
		{
			var edges = values.Select(ParseEdge).ToImmutableList();
			var nodes = edges.SelectMany( e => new[]{e.start,e.end}).Distinct().ToImmutableList();
			this.Edges = edges;
			this.Nodes = nodes;
			this.getNextNodes = NextNodes;
			this.getNextNodes = this.getNextNodes.Memoize();
		}
		
		static Node Start = new Node("start");
		static Node End = new Node("end");

		private readonly Func<Node,ImmutableHashSet<Node>> getNextNodes;

		public int FindAllPathsOnce() => FindAllPaths(Start, CanVisitSmallOnce);

		public int FindAllPathsTwice() => FindAllPaths(Start, CanVisitSmallTwice);

		public int FindAllPaths(Node node, Func<IEnumerable<Node>,Node,bool> canVisit) 
		=> FindAllPaths(0, ImmutableList<Node>.Empty, node, canVisit);

		public int FindAllPaths(int connectionPaths, 
			ImmutableList<Node> connectionPath, Node node, Func<IEnumerable<Node>,Node,bool> canVisit) 
		=> this.getNextNodes(node).AsParallel().Aggregate( connectionPaths, 
				(accu, nextNode) => 
					(nextNode == End)
					?
					accu+1
					:
					(
						(canVisit(connectionPath, nextNode)) 
						? 
						FindAllPaths(accu, connectionPath.Add(nextNode), nextNode, canVisit)
						: 
						accu
					));

		public static bool CanVisitSmallOnce(IEnumerable<Node> connectionPath, Node nextNode)
		=> !(nextNode.IsSmall && connectionPath.Contains(nextNode));

		public static bool CanVisitSmallTwice(IEnumerable<Node> connectionPath, Node nextNode) {
			if (nextNode.IsSmall) 
			{
				var nodes = connectionPath
					.Where( n => n.IsSmall)
					.GroupBy( n => n, (k,xs) => xs.Count() != 2);
				
				return (!connectionPath.Contains(nextNode) || nodes.All( x => x));
			}
			return true;
		}

		public ImmutableHashSet<Node> NextNodes(Node node) 
		=> Edges
			.Where( e => e.start == node || e.end == node)
			.Select( e => e.end == node ? e.start : e.end)
			.ToImmutableHashSet()
			.Remove(Start);
	}

	private static Edge ParseEdge (string line) {
		var parts = line.Split('-');
		return new Edge(new Node(parts.First()), new Node(parts.Last()));
	}

	public static Graph Parse(IEnumerable<string> values) => new Graph(values);
}