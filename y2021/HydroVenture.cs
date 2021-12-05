namespace advent.of.code.y2021.day5;

// http://adventofcode.com/2021/day/5

public class HydroVenture : IPuzzle
{
	public int Silver(IEnumerable<string> values)
	{
		var lines = values.Select(ToLine).Where( l => l.IsHorizontal() || l.IsVertical()).ToArray();

		var field = lines.Aggregate(new VentField(), (accu,current) => accu + current);

		return field.CountOverlaps;
	}
		
	public int Gold(IEnumerable<string> values) {
		var lines = values.Select(ToLine).ToArray();

		var field = lines.Aggregate(new VentField(), (accu,current) => accu + current);

		return field.CountOverlaps;
	}
	

	
	public static Line ToLine(string input)
	{
		string pattern =
			@"^(\d*),(\d*)\s->\s(\d*),(\d*)$";

		Match m = Regex.Match(input, pattern);
		if (m.Success) {
			var pts = ToPoints(m.Groups.Values.Skip(1).Select(grp => Convert.ToInt32(grp.Value))).ToArray();
			return new Line(pts[0], pts[1]);
		}
		return new Line(Point.Zero, Point.Zero);
	}

	private static IEnumerable<Point> ToPoints(IEnumerable<int> values) 
	=> values.Chunk(2).Select( chunk => new Point(chunk[0], chunk[1]));
}




record VentField
{
	
	public ImmutableDictionary<Point,int> Visited { get; init; }
	
	public VentField()
	{
		Visited = ImmutableDictionary<Point,int>.Empty;
	}

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

	public int CountOverlaps => Visited.Values.Count( v => v >= 2);
	
}

// http://ericw.ca/notes/bresenhams-line-algorithm-in-csharp.html
public static class BresenhamLineAlgorithm {

	public static IEnumerable<Point> GetPointsOnLine(Line line) 
	=> GetPointsOnLine(line.start.X, line.start.Y, line.end.X, line.end.Y);

	public static IEnumerable<Point> GetPointsOnLine(int x0, int y0, int x1, int y1)
    {
        bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
        if (steep)
        {
            int t;
            t = x0; // swap x0 and y0
            x0 = y0;
            y0 = t;
            t = x1; // swap x1 and y1
            x1 = y1;
            y1 = t;
        }
        if (x0 > x1)
        {
            int t;
            t = x0; // swap x0 and x1
            x0 = x1;
            x1 = t;
            t = y0; // swap y0 and y1
            y0 = y1;
            y1 = t;
        }
        int dx = x1 - x0;
        int dy = Math.Abs(y1 - y0);
        int error = dx / 2;
        int ystep = (y0 < y1) ? 1 : -1;
        int y = y0;
        for (int x = x0; x <= x1; x++)
        {
            yield return new Point((steep ? y : x), (steep ? x : y));
            error = error - dy;
            if (error < 0)
            {
                y += ystep;
                error += dx;
            }
        }
        yield break;
    }
}
