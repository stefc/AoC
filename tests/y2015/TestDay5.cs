// http://adventofcode.com/2015/day/5

using advent.of.code.y2015.day5;

namespace advent.of.code.tests.y2015;

[Trait("Year", "2015")]
[Trait("Day", "5")]
public class TestDay5
{
	private readonly IPuzzle _ = new StringClassifier();

	[Fact]
	public void Puzzle()
	{
		var input = _.ReadPuzzle();

		Assert.Equal(255, _.Silver(input));

		Assert.Equal(55, _.Gold(input));
		
	}
}
