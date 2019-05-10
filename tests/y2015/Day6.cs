// http://adventofcode.com/2015/day/6

using System;
using System.IO;
using System.Linq;
using Xunit;
using advent.of.code.y2015.day6;

namespace advent.of.code.tests.y2015 {

	public class TestDay6
	{
		[Theory]
		[InlineData("turn on 0,0 through 999,999", FireHazard.Command.TurnOn, 0, 0, 999, 999)]
		[InlineData("toggle 0,0 through 999,0", FireHazard.Command.Toggle, 0, 0, 999, 0)]
		[InlineData("turn off 499,499 through 500,500", FireHazard.Command.TurnOff, 499, 499, 500, 500)]
		internal void TurnOnStatementParse(string code, FireHazard.Command cmd, int fromX, int fromY, int throughX, int throughY)
		{
			var stmt = FireHazard.Statement.FromString(code);

			Assert.Equal(cmd, stmt.Command);
			Assert.Equal(fromX, stmt.From.X);
			Assert.Equal(fromY, stmt.From.Y);
			Assert.Equal(throughX, stmt.Through.X);
			Assert.Equal(throughY, stmt.Through.Y);
		}


		[Theory]
		[InlineData("ugknbfddgicrmopn",true)]
		public void PartOne(string value, bool isNice) {}
/*
		[Theory]
		[InlineData("qjhvhtzxzqqjkmpb",true)]
		public void PartTwo(string value, bool isNice)
			=>
				Assert.Equal(isNice,
					FireHazard.IsNicer(value));
 */
		[Fact]
		public void Puzzle() {
			var input = File
				.ReadLines("tests/y2015/Day6.Input.txt");
		}
	}
}