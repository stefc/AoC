namespace advent.of.code.y2021.day9;

// http://adventofcode.com/2021/day/9

internal class SmokeBasin : IPuzzle
{
	public long Silver(IEnumerable<string> values)
	{
		var basin = Parse(values);
		return basin.Map.Where( kvp => basin.IsLower(kvp.Key)).Sum( kvp => 1 + kvp.Value);
	}

	public long Gold(IEnumerable<string> values) 
	{
		var basin = Parse(values);
		return basin.Map.Keys
			.Where( at => basin.IsLower(at))
			.Select( pt => basin.Flood(pt).Count())
			.OrderByDescending(x=>x)
			.Take(3)
			.Aggregate( 1, (acc,cur) => acc * cur);
	}

	public static Basin Parse(IEnumerable<string> values)
	=> new Basin(ToMatrix(values));
	
	private static int[][] ToMatrix(IEnumerable<string> lines) 
	=> lines.Select( l => l.ToDigits().ToArray()).ToArray();
	
}

public record Basin
{
	private readonly Point[] adjacents = new Point[]{Point.North,Point.South,Point.East,Point.West};

	public ImmutableDictionary<Point,int> Map { get; init; }
	
	public Basin(int[][] numbers)
	{
		var height = numbers.Length;
		var width = numbers[0].Length;
		Map = Point.Cloud(width, height)
			.Aggregate(ImmutableDictionary<Point,int>.Empty,
				(acc, cur) =>
				{
					var number = numbers[cur.Y][cur.X];
					return number < 9 ? acc.Add(cur,number) : acc;
				});
		
	}

	public bool IsLower(Point at)
	{
		var height = Map[at];
		return !adjacents
			.Select( cur => at + cur)
			.Where( cur => Map.ContainsKey(cur))
			.Select( xy => Map[xy])
			.Any( h => h <= height);
	}

	public ImmutableHashSet<Point> Flood(Point xy) => Flood(-1,xy, ImmutableHashSet<Point>.Empty);

	private ImmutableHashSet<Point> Flood(int h, Point xy, ImmutableHashSet<Point> visited) 
	=> ((Map.TryGetValue(xy, out var height))
		&& 
		((height > h) &&  (!visited.Contains(xy)))) 
		? 
		adjacents.Aggregate( visited.Add(xy), (acc, cur) => Flood( height, cur + xy, acc))
		:
		visited;
}



