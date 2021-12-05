namespace advent.of.code.common;

public record Line(Point start, Point end);

public static class LineExtensions {

	public static bool IsVertical(this Line line) => line.start.X == line.end.X;  
	public static bool IsHorizontal(this Line line) => line.start.Y == line.end.Y;  
}


