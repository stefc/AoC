using advent.of.code.y2021.day2;

namespace advent.of.code.tests.y2021;

[Trait("Year", "2021")]
[Trait("Day", "2")]
public class TestDay2
{
	private readonly IPuzzle _ = new Dive();

	private IEnumerable<string> CreateSample()
	=> new string[]{
		"forward 5","down 5","forward 8","up 3","down 8","forward 2"
		};
	
	[Fact]
	public void SampleSilver()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.Silver(input);

		// Assert
		Assert.Equal(150, actual);
	}

	[Fact]
	public void PuzzleSilver()
	{
		//  Arrange
		var input = _.ReadPuzzle();

		// Act
		var actual = _.Silver(input);

		// Assert
		Assert.Equal(1882980, actual);
	}

	[Fact]
	public void SampleGold()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.Gold(input);

		// Assert
		Assert.Equal(900, actual);
	}

	[Fact]
	public void PuzzleGold()
	{
		//  Arrange
		var input = _.ReadPuzzle();

		// Act
		var actual = _.Gold(input);

		// Assert
		Assert.Equal(1971232560, actual);
	}
}