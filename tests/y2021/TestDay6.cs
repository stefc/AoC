using advent.of.code.y2021.day6;

namespace advent.of.code.tests.y2021;

[Trait("Year", "2021")]
[Trait("Day", "6")]
public class TestDay6  : IPuzzleTest
{
	private readonly IPuzzle _ = new Lanternfish();

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
		Assert.Equal(5934, actual);
	}

	
	[Fact]
	public void PuzzleSilver()
	{
		//  Arrange
		var input = _.ReadPuzzle();

		// Act
		var actual = _.Silver(input);

		// Assert
		Assert.Equal(374927, actual);
	}

	[Fact]
	public void SampleGold()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.Gold(input);

		// Assert
		Assert.Equal(26984457539, actual);
	}

	[Fact]
	public void PuzzleGold()
	{
		//  Arrange
		var input = _.ReadPuzzle();

		// Act
		var actual = _.Gold(input);

		// Assert
		Assert.Equal(1_687_617_803_407, actual);
	}

}