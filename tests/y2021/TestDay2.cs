using Xunit;
using advent.of.code.y2021.day2;

namespace advent.of.code.tests.y2021;

[Trait("Year", "2021")]
[Trait("Day", "2")]
public class TestDay2
{
	private readonly Prepare _ = new Prepare();

	private IEnumerable<int> ReadPuzzle() 
	=> 	File.ReadLines($"tests/y2021/{nameof(TestDay2)}.Input.txt").Select(x => Convert.ToInt32(x)).ToArray();

	private IEnumerable<int> CreateSample()
	=> new int[]{199,200,208,210,200,207,240,269,260,263};
	
	[Fact]
	public void SampleSilver()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.A(input);

		// Assert
		Assert.Equal(7, actual);
	}

	[Fact(Skip="Fehlerhaft")]
	public void PuzzleSilver()
	{
		//  Arrange
		var input = ReadPuzzle();

		// Act
		var actual = _.A(input);

		// Assert
		Assert.Equal(1754, actual);
	}

	[Fact(Skip="Fehlerhaft")]
	public void SampleGold()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.B(input);

		// Assert
		Assert.Equal(5, actual);
	}

	[Fact(Skip="Fehlerhaft")]
	public void PuzzleGold()
	{
		//  Arrange
		var input = ReadPuzzle();

		// Act
		var actual = _.B(input);

		// Assert
		Assert.Equal(1789, actual);
	}
}