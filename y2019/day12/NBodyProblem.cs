// http://adventofcode.com/2019/day/12


using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;


using advent.of.code;
using advent.of.code.common;
using System.Text.RegularExpressions;

namespace advent.of.code.y2019.day12
{
    using static F;

    using Line = ValueTuple<Point3D, Point3D>;


    public static class ExtensionMethods
    {
        public static IEnumerable<Point3D> GetMoonPositions(
            this IEnumerable<string> lines)
        => lines.Select( line => line.ToPoint3D()).Bind( x => x); 

        public static IEnumerable<Line> ToConnections(
            this IEnumerable<Point3D> moons)
        => (from a in moons
           from b in moons 
           select (a,b))
           .Distinct(new ConnectionComparer())
           .Where( x => !x.Item1.Equals(x.Item2));

        public static IEnumerable<(Point3D pos, Point3D vel)> CalcVelocity(this 
            (Point3D start, Point3D end) connection)
        {
            var vel = new Point3D(
                connection.start.X.CompareTo(connection.end.X),
                connection.start.Y.CompareTo(connection.end.Y),
                connection.start.Z.CompareTo(connection.end.Z));

            yield return (pos: connection.start, vel: !vel);
            yield return (pos: connection.end, vel: vel);
        }

    }


    public static class NBodyProblem
    {

        public static IEnumerable<(Point3D pos, Point3D vel)> Simulation(
            this IEnumerable<(Point3D pos, Point3D vel)> state)
        {
            var lookUpVelocity = state
                .ToImmutableDictionary( x => x.pos, x => x.vel);

            var movements = state
                .Select( moon => moon.pos)
                .ToConnections()
                .SelectMany( connection => connection.CalcVelocity())
                .GroupBy( moon => moon.pos, moon => moon.vel)
                .Select( grp => (pos: grp.Key, 
                    vel: grp.Aggregate(lookUpVelocity[grp.Key],
                        (accu,current) => accu+current)))
                .Select( x => (pos:x.pos+x.vel, vel: x.vel))
                .ToList();
            return movements;
        }

        public static int EnergyOf(this IEnumerable<(Point3D pos, Point3D vel)> state)
        => state
            .Select(x => 
                (x.pos.AsEnumerable().Select(x => Math.Abs(x)).Sum()) * 
                (x.vel.AsEnumerable().Select(x => Math.Abs(x)).Sum()))
            .Sum();
    }
}