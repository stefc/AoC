using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace advent.of.code.common
{
    public class Point : IEquatable<Point>
    {

        private static Lazy<Point> zero = new Lazy<Point>(() => new Point(0, 0));
        private static Lazy<Point> north = new Lazy<Point>( () => new Point(0,-1));
        private static Lazy<Point> south = new Lazy<Point>( () => new Point(0,+1));
        private static Lazy<Point> west = new Lazy<Point>( () => new Point(-1,0));
        private static Lazy<Point> east = new Lazy<Point>( () => new Point(+1,0));


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

        public override string ToString() => $"({X},{Y})";

        public static Point operator +(Point a, Point b) => a.Add(b);

        public static Point operator -(Point a, Point b) => a.Sub(b);

        public static float operator ^(Point a, Point b) => a.Dotproduct(b);
        public static Point operator *(Point a, int scalar)
        => new Point(a.X*scalar,a.Y*scalar);

        public static Point operator *(Point a, float scalar)
        => new Point((int)(a.X*scalar),(int)(a.Y*scalar));
    }

    public static class PointExtensions {
        public static Point Add(this Point a, Point b) 
        => new Point(a.X + b.X, a.Y + b.Y);

        public static Point Sub(this Point a, Point b)
        => new Point(a.X - b.X, a.Y - b.Y);

        public static int ManhattenDistance(this Point point)
        => Math.Abs(point.X) + Math.Abs(point.Y);

        public static Point Scale(this Point point, int factor) 
        => new Point(point.X * factor, point.Y * factor); 

        public static float Dotproduct(this Point a, Point b) 
        => (float)(a.X*b.X + a.Y*b.Y); 

        public static Point ToPoint(this string s) {
            var parts = s.Split(',').Select( p => Convert.ToInt32(p));
            return new Point(parts.First(), parts.Last());
        }

        public static IEnumerable<int> AsEnumerable(this Point p) 
        {
            yield return p.X;
            yield return p.Y;
        }

        public static Point RotateRight(this Point p) 
        => p.Rotate( new float[,]{{0,-1},{1,0}});
        public static Point RotateLeft(this Point p) 
        => p.Rotate( new float[,]{{0,1},{-1,0}});

        private static Point Rotate(this Point p, float[,] transform) {

            var M = Matrix<float>.Build;
            var V = Vector<float>.Build;

            var m = M.DenseOfArray(transform); 
            var v = V.Dense(p.AsEnumerable().Select(Convert.ToSingle).ToArray());
            var v_ = (m * v)
                .Select(Convert.ToInt32)
                .ToArray();

            return new Point(v_[0],v_[1]);
        }

    }
}