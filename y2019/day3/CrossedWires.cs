// http://adventofcode.com/2019/day/3

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;

using advent.of.code;
using advent.of.code.common;

namespace advent.of.code.y2019.day3
{
    using static F;

    using Line = ValueTuple<Point, Point>;

    using Computation =
        StatefulComputation<IEnumerator<(Point start, Point end)>, int>;

    public readonly struct Direction
    {
        public readonly char Code;
        public readonly int Length;
        private Direction(char code, int length)
            => (Code, Length) = (code, length);
        public Direction(string stmt) => (Code, Length) = FromString(stmt);

        public override string ToString() => $"{Code}{Length}";

        private static (char, int) FromString(string stmt)
        {
            var ch = char.ToUpper(stmt.FirstOrDefault());
            if (int.TryParse(new String(stmt.Skip(1).ToArray()), out var len))
            {
                return (char.ToUpper(ch), len);
            };
            return (ch, 0);
        }

        public Point ToPoint()
        {
            switch (Code)
            {
                case 'R': return new Point(+Length, 0);
                case 'L': return new Point(-Length, 0);
                case 'U': return new Point(0, +Length);
                case 'D': return new Point(0, -Length);
                default: return Point.Zero;
            }
        }
    }

    

    public static class ExtensionMethods
    {
        public static Point Move(this Point lhs, string direction)
        {
            var rhs = new Direction(direction).ToPoint();
            return new Point(lhs.X + rhs.X, lhs.Y + rhs.Y);
        }

       
        public static int Steps(this (Point start, Point end) line)
        => Math.Abs(line.start.X-line.end.X) + 
            Math.Abs(line.start.Y-line.end.Y);

        public static bool Between(this (Point start, Point end) line,
            int value, Func<Point, int> selector)
        => (value > Math.Min(selector(line.start), selector(line.end))
            && value < Math.Max(selector(line.start), selector(line.end)));

        public static int Delta(this (Point start, Point end) line,
            Func<Point, int> selector)
        => Math.Abs(selector(line.start) - selector(line.end));

        public static IEnumerable<Point> ToPath(this string wire)
        => wire.ToSegments()
            .Aggregate(ImmutableList<Point>.Empty.Add(Point.Zero),
            (accu, current) => accu.Add(accu.Last().Move(current)));

        public static IEnumerable<Line> ToLines(this IEnumerable<Point> path)
        => path
            .Zip(path.Skip(1), (current, next) => (current, next))
            .ToList();


        public static bool XBetween(this (Point start, Point end) line, int x)
        => line.Between(x, pt => pt.X);
        public static bool YBetween(this (Point start, Point end) line, int y)
        => line.Between(y, pt => pt.Y);

        public static bool IsOnLine(this (Point start, Point end) line, Point point)
        => (
            (
                (line.start.X == line.end.X) &&
                (line.start.X == point.X && line.YBetween(point.Y))
            )
            ||
               (
                (line.start.Y == line.end.Y) &&
                (line.start.Y == point.Y && line.XBetween(point.X))
            )
        );
        private static (Point, Point) ReplaceEnd(this (Point start, Point) line,
            Point point) => (line.start, point);

        public static IEnumerable<Line> TakeUntilPointFound(
            this IEnumerable<Line> lines, Point point)
        {
            foreach (var line in lines)
            {
                bool found = line.IsOnLine(point);
                if (found)
                {
                    yield return line.ReplaceEnd(point);
                    yield break;
                }
                else
                {
                    yield return line;
                }
            }
        }
    }

    public static class CrossedWires
    {
        public static Option<Point> Crossing(
            (Point start, Point end) a, (Point start, Point end) b)
        {
            Func<Point,int> XSelector = pt => pt.X;
            Func<Point,int> YSelector = pt => pt.Y;

            var cx = a.Delta( XSelector);
            var cy = a.Delta( YSelector);

            var dx = b.Delta( XSelector);
            var dy = b.Delta( YSelector);

            if (cx == 0 && dx != 0)
            {
                var ax = XSelector(a.start);  // ax ist fix, bx ist variabel
                var by = YSelector(b.start);  // by ist fix, ay ist variabel
                if (b.Between(ax, XSelector) && a.Between(by,YSelector))
                {
                    return Some(new Point(ax, by));
                }
            }
            else if (cx != 0 && dx == 0)
            {
                var bx = XSelector(b.start);  // bx ist fix, ax ist variabel
                var ay = YSelector(a.start);  // ay ist fix, by ist variabel
                if (a.Between(bx, XSelector) && b.Between(ay,YSelector))
                {
                    return Some(new Point(bx, ay));
                }
            }
            return None;
        }

        public static IEnumerable<Point> FindCrossings(string wire1, string wire2)
       => (from a in wire1.ToPath().ToLines()
           from b in wire2.ToPath().ToLines()
           select Crossing(a, b))
           .Bind(x => x);

        public static int FindDistanceCrossings(string wire1, string wire2)
        => FindCrossings(wire1, wire2)
            .Select(PointExtensions.ManhattenDistance)
            .Min();

        public static int Steps(this IEnumerable<Line> path, Point point)
        {
            var lines = path.TakeUntilPointFound(point);
            var computer = CrossedWires.GetSteps(point);
            return computer(lines.GetEnumerator()).Value;
        }

        public static Computation GetSteps(Point point)
        => enumerator =>
        {
            bool hasNext = enumerator.MoveNext();
            return (Value: hasNext ?
                enumerator.Current.Steps()  + GetSteps(point)(enumerator).Value : 0,
                State: enumerator);
        };

      

       

        public static int FindStepsCrossings(string wire1, string wire2)
        {
            var aLines = wire1.ToPath().ToLines();
            var bLines = wire2.ToPath().ToLines();
            return (from a in aLines
                    from b in bLines
                    from crossed in Crossing(a, b)
                    select Steps(aLines, crossed) + Steps(bLines, crossed))
                         .Min();
        }
    }
}