using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace advent.of.code.y2019.day12
{
    using static F;
    public class Point3D : IEquatable<Point3D>
    {

        private static Lazy<Point3D> zero = new Lazy<Point3D>(() => new Point3D(0, 0, 0));

        public static Point3D Zero => zero.Value;

        public int X { get; private set; }

        public int Y { get; private set; }
        public int Z { get; private set; }

        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override int GetHashCode()
        {
            return this.X * 26 ^ this.Y * 13 ^ this.Z;
        }

        public bool Equals(Point3D other)
        {
            if (other == null)
                return false;
            return other.X == this.X && other.Y == this.Y && other.Z == this.Z;
        }

        public override string ToString() => $"({X},{Y},{Z})";

        public static Point3D operator +(Point3D a, Point3D b) => a.Add(b);

        public static Point3D operator -(Point3D a, Point3D b) => a.Sub(b);

        public static Point3D operator *(Point3D a, int scalar)
        => new Point3D(a.X*scalar,a.Y*scalar,a.Z*scalar);

        public static Point3D operator !(Point3D a) 
        => a * -1;


        // public static float operator ^(Point a, Point b) => a.Dotproduct(b);

    //     public static Point operator *(Point a, float scalar)
    //     => new Point((int)(a.X*scalar),(int)(a.Y*scalar));
    }

    public static class Point3DExtensions {
        public static Point3D Add(this Point3D a, Point3D b) 
        => new Point3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        public static Point3D Sub(this Point3D a, Point3D b)
        => new Point3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        public static int ManhattenDistance(this Point3D point)
        => Math.Abs(point.X) + Math.Abs(point.Y) + Math.Abs(point.Z);

        public static Point3D Scale(this Point3D point, int factor) 
        => new Point3D(point.X * factor, point.Y * factor, point.Z * factor); 

        public static float Dotproduct(this Point3D a, Point3D b) 
        => (float)(a.X*b.X + a.Y*b.Y); 

        public static IEnumerable<int> AsEnumerable(this Point3D p) 
        {
            yield return p.X;
            yield return p.Y;
            yield return p.Z;
        }

        public static Option<Point3D> ToPoint3D(this string input) {
            string pattern = 
                @"^<x=([-|+]?\d*)\s?,\s?y=([-|+]?\d*)\s?,\s?z=([-|+]?\d*)>$";
        	
        	Match m = Regex.Match(input, pattern);
			if (m.Success)
			{
				var values = m.Groups.Values
					.Skip(1)
                    .Select(g => g.Value)
                    .Select(v => Convert.ToInt32(v))
                    .ToArray();

				return Some(new Point3D( values[0], values[1], values[2]));
			}
            return None;
         }
    }


}