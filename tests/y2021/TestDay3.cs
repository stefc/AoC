using advent.of.code.y2021.day3;

namespace advent.of.code.tests.y2021;

[Trait("Year", "2021")]
[Trait("Day", "3")]
public class TestDay3
{
	private readonly BinaryDiagnostic _ = new BinaryDiagnostic();

	private IEnumerable<string> ReadPuzzle() 
	=> 	File.ReadLines($"tests/y2021/{nameof(TestDay3)}.Input.txt").ToArray();

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
		var actual = _.Silver(input);

		// Assert
		Assert.Equal(198, actual);
	}

	[Fact]
	public void PuzzleSilver()
	{
		//  Arrange
		var input = ReadPuzzle();

		// Act
		var actual = _.Silver(input);

		// Assert
		Assert.Equal(693486, actual);
	}

	[Fact]
	public void SampleGold()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.Gold(input);

		// Assert
		Assert.Equal(230, actual);
	}

	[Fact]
	public void PuzzleGold()
	{
		//  Arrange
		var input = ReadPuzzle();

		// Act
		var actual = _.Gold(input);

		// Assert
		Assert.Equal(3379326, actual);
	}
}