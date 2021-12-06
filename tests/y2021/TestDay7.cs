using advent.of.code.y2021.day7;

namespace advent.of.code.tests.y2021;

[Trait("Year", "2021")]
[Trait("Day", "7")]
public class TestDay7
{
	private readonly IPuzzle _ = new Prepare();

	private IEnumerable<string> CreateSample()
	{
		string input = "3,4,3,1,2";
		return input.Split("\n")
				.ToArray();
	}

	[Fact]
	public void SampleSilver()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.Silver(input);

		// Assert
		Assert.Equal(1, actual);
	}

	
	[Fact]
	public void PuzzleSilver()
	{
		//  Arrange
		var input = _.ReadPuzzle();

		// Act
		var actual = _.Silver(input);

		// Assert
		Assert.Equal(1, actual);
	}

	[Fact]
	public void SampleGold()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.Gold(input);

		// Assert
		Assert.Equal(1, actual);
	}

	[Fact]
	public void PuzzleGold()
	{
		//  Arrange
		var input = _.ReadPuzzle();

		// Act
		var actual = _.Gold(input);

		// Assert
		Assert.Equal(1, actual);
	}

}