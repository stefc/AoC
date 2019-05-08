

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace advent.of.code.common
{
	public class Point : IEquatable<Point>
	{

		private static Lazy<Point> zero = new Lazy<Point>(() => new Point(0, 0));
		private static Lazy<Point> north = new Lazy<Point>(() => new Point(0, -1));
		private static Lazy<Point> south = new Lazy<Point>(() => new Point(0, +1));
		private static Lazy<Point> west = new Lazy<Point>(() => new Point(-1, 0));
		private static Lazy<Point> east = new Lazy<Point>(() => new Point(+1, 0));

		public static Point Zero => zero.Value;
		public static Point North => north.Value;
		public static Point South => south.Value;
		public static Point West => west.Value;
		public static Point East => east.Value;

		public int X { get; private set; }

		public int Y { get; private set; }

		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}

		public override int GetHashCode()
		{
			return this.X * 26 ^ this.Y;
		}

		public bool Equals(Point other)
		{
			if (other == null)
				return false;
			return other.X == this.X && other.Y == this.Y;
		}
	}


	public static class ExtensionMethods
	{
		public static Point Add(this Point lhs, Point rhs) =>
			new Point(lhs.X + rhs.X, lhs.Y + rhs.Y);
	}

}