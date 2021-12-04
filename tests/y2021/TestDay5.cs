using Xunit;
using advent.of.code.y2021;
using advent.of.code.y2021.day5;

namespace advent.of.code.tests.y2021;

[Trait("Year", "2021")]
[Trait("Day", "5")]
public class TestDay5
{
	private readonly IPuzzle _ = new Prepare();

	private IEnumerable<string> ReadPuzzle()
	=> File.ReadLines($"tests/y2021/{nameof(TestDay5)}.Input.txt")
		.Where(line => !String.IsNullOrEmpty(line))
		.ToArray();

	private IEnumerable<string> CreateSample()
	{
		string input = @"
7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1

22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19

 3 15  0  2 22
 9 18 13 17  5
19  8  7 25 23
20 11 10 24  4
14 21 16 12  6

14 21 17 24  4
10 16 15  9 19
18  8 23 26 20
22 11 13  6  5
 2  0 12  3  7
";
		return input.Split("\n")
				.Where(line => !String.IsNullOrEmpty(line))
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
		Assert.Equal(4512, actual);
	}

	
	[Fact]
	public void PuzzleSilver()
	{
		//  Arrange
		var input = ReadPuzzle();

		// Act
		var actual = _.Silver(input);

		// Assert
		Assert.Equal(50008, actual);
	}

	[Fact]
	public void SampleGold()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.Gold(input);

		// Assert
		Assert.Equal(1924, actual);
	}

	[Fact]
	public void PuzzleGold()
	{
		//  Arrange
		var input = ReadPuzzle();

		// Act
		var actual = _.Gold(input);

		// Assert
		Assert.Equal(17408, actual);
	}
}