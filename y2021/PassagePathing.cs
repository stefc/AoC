using MoreLinq;

namespace advent.of.code.y2021.day12;

// http://adventofcode.com/2021/day/12

public class PassagePathing : IPuzzle
{	
	public long Silver(IEnumerable<string> input) 
	=> Parse(input).FindAllPathsOnce().Count();
	
	public long Gold(IEnumerable<string> input) 
	=> Parse(input).FindAllPathsTwice().Count();

	public record struct Node (string Id) {
		public bool IsSmall => Id.First() >= 'a';
	}

	public record struct Edge (Node start, Node end) {}

	public record Graph (ImmutableList<Node> Nodes, ImmutableList<Edge> Edges) {

		static Node Start = new Node("start");
		static Node End = new Node("end");

		public ImmutableHashSet<string> FindAllPathsOnce() => FindAllPaths(Start, CanVisitSmallOnce);

		public ImmutableHashSet<string> FindAllPathsTwice() => FindAllPaths(Start, CanVisitSmallTwice);

		public ImmutableHashSet<string> FindAllPaths(Node node, Func<IEnumerable<Node>,Node,bool> canVisit) 
		=> FindAllPaths(ImmutableHashSet<string>.Empty, ImmutableStack<Node>.Empty.Push(node), node, canVisit);

		private static string ToString(IEnumerable<Node> path) 
		=> String.Join(',', path.Select( n=> n.Id));

		public ImmutableHashSet<string> FindAllPaths(ImmutableHashSet<string> connectionPaths, 
			ImmutableStack<Node> connectionPath, Node node, Func<IEnumerable<Node>,Node,bool> canVisit) 
		{
			foreach( var nextNode in NextNodes(node)) 
			{
				if (nextNode == End)
				{
					connectionPaths = connectionPaths.Add(ToString(connectionPath.Push(End)));
				} else if (canVisit(connectionPath, nextNode)) {
					connectionPath = connectionPath.Push(nextNode);
					connectionPaths = FindAllPaths(connectionPaths, connectionPath, nextNode, canVisit);
					connectionPath = connectionPath.Pop(); 
				}
 			}
			return connectionPaths;
		}

		public static bool CanVisitSmallOnce(IEnumerable<Node> connectionPath, Node nextNode)
		=> (!nextNode.IsSmall || !connectionPath.Contains(nextNode));

		public static bool CanVisitSmallTwice(IEnumerable<Node> connectionPath, Node nextNode) {
			if (nextNode.IsSmall) 
			{
				var nodes = connectionPath
					.Where( n => n.IsSmall)
					.GroupBy( n => n)
					.ToDictionary( grp => grp.Key, grp => grp.Count());
				
				var alreadyTwice = nodes.Values.Any( x => x == 2);
				
				return !(nodes.ContainsKey(nextNode) && alreadyTwice);
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

	public static Graph Parse(IEnumerable<string> values) {
		var edges = values.Select(ParseEdge).ToImmutableList();
		var nodes = edges.SelectMany( e => new[]{e.start,e.end}).Distinct().ToImmutableList();
		return new Graph(nodes,edges);
	}
}