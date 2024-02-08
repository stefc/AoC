namespace advent.of.code.y2022;

// http://adventofcode.com/2022/day/9

using Pt = Point;
using Map = ImmutableSortedDictionary<Point,char>;

class HillClimbing : IPuzzle
{
	private static Pt[] adjacents = new Pt[]{Pt.North,Pt.South,Pt.East,Pt.West};

	
	internal long CountUp(IEnumerable<string> input) {
		var m = input.ToMatrix( l => l);
		var map = ToMap(m);
		var w = m.GetLength(0);
		var PtToIdx = (Pt pt) => w * pt.X + pt.Y;
		
		var adjList = map
			.Select( kvp => (xy:kvp.Key, weight:kvp.Value, startVertex: PtToIdx(kvp.Key)))
			.Aggregate( new AdjacencyList(),
			(acc,cur) => AddVerticies(acc, cur.xy, cur.startVertex, cur.weight, map,
				weight => weight > 1, PtToIdx )) 
				
			with { Resolve = (int index) => $"{map.Keys.ElementAt(index).CellAdr} '{map[map.Keys.ElementAt(index)]}'" };

		var start = map.First( kvp => kvp.Value == 'S').Key;
		var end = map.First( kvp => kvp.Value == 'E').Key;
		
		var path = DijkstraSearch.FindNearestPath( adjList, PtToIdx(start), PtToIdx(end)).ToArray();

		return path.Length-1;
	}

	internal long CountDown(IEnumerable<string> input) {
		var m = input.ToMatrix( l => l);
		var map = ToMap(m);

		var w = m.GetLength(0);
		var PtToIdx = (Pt pt) => w * pt.X + pt.Y;

		var resolver = (int index) => $"{map.Keys.ElementAt(index).CellAdr} '{map[map.Keys.ElementAt(index)]}'";

		var adjList = map
			.Select( kvp => (xy:kvp.Key, weight:kvp.Value, startVertex:PtToIdx(kvp.Key)))
			.Aggregate( new AdjacencyList(),
			(acc,cur) => AddVerticies(acc, cur.xy, cur.startVertex, cur.weight, map,
				weight => weight > 1, PtToIdx ))
			with { Resolve = resolver };

		var end = map.First( kvp => kvp.Value == 'E').Key;	

		var possibleStartPositions = map.Where( kvp => kvp.Value == 'a' || kvp.Value == 'S' ).Select( kvp => PtToIdx(kvp.Key)).ToArray();

		var path = DijkstraSearch.FindNearestPath( adjList, possibleStartPositions, PtToIdx(end)).ToArray();
		return path.Length-1;
	}

	private AdjacencyList AddVerticies(AdjacencyList adjList, Pt xy, int startVertex, char startWeight, Map map, Predicate<int> predicate, Func<Pt,int> pointToIndex)
	{
		var newAdj = adjacents
			.Select( cur => xy + cur)
			.Where( at => map.ContainsKey(at))
			.Aggregate( adjList, 
			(acc, cur) =>  {
				var weight = ToCode(map[cur])-(ToCode(startWeight));
				var endVertex = pointToIndex(cur);
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
