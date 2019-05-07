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