namespace advent.of.code.y2022;

// http://adventofcode.com/2022/day/9

using Pt = Point;
using Map = ImmutableSortedDictionary<Point,char>;

class HillClimbing : IPuzzle
{
	private static Pt[] adjacents = new Pt[]{Pt.North,Pt.South,Pt.East,Pt.West};

	internal record struct Instructions ( AdjacencyList adj, int start, int end);

	internal long CountUp(IEnumerable<string> input) {
		var m = input.ToMatrix( l => l);
		var map = ToMap(m);

		var adjList = map
			.Select( kvp => (xy:kvp.Key, weight:kvp.Value, startVertex:map.Keys.FindIndex( pt => pt == kvp.Key)))
			.Aggregate( new AdjacencyList(),
			(acc,cur) => AddVerticies(acc, cur.xy, cur.startVertex, cur.weight, map,
				weight => weight > 1));

		var start = map.First( kvp => kvp.Value == 'S').Key;
		var end = map.First( kvp => kvp.Value == 'E').Key;

		var instructions = new Instructions(adjList, 
			map.Keys.FindIndex( pt => pt == start), 
			map.Keys.FindIndex( pt => pt == end));

		var path = DijkstraSearch.FindNearestPath( instructions.adj, instructions.start, instructions.end).ToArray();

		return path.Length-1;
	}

	internal long CountDown(IEnumerable<string> input) {
		var m = input.ToMatrix( l => l);
		var map = ToMap(m);

		var resolver = (int index) => $"{map.Keys.ElementAt(index).CellAdr} '{map[map.Keys.ElementAt(index)]}'";

		var adjList = map
			.Select( kvp => (xy:kvp.Key, weight:kvp.Value, startVertex:map.Keys.FindIndex( pt => pt == kvp.Key)))
			.Aggregate( new AdjacencyList(),
			(acc,cur) => AddVerticies(acc, cur.xy, cur.startVertex, cur.weight, map,
				weight => weight < 1))
			with { Resolve = resolver };

		var start = map.First( kvp => kvp.Value == 'E').Key;
		var end = map.First( kvp => kvp.Value == 'S').Key;

		var instructions = new Instructions(adjList, 
			map.Keys.FindIndex( pt => pt == start), 
			map.Keys.FindIndex( pt => pt == end));

		var path = DijkstraSearch.FindNearestPathForWeight( instructions.adj, instructions.start, instructions.end, 0).ToArray();

		return path.Length-1;
	}

	private AdjacencyList AddVerticies(AdjacencyList adjList, Pt xy, int startVertex, char startWeight, Map map, Predicate<int> predicate)
	{
		var newAdj = adjacents
			.Select( cur => xy + cur)
			.Where( at => map.ContainsKey(at))
			.Aggregate( adjList, 
			(acc, cur) =>  {
				var weight = ToCode(map[cur])-(ToCode(startWeight));
				var endVertex = map.Keys.FindIndex(pt => pt == cur);
				if (predicate(weight)) return acc;
				return acc.AddEdge(startVertex, new Vertex(endVertex, 1));
			});

		return newAdj;
	}

	internal Map ToMap(char[][] chars)
	{
		var height = chars.Length;
		var width = chars[0].Length;
		return Pt.Cloud(width, height)
			.Aggregate(Map.Empty, (acc, cur) => acc.Add(cur, chars[cur.Y][cur.X]));
	}

	private int ToCode(char ch) {
		if (ch=='S') return 0;
		if (ch=='E') return ((int)'z'-(int)'a');
		return (int)ch-(int)'a';
	}



	
	public long Silver(IEnumerable<string> input) => CountUp(input);

	public long Gold(IEnumerable<string> input) => CountDown(input);
}
