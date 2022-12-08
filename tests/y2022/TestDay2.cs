using advent.of.code.y2022;

namespace advent.of.code.tests.y2022;

[Trait("Year", "2022")]
[Trait("Day", "2")]
public class TestDay2 : IPuzzleTest
{
	private readonly IPuzzle _ = new RockPaperScissors();

	private IEnumerable<string> CreateSample()
	=> new string[]{
		"A Y",
		"B X",
		"C Z"};

	[Fact]
	public void SampleSilver()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.Silver(input);

		// Assert
		Assert.Equal(15, actual);
	}

	[Fact]
	public void PuzzleSilver()
	{
		//  Arrange
		var input = _.ReadPuzzle();

		// Act
		var actual = _.Silver(input);

		// Assert
		Assert.Equal(13809, actual);
	}

	[Fact]
	public void SampleGold()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.Gold(input);

		// Assert
		Assert.Equal(12, actual);
	}

	[Fact]
	public void PuzzleGold()
	{
		//  Arrange
		var input = _.ReadPuzzle();

		// Act
		var actual = _.Gold(input);

		// Assert
		Assert.Equal(12316, actual);
	}

}