// http://adventofcode.com/2015/day/3

using advent.of.code.y2015.day3;
namespace advent.of.code.tests.y2015;

[Trait("Year", "2015")]
[Trait("Day", "3")]
public class TestDay3
{
	private readonly IPuzzle _ = new SphericalHouses();

	[Theory]
	[InlineData(">", 2)]
	[InlineData("^>v<", 4)]
	[InlineData("^v^v^v^v^v", 2)]
	public void PartOne(string instructions, int expected)
		=> Assert.Equal(expected, _.Silvered(instructions));

	[Theory]
	[InlineData("^v", 3)]
	[InlineData("^>v<", 3)]
	[InlineData("^v^v^v^v^v", 11)]
	public void PartTwo(string instructions, int expected)
		=> Assert.Equal(expected, _.Golded(instructions));

	[Fact]
	public void Puzzle()
	{
		var input = File
			.ReadAllText("tests/y2015/Day3.Input.txt");

		Assert.Equal(2081, _.Silvered(input));

		Assert.Equal(2341, _.Golded(input));
	}
}