using MoreLinq;

namespace advent.of.code.common;

// http://theoryofprogramming.com/adjacency-list-implementation-in-c-sharp/
public record struct Vertex(int end, int weight) { }

public record struct AdjacencyList(ImmutableDictionary<int, ImmutableList<Vertex>> Matrix)
{
	// Constructor - creates an empty Adjacency List
	public AdjacencyList() : this(ImmutableDictionary<int, ImmutableList<Vertex>>.Empty)
	{
	}

	// Appends a new Edge to the linked list
	public AdjacencyList AddEdge(int startVertex, Vertex vertex)
	{
		var newMatrix = !Matrix.TryGetValue(startVertex, out var list) ?
			Matrix.Add(startVertex, ImmutableList<Vertex>.Empty.Add(vertex))
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
		foreach (var startVertex in this.Matrix.Keys.OrderBy( x => x))
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


	public int CostOfPath(IEnumerable<int> nearestPath) 
	{	
		var m = this.Matrix;
		return nearestPath
			.Pairwise( (act,prev) => (index: act, prev:prev) )
			.Aggregate( 0, (acc, cur) => acc + m[cur.index].Single( v => v.end == cur.prev).weight);
	}
}