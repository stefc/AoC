namespace advent.of.code.y2021.day5;

// http://adventofcode.com/2021/day/5

public class HydroVenture : IPuzzle
{
	public long Silver(IEnumerable<string> values) => Calc( values, l => l.IsHorizontal() || l.IsVertical());
	
	public long Gold(IEnumerable<string> values) => Calc( values, _ => true);

	private long Calc(IEnumerable<string> values, Func<Line,bool> predicate) 
	=> values
		.Select(l => l.ToLine())
		.Where( predicate )
		.Aggregate(new VentField(), (accu, current) => accu + current, accu => accu.CountOverlaps);
}

record VentField
{
	public ImmutableDictionary<Point,int> Visited { get; init; }

	public VentField() => Visited = ImmutableDictionary<Point, int>.Empty;

	public VentField Draw(Line line) 
	{
		var points = BresenhamLineAlgorithm.GetPointsOnLine(line).ToArray();

		var newVisited = points.Aggregate( Visited, 
			(accu, current) => {
				return (accu.TryGetValue(current, out var visits)) 
					? accu.SetItem(current, visits + 1) 
					: accu.Add(current, 1);
			});
		return this with { Visited = newVisited };
	}

	public static VentField operator + (VentField state, Line line)
		=> state.Draw(line);

	public long CountOverlaps => Visited.Values.Count( v => v >= 2);	
}
