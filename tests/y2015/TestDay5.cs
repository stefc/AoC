// http://adventofcode.com/2015/day/5

using advent.of.code.y2015.day5;

namespace advent.of.code.tests.y2015;

[Trait("Year", "2015")]
[Trait("Day", "5")]
public class TestDay5 : IPuzzleTest
{
	private readonly IPuzzle _ = new StringClassifier();

	public TestDay5()
	{
		this.input = _.ReadPuzzle();
	}

	private readonly IEnumerable<string> input;

	[Fact]
	public void PuzzleGold()
	{
		Assert.Equal(55, _.Gold(this.input));
	}

	[Fact]
	public void PuzzleSilver()
	{
		Assert.Equal(255, _.Silver(this.input));
	}
}
