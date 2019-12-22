
using System.IO;
using System.Collections.Immutable;
using System.Collections.Generic;
using System.Linq;

using Xunit;

using advent.of.code.y2019.day12;

namespace advent.of.code.tests.y2019
{


    [Trait("Year", "2019")]
    public class TestDay12
    {
        [Fact]
        public void TestPointConvert()
        {
            var point = @"<x=-1, y=0, z=2>".ToPoint3D();
            Assert.Equal(new Point3D(-1, 0, 2), point.GetOrElse(Point3D.Zero));
        }

        [Fact]
        public void TestMoonSample()
        {
            var moons = GetSample(1);
            Assert.Equal(4, moons.Count());
        }

        [Theory]
        [InlineData(1, 10, 179)]
        [InlineData(2, 100, 1940)]
        public void TestGravity(int sample, int steps, int expected)
        {
            var state = GetSample(sample)
                .Select(moon => (pos: moon, vel: Point3D.Zero));

            state = Enumerable
                .Range(0, steps)
                .Aggregate(state, (accu, _) => accu.Simulation());

            var actual = state.ToImmutableDictionary(x => x.pos, x => x.vel);

            var energy = state.EnergyOf();

            Assert.Equal(expected, energy);
        }

        [Theory]
        [InlineData(2772,1)]
        [InlineData(4686774924,2)]
        public void LookForCycle(long expected, int sample)
        {
            var state = GetSample(sample)
                .Select(moon => (pos: moon, vel: Point3D.Zero));

            var xCyle = state.FindCycle( p => p.X);
            var yCyle = state.FindCycle( p => p.Y);
            var zCyle = state.FindCycle( p => p.Z);

           
            var actual = NBodyProblem.lcm(
                NBodyProblem.lcm(xCyle,yCyle),
                    zCyle);
            Assert.Equal(expected, actual);
        }

        
        [Fact]
        public void PuzzleOne()
        {
            var input = File.ReadLines("tests/y2019/Day12.Input.txt")
                .GetMoonPositions()
                .Select(moon => (pos: moon, vel: Point3D.Zero));

            var actual = Enumerable
                .Range(0, 1000)
                .Aggregate(input, (accu, _) => accu.Simulation())
                .EnergyOf();
            Assert.Equal(6678, actual);
        }
        [Fact]
        public void PuzzleTwo()
        {
            var input = File.ReadLines("tests/y2019/Day12.Input.txt")
                .GetMoonPositions()
                .Select(moon => (pos: moon, vel: Point3D.Zero));

            var xCyle = input.FindCycle( p => p.X);
            var yCyle = input.FindCycle( p => p.Y);
            var zCyle = input.FindCycle( p => p.Z);

           
            var actual = NBodyProblem.lcm(
                NBodyProblem.lcm(xCyle,yCyle),
                    zCyle);
            Assert.Equal(496734501382552, actual);
        }

        private IEnumerable<Point3D> GetSample(int variante)
        {
            if (variante == 1)
            {
                return new string[]{
                    "<x=-1, y=0, z=2>",
                    "<x=2, y=-10, z=-7>",
                    "<x=4, y=-8, z=8>",
                    "<x=3, y=5, z=-1>"
                }.GetMoonPositions();
            }
            else
            {
                return new string[]{
                    "<x=-8, y=-10, z=0>",
                    "<x=5, y=5, z=10>",
                    "<x=2, y=-7, z=3>",
                    "<x=9, y=-8, z=-3>"
                }.GetMoonPositions();
            }
        }
    }
}