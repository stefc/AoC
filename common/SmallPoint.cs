using System.Diagnostics;

namespace advent.of.code.common;

[DebuggerDisplay("{DebuggerDisplay,nq}")]

public record struct SmallPoint 
{
	private static Lazy<SmallPoint> zero = new Lazy<SmallPoint>(() => new SmallPoint(0, 0));
	private static Lazy<SmallPoint> north = new Lazy<SmallPoint>(() => new SmallPoint(0, -1));
	private static Lazy<SmallPoint> south = new Lazy<SmallPoint>(() => new SmallPoint(0, +1));
	private static Lazy<SmallPoint> west = new Lazy<SmallPoint>(() => new SmallPoint(-1, 0));
	private static Lazy<SmallPoint> east = new Lazy<SmallPoint>(() => new SmallPoint(+1, 0));
	private static Lazy<SmallPoint> northwest = new Lazy<SmallPoint>(() => new SmallPoint(-1, -1));
	private static Lazy<SmallPoint> northeast = new Lazy<SmallPoint>(() => new SmallPoint(+1, -1));
	private static Lazy<SmallPoint> southwest = new Lazy<SmallPoint>(() => new SmallPoint(-1, +1));
	private static Lazy<SmallPoint> southeast = new Lazy<SmallPoint>(() => new SmallPoint(+1, +1));

	public static SmallPoint Zero => zero.Value;
	public static SmallPoint North => north.Value;
	public static SmallPoint South => south.Value;
	public static SmallPoint West => west.Value;
	public static SmallPoint East => east.Value;
	public static SmallPoint NorthWest => northwest.Value;
	public static SmallPoint NorthEast => northeast.Value;
	public static SmallPoint SouthWest => southwest.Value;
	public static SmallPoint SouthEast => southeast.Value;



	public sbyte X { get; private set; }

	public sbyte Y { get; private set; }

	public SmallPoint(sbyte x, sbyte y)
	{
		X = x;
		Y = y;
	}

	public SmallPoint(int x, int y) : this(Convert.ToSByte(x), Convert.ToSByte(y)) 
	{
	}

	
	public static SmallPoint FromString(string value)
	{
		var parts = value.Split(',');
		sbyte x = Convert.ToSByte(parts.FirstOrDefault());
		sbyte y = Convert.ToSByte(parts.LastOrDefault());
		return new SmallPoint(x, y);
	}

	public int SquareLength => Math.Abs(this.X * this.X) + Math.Abs(this.Y * this.Y);
	public double Length => Math.Sqrt((this.X * this.X) + (this.Y * this.Y));

	public static SmallPoint operator +(SmallPoint a, SmallPoint b) => a.Add(b);

	public static SmallPoint operator -(SmallPoint a, SmallPoint b) => a.Sub(b);

	public static float operator ^(SmallPoint a, SmallPoint b) => a.Dotproduct(b);
	public static SmallPoint operator *(SmallPoint a, sbyte scalar)
	=> new SmallPoint(a.X * scalar, a.Y * scalar);

	public static SmallPoint operator *(SmallPoint a, float scalar)
	=> new SmallPoint((sbyte)(a.X * scalar), (sbyte)(a.Y * scalar));

	private string DebuggerDisplay => $"x={this.X},y={this.Y}";

	public static IEnumerable<SmallPoint> Cloud(int size) => Cloud(size,size);
	public static IEnumerable<SmallPoint> Cloud(int width, int height) 
	=> 	Enumerable.Range(0, width).SelectMany(x => Enumerable.Range(0, height).Select(y => new SmallPoint(x, y)));

}

public static class SmallPointExtensions
{

	public static IEnumerable<SmallPoint> AsEnumerable(this (SmallPoint start, SmallPoint end) line)
	{
		yield return line.start;
		yield return line.end;
	}

	public static SmallPoint Sub(this SmallPoint a, SmallPoint b)
	=> new SmallPoint(a.X - b.X, a.Y - b.Y);

	public static int ManhattenDistance(this SmallPoint point)
	=> Math.Abs(point.X) + Math.Abs(point.Y);

	public static SmallPoint Scale(this SmallPoint point, sbyte factor)
	=> new SmallPoint(point.X * factor, point.Y * factor);

	public static float Dotproduct(this SmallPoint a, SmallPoint b)
	=> (float)(a.X * b.X + a.Y * b.Y);

	public static SmallPoint ToSmallPoint(this string s)
	{
		var parts = s.Split(',').Select(p => Convert.ToSByte(p));
		return new SmallPoint(parts.First(), parts.Last());
	}

	public static IEnumerable<sbyte> AsEnumerable(this SmallPoint p)
	{
		yield return p.X;
		yield return p.Y;
	}

	
	
	
	public static SmallPoint FromString(string value)
	{
		var parts = value.Split(',');
		sbyte x = Convert.ToSByte(parts.FirstOrDefault());
		sbyte y = Convert.ToSByte(parts.LastOrDefault());
		return new SmallPoint(x, y);
	}

	
	public static SmallPoint Add(this SmallPoint a, SmallPoint b)
	=> new SmallPoint(a.X + b.X, a.Y + b.Y);

	

	public static bool IsAdjacent(this SmallPoint p1, SmallPoint p2)
	{
		var p = p2 - p1;
		return p.X == 0 || p.Y == 0 || (Math.Abs(p.X) == Math.Abs(p.Y));
	}
}
