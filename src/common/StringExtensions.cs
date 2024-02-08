using System.Collections;

namespace advent.of.code.common;

public static class StringExtensions
{
	public static (int start, int end) ExtractRange(this string value)
	{
		var ranges = value.Split('-').Select(x => Convert.ToInt32(x)).ToArray();
		return (start: ranges.First(), end: ranges.Last());
	}

	public static bool IsNumeric(this string value) => int.TryParse(value, out var _);


	public static Line ToLine(this string input, string pattern = @"^(\d*),(\d*)\s->\s(\d*),(\d*)$")
	{
		Match m = Regex.Match(input, pattern);
		if (m.Success)
		{
			var pts = ToPoints(m.Groups.Values.Skip(1).Select(grp => Convert.ToInt32(grp.Value))).ToArray();
			return new Line(pts[0], pts[1]);
		}
		return new Line(Point.Zero, Point.Zero);
	}

	private static IEnumerable<Point> ToPoints(IEnumerable<int> values)
	=> values.Chunk(2).Select(chunk => new Point(chunk[0], chunk[1]));
}
