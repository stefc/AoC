
// http://adventofcode.com/2015/day/3

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace advent.of.code.y2015.day3 {

    static class SphericalHouses {
        
        public static int AtLeastOnePresent(string instructions)  => GetVisits(instructions).Count;

        public static int TogetherWithRobodog(string instructions) {
            var result = instructions
                .Select( (instruction, index) => new {instruction, isSanta = index % 2 == 0})
                .GroupBy( x => x.isSanta, x => x.instruction)
                .Aggregate( 
                    seed: ImmutableHashSet<Point>.Empty,
                    func: (accu,current) => accu.Union(GetVisits(string.Concat(current))),
                    resultSelector: accu => accu.Count);
            return result;
        } 

        private static ISet<Point> GetVisits(string instructions) {
            var initialResult = ImmutableHashSet<Point>.Empty.Add(Point.Zero);
            return instructions
                .Select(InstructionToPoint)
                .Aggregate(
                    seed: (location: Point.Zero, result: initialResult),
                    func: (accu, current) => {
                        var newLocation = accu.location.Add(current);
                        var newResult = accu.result.Add(newLocation);
                        return (location: newLocation, result: newResult);
                    },
                    resultSelector: accu => accu.result
                );
        } 
        private static Point InstructionToPoint(char instruction) {
            if (instruction == '<')
                return Point.West;
            else if (instruction == '>')
                return Point.East;
            else if (instruction == 'v')
                return Point.South;
            else if (instruction == '^')
                return Point.North;
            throw new ArgumentException();
        }
    }

    public class Point : IEquatable<Point> {

        private static Lazy<Point> zero = new Lazy<Point>( () => new Point(0,0));
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

        public override int GetHashCode() {
            return this.X * 26 ^ this.Y;
        }

        public bool Equals(Point other)
        {
            if (other == null)
                return false;
            return other.X == this.X && other.Y == this.Y;
        }
    }

    public static class ExtensionMethods {
        public static Point Add(this Point lhs, Point rhs) => 
            new Point(lhs.X + rhs.X, lhs.Y + rhs.Y);
    }
}