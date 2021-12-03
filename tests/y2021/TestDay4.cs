using Xunit;
using advent.of.code.y2021.day4;

namespace advent.of.code.tests.y2021;

[Trait("Year", "2021")]
[Trait("Day", "4")]
public class TestDay4
{
	private readonly Prepare _ = new Prepare();

	private IEnumerable<string> ReadPuzzle() 
	=> 	File.ReadLines($"tests/y2021/{nameof(TestDay4)}.Input.txt").ToArray();

	private IEnumerable<string> CreateSample()
	=> new string[]{
	"00100",
	"11110",
	"10110",
	"10111",
	"10101",
	"01111",
	"00111",
	"11100",
	"10000",
	"11001",
	"00010",
	"01010"
		};
	
	[Fact]
	public void SampleSilver()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.A(input);

		// Assert
		Assert.Equal(1, actual);
	}

	[Fact]
	public void PuzzleSilver()
	{
		//  Arrange
		var input = ReadPuzzle();

		// Act
		var actual = _.A(input);

		// Assert
		Assert.Equal(1, actual);
	}

	[Fact]
	public void SampleGold()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.B(input);

		// Assert
		Assert.Equal(1, actual);
	}

	[Fact]
	public void PuzzleGold()
	{
		//  Arrange
		var input = ReadPuzzle();

		// Act
		var actual = _.B(input);

		// Assert
		Assert.Equal(1, actual);
	}
}