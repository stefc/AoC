namespace advent.of.code.y2021.day15;

// http://adventofcode.com/2021/day/15

// https://toreopsahl.com/tnet/weighted-networks/shortest-paths/

public class Chiton : IPuzzle
{
	private static SmallPoint[] adjacents = new SmallPoint[]{SmallPoint.North,SmallPoint.South,SmallPoint.East,SmallPoint.West};

	public long Silver(IEnumerable<string> values) {
		var instructions = Chiton.Parse(values);
		var path = DijkstraSearch.FindNearestPath( instructions.adj, instructions.start, instructions.end);
		return instructions.adj.CostOfPath(path);
	}
	
	public long Gold(IEnumerable<string> values) => 42;

	internal record struct Instructions ( AdjacencyList adj, int start, int end) {

	}

	internal static Instructions Parse(IEnumerable<string> values)
	{
		var arr = ToMatrix(values);
		var map = ToMap(arr);

		var start = new SmallPoint(0,0);
		var end = new SmallPoint(arr.Length-1, arr.Length-1);
	
		var adjList = map
			.Select( kvp => (xy:kvp.Key, startVertex:map.Keys.FindIndex( pt => pt == kvp.Key)))
			.Aggregate( new AdjacencyList(),
			(acc,cur) => AddVerticies(acc, cur.xy, cur.startVertex, map));

		adjList.Print();

		return new Instructions(adjList, 
			map.Keys.FindIndex( pt => pt == start), 
			map.Keys.FindIndex( pt => pt == end));

	}

	internal static string SpreadHorizontal(string v)
	{
		return Enumerable.Range(1,4).Aggregate(v, (acc, cur) => acc + Spread(v, cur));
	}

	internal static string Spread(string v, int add=1)
	{
		var digits = new String(v.ToDigits()
			.Select( digit => ((digit + add-1) % 9)+1).Select( digit => (char)('0'+digit))
			.ToArray());
		return digits;
	}

	private static AdjacencyList AddVerticies(AdjacencyList adjList, SmallPoint xy, int startVertex, ImmutableSortedDictionary<SmallPoint,int> map)
	{
		var newAdj = adjacents
			.Select( cur => xy + cur)
			.Where( at => map.ContainsKey(at))
			.Aggregate( adjList, 
			(acc, cur) =>  {
				var weight = map[cur];
				var endVertex = map.Keys.FindIndex(pt => pt == cur);
				return acc.AddEdge(startVertex, new Vertex(endVertex, weight));
			});

		return newAdj;
	}

	private static int[][] ToMatrix(IEnumerable<string> lines) 
	=> lines.AsParallel().Select( l => l.ToDigits().ToArray()).ToArray();

	internal static ImmutableSortedDictionary<SmallPoint,int> ToMap(int[][] numbers)
	{
		var height = numbers.Length;
		var width = numbers[0].Length;
		var map = SmallPoint.Cloud(width, height)
			.Aggregate(ImmutableSortedDictionary<SmallPoint,int>.Empty,
				(acc, cur) => acc.Add(cur, numbers[cur.Y][cur.X]));
		return map;
	}
}
