using Xunit;
using advent.of.code.y2021.day3;

namespace advent.of.code.tests.y2021;

[Trait("Year", "2021")]
[Trait("Day", "3")]
public class TestDay3
{
	private readonly Prepare _ = new Prepare();

	private IEnumerable<string> ReadPuzzle() 
	=> 	File.ReadLines($"tests/y2021/{nameof(TestDay3)}.Input.txt").ToArray();

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
		var actual = _.A(input);

		// Assert
		Assert.Equal(150, actual);
	}

	[Fact]
	public void PuzzleSilver()
	{
		//  Arrange
		var input = ReadPuzzle();

		// Act
		var actual = _.A(input);

		// Assert
		Assert.Equal(1882980, actual);
	}

	[Fact]
	public void SampleGold()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.B(input);

		// Assert
		Assert.Equal(900, actual);
	}

	[Fact]
	public void PuzzleGold()
	{
		//  Arrange
		var input = ReadPuzzle();

		// Act
		var actual = _.B(input);

		// Assert
		Assert.Equal(1971232560, actual);
	}
}