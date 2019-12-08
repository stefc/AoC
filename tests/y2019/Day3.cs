using Xunit;

using advent.of.code.y2019.day3;
using System.Linq;
using System.IO;
using System.Collections.Immutable;
using System;

namespace advent.of.code.tests.y2019
{

	[Trait("Year", "2019")]
    public class TestDay3
	{

		[Fact]
		public void TestPath() {
			var path = CrossedWires.ToPath("R8,U5,L5,D3");

			Assert.Equal(5, path.Count());
			Assert.Equal(new Point(0,0), path.ElementAt(0));
			Assert.Equal(new Point(8,0), path.ElementAt(1));
			Assert.Equal(new Point(8,5), path.ElementAt(2));
			Assert.Equal(new Point(8-5,5), path.ElementAt(3));
			Assert.Equal(new Point(8-5,5-3), path.ElementAt(4));
		}

		[Fact]
		public void TestCrossed() {

			var crossed = CrossedWires.FindCrossings(
				"R8,U5,L5,D3", "U7,R6,D4,L4");
			Assert.Equal( 2, crossed.Count());
			Assert.Contains(new Point(3,3), crossed);
			Assert.Contains(new Point(6,5), crossed);
		}

		[Theory]
		[InlineData(6, "R8,U5,L5,D3", "U7,R6,D4,L4")]
		[InlineData( 159,
			"R75,D30,R83,U83,L12,D49,R71,U7,L72",
			"U62,R66,U55,R34,D71,R55,D58,R83")]
		[InlineData( 135,
			"R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51",
			"U98,R91,D20,R16,D67,R40,U7,R15,U6,R7")]
		public void FindNearestDistance(int expected, string a, string b)
		{
			var actual = CrossedWires.FindCrossings(a,b)
				.Select(ExtensionMethods.ManhattenDistance)
				.Min();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void TestCrossing() {
			var AB = (new Point(2,1), new Point(8,1));
			var CD = (new Point(10,0), new Point(10,3));
			var EF = (new Point(5,0), new Point(5,4));
			var AB_CD = CrossedWires.Crossing(AB, CD);
			Assert.False(AB_CD.IsSome());

			var AB_EF = CrossedWires.Crossing(AB, EF);
			Assert.True(AB_EF.IsSome());
			Assert.Equal(new Point(5,1), AB_EF.GetOrElse(Point.Zero));
		}

		[Fact]
		public void PuzzleOne() {
			string input = File.ReadAllText("tests/y2019/Day3.Input.txt");
			var wires = input.Split('\n');
			var actual = CrossedWires.FindCrossings(wires.First(),wires.Last())
				.Select(ExtensionMethods.ManhattenDistance)
				.Min();
			Assert.Equal(227, actual);
		}





	}
}