
using advent.of.code.y2015.day1;

namespace advent.of.code.tests.y2015;

[Trait("Year", "2015")]
[Trait("Day", "1")]
public class TestDay1
{
	private readonly IPuzzle _ = new NotQuiteLisp();

	[Theory]
	[InlineData("(())", 0)]
	[InlineData("()()", 0)]
	[InlineData("(((", 3)]
	[InlineData("(()(()(", 3)]
	[InlineData("))(((((", 3)]
	[InlineData("())", -1)]
	[InlineData("))(", -1)]
	[InlineData(")())())", -3)]
	[InlineData(")))", -3)]
	public void PartOne(string instructions, int expectedFloor)
		=> Assert.Equal(expectedFloor, _.Silvered(instructions));

	[Theory]
	[InlineData(")", 1)]
	[InlineData("()())", 5)]
	public void PartTwo(string instructions, int expected)
		=> Assert.Equal(expected, _.Golded(instructions));

	[Fact]
	public void PuzzleSilver()
	{
		//  Arrange
		var input = _.ReadPuzzle();

		// Act
		var actual = _.Silver(input);

		// Assert
		Assert.Equal(74, actual);
	}

	[Fact]
	public void PuzzleGold()
	{
		//  Arrange
		var input = _.ReadPuzzle();

		// Act
		var actual = _.Gold(input);

		// Assert
		Assert.Equal(1795, actual);
	}
}
