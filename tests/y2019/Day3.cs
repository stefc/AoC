
using System.Linq;
using System.IO;

using Xunit;

using advent.of.code.common;
using advent.of.code.y2019.day3;

namespace advent.of.code.tests.y2019
{
    [Trait("Year", "2019")]
    public class TestDay3
	{

		[Fact]
		public void TestPath() {
			var path = "R8,U5,L5,D3".ToPath();

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
				.Select(PointExtensions.ManhattenDistance)
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
		public void TestOnLine()
		{
			var line1 = (new Point(2,1), new Point(8,1));
			Assert.True(line1.IsOnLine(new Point(4,1)));
			Assert.False(line1.IsOnLine(new Point(2,1)));
			Assert.False(line1.IsOnLine(new Point(1,1)));
			Assert.False(line1.IsOnLine(new Point(9,1)));
			Assert.False(line1.IsOnLine(new Point(8,1)));

			var line2 = (new Point(10,0), new Point(10,5));
			Assert.True(line2.IsOnLine(new Point(10,3)));
			Assert.False(line2.IsOnLine(new Point(10,0)));
			Assert.False(line2.IsOnLine(new Point(10,-1)));
			Assert.False(line2.IsOnLine(new Point(10,6)));
			Assert.False(line2.IsOnLine(new Point(10,5)));
		}

		[Theory]
		[InlineData("R8,U5,L5,D3",3,3,20)]
		[InlineData("U7,R6,D4,L4",3,3,20)]
		[InlineData("R8,U5,L5,D3",6,5,15)]
		[InlineData("U7,R6,D4,L4",6,5,15)]
		public void TestGetSteps(string path, int x, int y, int expected)
		{
			var actual = path
				.ToPath()
				.ToLines()
				.Steps(new Point(x,y));
			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData(30, "R8,U5,L5,D3", "U7,R6,D4,L4")]
		[InlineData( 610,
			"R75,D30,R83,U83,L12,D49,R71,U7,L72",
			"U62,R66,U55,R34,D71,R55,D58,R83")]
		[InlineData( 410,
			"R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51",
			"U98,R91,D20,R16,D67,R40,U7,R15,U6,R7")]
		public void FindNearestSteps(int expected, string a, string b)
		{
			var actual = CrossedWires.FindStepsCrossings(a,b);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void PuzzleOne() {
			string input = File.ReadAllText("tests/y2019/Day3.Input.txt");
			var wires = input.Split('\n');
			var actual = CrossedWires
				.FindDistanceCrossings(wires.First(),wires.Last());
			Assert.Equal(227, actual);
		}

		[Fact]
		public void PuzzleTwo() {
			string input = File.ReadAllText("tests/y2019/Day3.Input.txt");
			var wires = input.Split('\n');
			var actual = CrossedWires
				.FindStepsCrossings(wires.First(),wires.Last());
			Assert.Equal(20286, actual);
		}
	}
}