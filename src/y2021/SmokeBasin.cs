namespace advent.of.code.y2021.day9;

// http://adventofcode.com/2021/day/9

internal class SmokeBasin : IPuzzle
{
	public long Silver(IEnumerable<string> values)
	{
		var basin = Parse(values);
		var isLower = basin.GetIsLower();
		return basin.Map.AsParallel().Where( kvp => isLower(kvp.Key)).Sum( kvp => 1 + kvp.Value);
	}

	public long Gold(IEnumerable<string> values) 
	{
		var basin = Parse(values);
		var isLower = basin.GetIsLower();
		return basin.Map.Keys
			.AsParallel()
			.Where(isLower)
			.Select( pt => basin.Flood(pt).Count())
			.OrderByDescending(x=>x)
			.Take(3)
			.Aggregate( 1, (acc,cur) => acc * cur);
	}

	public static Basin Parse(IEnumerable<string> values) => new Basin(ToMatrix(values));
	
	private static sbyte[][] ToMatrix(IEnumerable<string> lines) 
	=> lines.AsParallel().Select( l => l.ToDigits().Select(Convert.ToSByte).ToArray()).ToArray();
	
}

public record Basin
{
	private readonly SmallPoint[] adjacents = new SmallPoint[]{SmallPoint.North,SmallPoint.South,SmallPoint.East,SmallPoint.West};

	public ImmutableDictionary<SmallPoint,sbyte> Map { get; init; }
	
	public Basin(sbyte[][] numbers)
	{
		var height = numbers.Length;
		var width = numbers[0].Length;
		Map = SmallPoint.Cloud(width, height)
			.AsParallel()
			.Aggregate(ImmutableDictionary<SmallPoint,sbyte>.Empty,
				(acc, cur) =>
				{
					var number = numbers[cur.Y][cur.X];
					return number < 9 ? acc.Add(cur,number) : acc;
				});
		
	}

	// Memoize ? 
	public bool IsLower(SmallPoint at)
	{
		var height = Map[at];
		return !adjacents
			.Select( cur => at + cur)
			.Where( cur => Map.ContainsKey(cur))
			.Select( xy => Map[xy])
			.Any( h => h <= height);
	}

	public Func<SmallPoint,bool> GetIsLower() => 
		at => IsLower(at);

	public ImmutableHashSet<SmallPoint> Flood(SmallPoint xy) => Flood(-1,xy, ImmutableHashSet<SmallPoint>.Empty);

	private ImmutableHashSet<SmallPoint> Flood(sbyte h, SmallPoint xy, ImmutableHashSet<SmallPoint> visited) 
	=> !(Map.TryGetValue(xy, out var height) && ((height > h) &&  (!visited.Contains(xy)))) 
		? 
		visited
		:
		adjacents.Aggregate( visited.Add(xy), (acc, cur) => Flood( height, cur + xy, acc));
}
