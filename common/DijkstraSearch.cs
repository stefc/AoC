using System.Diagnostics.CodeAnalysis;

namespace advent.of.code.common;

public static class DijkstraSearch {

    private record class Node(int index, int? minCostToStart, int weight, Node nearestToStart) 
	{
		public Node(int index, int weight) : this(index, null, weight, null)
		{}

	}

	private class NodeComparer : IEqualityComparer<Node>
    {
		public bool Equals(Node x, Node y) => x.index.Equals(y.index);
		
		public int GetHashCode([DisallowNull] Node obj) => obj.index;
        public static NodeComparer Default = new NodeComparer();
    }

    public static IEnumerable<int> FindNearestPath(AdjacencyList G, int s, int e) 
    {
        var start = new Node(s, 0, 0, null);
		var end = new Node(e, 0);
		var prioQueue = ImmutableList<Node>.Empty.Add(start);
		var visited = ImmutableHashSet<int>.Empty; 
		var nearestPath = ImmutableStack<int>.Empty;
		do {

			prioQueue = prioQueue.OrderBy( n => n.minCostToStart).ToImmutableList();

			var node = prioQueue.First();
			prioQueue = prioQueue.Remove(node);

			foreach(var cnn in G.Matrix[node.index]) {
				var childNode = new Node(cnn.end, cnn.weight);
				if (visited.Contains(childNode.index))
					continue;

				if (childNode.minCostToStart == null || 
					node.minCostToStart + cnn.weight < childNode.minCostToStart)
				{
					childNode = childNode with 
						{
							minCostToStart = node.minCostToStart + cnn.weight, 
					 		nearestToStart = node
						};
					if (!prioQueue.Contains(childNode, NodeComparer.Default))
						prioQueue = prioQueue.Add(childNode);
				}
			}
			visited = visited.Add(node.index);
			if (node.index==end.index)
			{
				while (node != null)
				{
					// totalCost += node.weight;
					nearestPath = nearestPath.Push(node.index);
					node = node.nearestToStart;
				}
				break;
			}

		} while (prioQueue.Any());

        return nearestPath;
    }

    public static IEnumerable<int> FindNearestPathForWeight(AdjacencyList G, int s, int e, int w) 
    {
        var start = new Node(s, 0, 0, null);
		var end = new Node(e, w);
	
		var prioQueue = ImmutableList<Node>.Empty.Add(start);
		var visited = ImmutableHashSet<int>.Empty; 
		var nearestPath = ImmutableStack<int>.Empty;
		do {

			prioQueue = prioQueue.OrderBy( n => n.minCostToStart).ToImmutableList();

			var node = prioQueue.First();
			prioQueue = prioQueue.Remove(node);

			foreach(var cnn in G.Matrix[node.index]) {
				var childNode = new Node(cnn.end, cnn.weight);
				if (visited.Contains(childNode.index))
					continue;

				if (childNode.minCostToStart == null ||
					node.minCostToStart + cnn.weight < childNode.minCostToStart)
				{
					childNode = childNode with 
						{
							minCostToStart = node.minCostToStart + cnn.weight, 
					 		nearestToStart = node
						};
					if (!prioQueue.Contains(childNode, NodeComparer.Default))
						prioQueue = prioQueue.Add(childNode);
				}
			}
			visited = visited.Add(node.index);
			if ((node.weight==end.weight) || (node.index==end.index))
			{
				while (node != null)
				{
					nearestPath = nearestPath.Push(node.index);
					node = node.nearestToStart;
				}
				break;
			}

		} while (prioQueue.Any());

        return nearestPath;
    }

}