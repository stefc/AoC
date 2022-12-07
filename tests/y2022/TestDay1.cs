using advent.of.code.y2022.day1;

namespace advent.of.code.tests.y2022;

[Trait("Year", "2022")]
[Trait("Day", "1")]
public class TestDay1 : IPuzzleTest
{
	private readonly IPuzzle _ = new CalorieCounting();

	private IEnumerable<string> CreateSample()
	=> new string[]{"1000","2000","3000","","4000","","5000","6000","","7000","8000","9000","","10000"};

	[Fact]
	public void SampleSilver()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.Silver(input);

		// Assert
		Assert.Equal(24000, actual);
	}

	[Fact]
	public void PuzzleSilver()
	{
		//  Arrange
		var input = _.ReadPuzzle(true);

		// Act
		var actual = _.Silver(input);

		// Assert
		Assert.Equal(71506, actual);
	}

	[Fact]
	public void SampleGold()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.Gold(input);

		// Assert
		Assert.Equal(45000, actual);
	}

	[Fact]
	public void PuzzleGold()
	{
		//  Arrange
		var input = _.ReadPuzzle(true);

		// Act
		var actual = _.Gold(input);

		// Assert
		Assert.Equal(209603, actual);
	}

}