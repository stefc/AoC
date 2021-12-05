namespace advent.of.code.common;

public record Line(Point start, Point end);

public static class LineExtensions {

	public static bool IsVertical(this Line line) => line.start.X == line.end.X;  
	public static bool IsHorizontal(this Line line) => line.start.Y == line.end.Y;  
}

public static class StringExtensions {
	public static Line ToLine(this string input, string pattern = @"^(\d*),(\d*)\s->\s(\d*),(\d*)$" )
	{
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


