// http://adventofcode.com/2015/day/3

using System;
using System.IO;
using System.Linq;
using Xunit;
using advent.of.code.y2015.day5;

namespace advent.of.code.tests.y2015 {

/*
ow, a nice string is one with all of the following properties:

It contains a pair of any two letters that appears at least twice in the string without overlapping, like xyxy (xy) or aabcdefgaa (aa), but not like aaa (aa, but it overlaps).
It contains at least one letter which repeats with exactly one letter between them, like xyx, abcdefeghi (efe), or even aaa.
For example:

qjhvhtzxzqqjkmpb is nice because is has a pair that appears twice (qj) and a letter that repeats with exactly one letter between them (zxz).
xxyxx is nice because it has a pair that appears twice and a letter that repeats with one between, even though the letters used by each rule overlap.
uurcxstgmygtbstg is naughty because it has a pair (tg) but no repeat with a single letter between them.
ieodomkazucvgmuy is naughty because it has a repeating letter with one between (odo), but no pair that appears twice.

*/

	public class TestDay5
	{
		[Theory]
		[InlineData("ugknbfddgicrmopn",true)]
		[InlineData("aaa",true)]
		[InlineData("jchzalrnumimnmhp",false)]
		[InlineData("haegwjzuvuyypxyu",false)]
		[InlineData("dvszwmarrgswjxmb",false)]
		public void PartOne(string value, bool isNice)
			=>
				Assert.Equal(isNice,
					StringClassifier.IsNice(value));

		[Theory]
		[InlineData("xyxy")]
		[InlineData("aabcdefgaa")]
		public void TestPair(string value) =>
			Assert.True(StringClassifier.HasPair(value));

		[Theory]
		[InlineData("xyx")]
		[InlineData("abcdefeghi")]
		public void TestSurrounding(string value) =>
			Assert.True(StringClassifier.HasSurounding(value));

		[Theory]
		[InlineData("qjhvhtzxzqqjkmpb",true)]
		[InlineData("xxyxx",true)]
		[InlineData("uurcxstgmygtbstg",false)]
		[InlineData("ieodomkazucvgmuy",false)]
		public void PartTwo(string value, bool isNice)
			=>
				Assert.Equal(isNice,
					StringClassifier.IsNicer(value));

		[Fact]
		public void Puzzle() {
			var input = File
				.ReadLines("tests/y2015/Day5.Input.txt");

			Assert.Equal(255, input
					.Where(StringClassifier.IsNice)
					.Count());
			Assert.Equal(77, input
					.Where(StringClassifier.IsNicer)
					.Count());

		}
	}
}