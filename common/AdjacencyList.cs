using System.Diagnostics;
using MoreLinq;


namespace advent.of.code.common;

using Vertices = ImmutableList<Vertex>;

// http://theoryofprogramming.com/adjacency-list-implementation-in-c-sharp/
public record struct Vertex(int end, int weight) { }

[DebuggerDisplay("{debugDescription,nq}")]
public record struct AdjacencyList(ImmutableDictionary<int, Vertices> Matrix, Func<int, string> Resolve = null)
{
	// Constructor - creates an empty Adjacency List
	public AdjacencyList() : this(ImmutableDictionary<int, Vertices>.Empty)
	{
	}
	
	// Appends a new Edge to the linked list
	public AdjacencyList AddEdge(int startVertex, Vertex vertex)
	{
		var newMatrix = !Matrix.TryGetValue(startVertex, out var list) ?
			Matrix.Add(startVertex, Vertices.Empty.Add(vertex))
			:
			Matrix.SetItem(startVertex, list.Add(vertex));

		return this with { Matrix = newMatrix };
	}

	// Removes the first occurence of an edge and returns true
	// if there was any change in the collection, else false
	public AdjacencyList RemoveEdge(int startVertex, Vertex vertex)
	{
		if (Matrix.TryGetValue(startVertex, out var list))
		{
			list = list.Remove(vertex);
			if (list.IsEmpty)
			{
				return this with { Matrix = this.Matrix.Remove(startVertex) };
			}
			else
			{
				return this with { Matrix = this.Matrix.SetItem(startVertex, list) };
			}
		}
		return this;
	}

	public void Print()
	{
		foreach (var startVertex in this.Matrix.Keys.OrderBy(x => x))
		{
			var list = this.Matrix[startVertex];
			Console.Write("adjacencyList[" + startVertex + "] -> ");

			foreach (var vertex in list)
			{
				Console.Write(vertex.end + "(" + vertex.weight + ")");
			}
			Console.WriteLine();
		}
	}

	private static string verticesToString(Vertices vertices) =>
		vertices.IsEmpty ? "empty" :
			string.Join(" ; ",
				vertices.Select(vertex => $"#{vertex.end} ({vertex.weight})"));

	private string debugDescription
	{
		get
		{
			var m = this.Matrix;
			var f = (int vertex) => verticesToString(m[vertex]);
			return this.Matrix == null ? "null" :
				string.Join(Environment.NewLine,
					this.Matrix.Keys
						.OrderBy(x => x)
						.Select(vertex => $"[#{vertex}] -> {f(vertex)}"));
		}
	}

	public int CostOfPath(IEnumerable<int> nearestPath)
	{
		var m = this.Matrix;
		return nearestPath
			.Pairwise((act, prev) => (index: act, prev: prev))
			.Aggregate(0, (acc, cur) => acc + m[cur.index].Single(v => v.end == cur.prev).weight);
	}
}