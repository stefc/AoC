using advent.of.code.y2022;

namespace advent.of.code.tests.y2022;

[Trait("Year", "2022")]
[Trait("Day", "6")]
public class TestDay6 : IPuzzleTest
{
	private readonly IPuzzle _ = new TuningTrouble();

	[Theory]
	[InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7)]
	[InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
	[InlineData("nppdvjthqldpwncqszvftbrmjlhg", 6)]
	[InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
	[InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
	public void SampleSilver(string input, long expected)
	{
		// Arrange
		
		// Act
		var actual = _.Silver(new string[]{input});

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void PuzzleSilver()
	{
		//  Arrange
		var input = _.ReadPuzzle();

		// Act
		var actual = _.Silver(input);

		// Assert
		Assert.Equal(1282, actual);
	}

	[Theory]
	[InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
	[InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
	[InlineData("nppdvjthqldpwncqszvftbrmjlhg", 23)]
	[InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
	[InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
	public void SampleGold(string input, long expected)
	{
		// // Arrange

		// Act
		var actual = _.Gold(new string[]{input});

		// // Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void PuzzleGold()
	{
		//  Arrange
		var input = _.ReadPuzzle(true);

		// Act
		var actual = _.Gold(input);

		// Assert
		Assert.Equal(3513, actual);
	}

}