// http://adventofcode.com/2015/day/4

using advent.of.code.y2015.day4;

namespace advent.of.code.tests.y2015;

[Trait("Year", "2015")]
[Trait("Day", "4")]
public class TestDay4
{
	[Theory]
	[InlineData("abcdef", 609043)]
	[InlineData("pqrstuv", 1048970)]
	public void PartOne(string secret, int expected)
		=>
			Assert.Equal(expected,
				StockingSuffer.FindLowestNumber(secret));

	[Theory(Skip = "Longrunner")]
	[InlineData("abcdef", 6742839)]
	[InlineData("pqrstuv", 5714438)]
	public void PartTwo(string secret, int expected)
		=>
			Assert.Equal(expected,
				StockingSuffer.FindLowestNumber(secret, prefix: 6));

	[Fact(Skip = "Longrunner")]
	public void Puzzle()
	{
		Assert.Equal(117946,
				StockingSuffer.FindLowestNumber("ckczppom"));
		Assert.Equal(3938038,
				StockingSuffer.FindLowestNumber("ckczppom", prefix: 6));
	}
}
