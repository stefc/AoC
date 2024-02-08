// http://adventofcode.com/2015/day/4

using advent.of.code.y2015.day4;

namespace advent.of.code.tests.y2015;

[Trait("Year", "2015")]
[Trait("Day", "4")]
public class TestDay4 : IPuzzleTest
{
	private readonly IPuzzle _ = new StockingSuffer();

	[Theory]
	[InlineData("abcdef", 609043)]
	[InlineData("pqrstuv", 1048970)]
	public void PartOne(string secret, int expected)
		=> Assert.Equal(expected, _.Silvered(secret));

	[Theory]
	[InlineData("abcdef", 6742839)]
	[InlineData("pqrstuv", 5714438)]
	public void PartTwo(string secret, int expected)
		=> Assert.Equal(expected, _.Golded(secret));
	
	[Fact]
	public void PuzzleSilver() => Assert.Equal(117_946, _.Silvered("ckczppom"));

	[Fact]
	public void PuzzleGold() => Assert.Equal(3_938_038, _.Golded("ckczppom"));
}
