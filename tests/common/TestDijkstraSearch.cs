
namespace advent.of.code.tests.common;

public class TestDijkstraSearch
{
	/*

	// https://www.youtube.com/watch?v=iFWvuQwRcUw

	// https://ericbackhage.net/c/how-to-design-a-priority-queue-in-c/
	// https://www.codeproject.com/Articles/1221034/Pathfinding-Algorithms-in-Csharp

	From the start node, add all connected nodes to a priority queue.
	Sort the priority queue by lowest cost and make the first node the current node.
	For every child node, select the best that leads to the shortest path to start.
	When all edges have been investigated from a node, that node is "Visited" and you don´t need to go there again.
	Add each child node connected to the current node to the priority queue.
	Go to step 2 until the queue is empty.
	Recursively create a list of each nodes node that leads the shortest path from end to start.
	Reverse the list and you have found the shortest path

	*/

	[Fact]
	public void Dijkstra()
	{
		var x = 0;
		var y = 1;
		var u = 2;
		var v = 3;
		var w = 4;

		var G = new AdjacencyList()
			.AddEdge(u, new Vertex(v, 5))
			.AddEdge(v, new Vertex(u, 5))
			.AddEdge(u, new Vertex(x, 2))
			.AddEdge(x, new Vertex(u, 2))
			.AddEdge(v, new Vertex(x, 1))
			.AddEdge(x, new Vertex(v, 1))
			.AddEdge(v, new Vertex(w, 8))
			.AddEdge(w, new Vertex(v, 8))
			.AddEdge(x, new Vertex(w, 4))
			.AddEdge(w, new Vertex(x, 4))
			.AddEdge(x, new Vertex(y, 9))
			.AddEdge(y, new Vertex(x, 9))
			.AddEdge(y, new Vertex(w, 3))
			.AddEdge(w, new Vertex(y, 3));

		//       v - - 8 - - w 
		//    5/ |         / |
		//    /  |       /   |
		//  u    1     4     3
		//    \  |   /       |
		//   2 \ | /         |
		//       x - - -9- - y

		var nearestPath = DijkstraSearch.FindNearestPath(G, u, y);

		Assert.Equal(4, nearestPath.Count());
		Assert.Equal(9, G.CostOfPath(nearestPath));
		Assert.Equal(new[] { u, x, w, y }, nearestPath);
	}
}
