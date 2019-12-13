// http://adventofcode.com/2019/day/10

// https://de.wikipedia.org/wiki/Bresenham-Algorithmus

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;


using advent.of.code;
using advent.of.code.common;

namespace advent.of.code.y2019.day10
{

    using Line = ValueTuple<Point, Point>;

    using static F;
    using System.Diagnostics.CodeAnalysis;

    public class LineComparer : IEqualityComparer<(Point start, Point end)>
    {
        public bool Equals(
            (Point start, Point end) a, (Point start, Point end) b)
        {
            return
            (a.start.Equals(b.start) && a.end.Equals(b.end)) ||
            (a.start.Equals(b.end) && a.end.Equals(b.start));
        }

        public int GetHashCode([DisallowNull] (Point start, Point end) line)
        {
            return line.start.GetHashCode() ^ line.end.GetHashCode();
        }

        public static LineComparer Default = new LineComparer();
    }

    public static class ExtensionMethods
    {
        public static Point PointInTheMiddle(this (Point p1, Point p2) l)
        => new Point((l.p1.X + l.p2.X) / 2, (l.p1.Y + l.p2.Y) / 2);

        public static double Distance(this (Point p1, Point p2) l)
        => Math.Sqrt(
            Math.Pow(l.p2.X - l.p1.X, 2) +
            Math.Pow(l.p2.Y - l.p1.Y, 2));

        public static bool InCircle(this Point p, Point c, double r) 
        => ((c - p) ^ (c-p)) <= (r*r);  

        public static (Point start, Point end) ToLine(this string s)
        {
            var parts = s.Split(':');
            return (parts.First().ToPoint(), parts.Last().ToPoint());
        }

        public static IEnumerable<Point> AsEnumerable(
            this (Point source, Point target) connection)
        {
            yield return connection.source;
            yield return connection.target;
        }

        public static IEnumerable<Point> Except(this IEnumerable<Point> points,
            Line connection)
        => points.Except(connection.AsEnumerable());
    }

    public static class MonitorStation
    {

        public static IEnumerable<Point> GetAsteroidsMap(
            this IEnumerable<string> lines)
        => lines
                .SelectMany((line, y) =>
                   line.Select((ch, x) =>
                      ch != '.' ? Some(new Point(x, y)) : None))
                .Bind(x => x)
                .ToImmutableArray();

        public static IEnumerable<Line> GetInterconnections(
            this IEnumerable<Point> points)
        => (from source in points
           from target in points
           where source != target
           select (start: source, end: target))
           .Distinct(LineComparer.Default);

        public static IEnumerable<Point> GetLinesOfSight(
            this IEnumerable<Line> connections, IEnumerable<Point> asteroids)
        => connections
            .Where( link => !asteroids
                .Except(link)
                .Except(
                    from a in asteroids
                    where !a.InCircle(link.PointInTheMiddle(), link.Distance()/2) 
                    select a 
                ).Any( target => target.Hittest(link)))
            .SelectMany( link => link.AsEnumerable());

        public static Point OrthogonalProjection(Point p1, Point p2, Point p3)
        {
            var b = p2 - p1;
            var a = p3 - p1;
            return b * ((a ^ b) / (b ^ b)); // b x (a∙b / b∙b)
        }

        // https://www.xarg.org/book/computer-graphics/2d-hittest/
        public static bool Hittest(this Point p3, (Point p1, Point p2) link)
        => Point.Zero.Equals(
            p3 - link.p1 - MonitorStation.OrthogonalProjection(link.p1, link.p2, p3));
    }
}