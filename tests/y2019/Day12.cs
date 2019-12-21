
using System;
using System.IO;
using System.Collections.Immutable;
using System.Collections.Generic;
using System.Linq;

using Xunit;

// using advent.of.code.y2019.day12;
using advent.of.code.common;
using System.Text.RegularExpressions;
using advent.of.code.y2019.day12;

namespace advent.of.code.tests.y2019
{
	using static F;


	[Trait("Year", "2019")]
    public class TestDay12
    {
		[Fact]
		public void TestPointConvert()
		{
			var point = @"<x=-1, y=0, z=2>".ToPoint3D();
			Assert.Equal(new Point3D(-1,0,2), point.GetOrElse(Point3D.Zero));
		}

		[Fact]
		public void TestMoonSample() {
			var moons = GetSample();
			Assert.Equal(4, moons.Count());
		}

		[Fact]
		public void TestGravity() 
		{
			var state = GetSample()
				.Select( moon => (pos: moon, vel: Point3D.Zero));

			var connections = state.Select( moon => moon.pos).ToConnections();

			Assert.Equal( (3*2*1)*2, connections.Count());
			
			
		}

		[Fact]
		public void PuzzleOne() {

		}
		[Fact]
		public void PuzzleTwo() {

		}

		private IEnumerable<Point3D> GetSample()
		=> new string[]{
"<x=-1, y=0, z=2>",
"<x=2, y=-10, z=-7>",
"<x=4, y=-8, z=8>",
"<x=3, y=5, z=-1>"
			}.GetMoonPositions();

    }
}