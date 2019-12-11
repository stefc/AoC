
// http://adventofcode.com/2015/day/3

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using advent.of.code.common;

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
}