

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace advent.of.code.common
{
	[DebuggerDisplay("{debugDescription,nq}")]
	public class Point : IEquatable<Point>
	{

		private static Lazy<Point> zero = new Lazy<Point>(() => new Point(0, 0));
		private static Lazy<Point> north = new Lazy<Point>(() => new Point(0, -1));
		private static Lazy<Point> south = new Lazy<Point>(() => new Point(0, +1));
		private static Lazy<Point> west = new Lazy<Point>(() => new Point(-1, 0));
		private static Lazy<Point> east = new Lazy<Point>(() => new Point(+1, 0));
		private string value;

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

		public static Point FromString(string value)
		{
			var parts = value.Split(',');
			int x = Convert.ToInt32(parts.FirstOrDefault());
			int y = Convert.ToInt32(parts.LastOrDefault());
			return new Point(x, y);
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

		public int SquareLength => Math.Abs(this.X * this.X) + Math.Abs(this.Y * this.Y);
		public double Length => Math.Sqrt((this.X * this.X) + (this.Y * this.Y));

		public static Point operator +(Point a, Point b) => a.Add(b);

		public static Point operator -(Point a, Point b) => a.Sub(b);

		public static float operator ^(Point a, Point b) => a.Dotproduct(b);
		public static Point operator *(Point a, int scalar)
		=> new Point(a.X * scalar, a.Y * scalar);

		public static Point operator *(Point a, float scalar)
		=> new Point((int)(a.X * scalar), (int)(a.Y * scalar));

		private string debugDescription => $"x={this.X},y={this.Y}";
	}

	public static class PointExtensions
	{
		public static Point Add(this Point a, Point b)
		=> new Point(a.X + b.X, a.Y + b.Y);

		public static Point Sub(this Point a, Point b)
		=> new Point(a.X - b.X, a.Y - b.Y);

		public static int ManhattenDistance(this Point point)
		=> Math.Abs(point.X) + Math.Abs(point.Y);

		public static Point Scale(this Point point, int factor)
		=> new Point(point.X * factor, point.Y * factor);

		public static float Dotproduct(this Point a, Point b)
		=> (float)(a.X * b.X + a.Y * b.Y);

		public static Point ToPoint(this string s)
		{
			var parts = s.Split(',').Select(p => Convert.ToInt32(p));
			return new Point(parts.First(), parts.Last());
		}

		public static IEnumerable<int> AsEnumerable(this Point p)
		{
			yield return p.X;
			yield return p.Y;
		}

		public static IEnumerable<Point> AsEnumerable(this (Point start, Point end) line)
		{
			yield return line.start;
			yield return line.end;
		}

		public static Point RotateRight(this Point p, int count = 1)
		=> Enumerable.Range(1, count).Aggregate(p,
		 	(acc, _) => acc.Rotate(new float[,] { { 0, -1 }, { 1, 0 } }));
		public static Point RotateLeft(this Point p, int count = 1)
		=> Enumerable.Range(1, count).Aggregate(p,
		 	(acc, _) => acc.Rotate(new float[,] { { 0, 1 }, { -1, 0 } }));

		private static Point Rotate(this Point p, float[,] transform)
		{

			var M = Matrix<float>.Build;
			var V = MathNet.Numerics.LinearAlgebra.Vector<float>.Build;

			var m = M.DenseOfArray(transform);
			var v = V.Dense(p.AsEnumerable().Select(Convert.ToSingle).ToArray());
			var v_ = (m * v)
				.Select(Convert.ToInt32)
				.ToArray();

			return new Point(v_[0], v_[1]);
		}


		// https://www.xarg.org/book/computer-graphics/2d-hittest/
		public static bool HitTest(this Point p3, (Point p1, Point p2) link)
		{
			const double ϵ = 0.9999;

			var b = link.p2 - link.p1;
			var a = p3 - link.p1;
			var ab = a ^ b;
			var bb = b ^ b;
			var p = link.p1 + (b * (ab / bb));
			var c = p - link.p1;
			var m = (a - c).Length;

			return ((0f <= ab) && (ab <= bb)) && (m <= ϵ);
		}

	}


}
