using System.Diagnostics.CodeAnalysis;
using MoreLinq;

namespace advent.of.code.common;

public static class DijkstraSearch
{

	private record class Node(int index, int minCostToStart, int weight, Node nearestToStart)
	{
		public Node(int index, int weight) : this(index, int.MaxValue, weight, null)
		{ }
	}

	private class NodeComparer : IEqualityComparer<Node>
	{
		public bool Equals(Node x, Node y) => x.index.Equals(y.index);

		public int GetHashCode([DisallowNull] Node obj) => obj.index;
		public static NodeComparer Default = new NodeComparer();
	}

	public static IEnumerable<int> FindNearestPath(AdjacencyList G, int s, int e) => FindNearestPath(G, MoreEnumerable.Return(s), e);

	public static IEnumerable<int> FindNearestPath(AdjacencyList G, IEnumerable<int> s, int e)
	{
		var end = new Node(e, 0);
		var prioQueue = new PriorityQueue<Node, int>();
		prioQueue.EnqueueRange( s.Select( c => (Element: new Node(c, 0, 0, null), Priority:0)));
		var containingIndieces = s.ToImmutableHashSet();
		var visited = ImmutableHashSet<int>.Empty.Union(s);
		var nearestPath = ImmutableStack<int>.Empty;
		do
		{

			// prioQueue = prioQueue.OrderBy(n => n.minCostToStart).ToImmutableList();

			var node = prioQueue.Dequeue();
			containingIndieces = containingIndieces.Remove(node.index);
			// prioQueue = prioQueue.Remove(node);

			
			foreach (var cnn in G.Matrix[node.index])
			{
				var childNode = new Node(cnn.end, cnn.weight);
				if (visited.Contains(childNode.index))
					continue;

				if (node.minCostToStart + cnn.weight < childNode.minCostToStart)
				{
					childNode = childNode with
					{
						minCostToStart = node.minCostToStart + cnn.weight,
						nearestToStart = node
					};
					if (!containingIndieces.Contains(childNode.index)) {

						prioQueue.Enqueue(childNode, childNode.minCostToStart);
						containingIndieces = containingIndieces.Add(childNode.index);
					}
				}
			}
			visited = visited.Add(node.index);
			if (node.index == end.index)
			{
				while (node != null)
				{
					// totalCost += node.weight;
					nearestPath = nearestPath.Push(node.index);
					node = node.nearestToStart;
				}
				break;
			}

		} while (prioQueue.Count!=0);

		return nearestPath;
	}

	
}