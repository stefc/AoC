using advent.of.code.y2022;
using MoreLinq;

namespace advent.of.code.tests.y2022;

[Trait("Year", "2022")]
[Trait("Day", "8")]
public class TestDay8 : IPuzzleTest
{
	private readonly IPuzzle _ = new TreeTop();

	private IEnumerable<string> CreateSample()
	=> @"30373
25512
65332
33549
35390".Split("\n").ToArray();


	[Fact]
	public void SampleSilver()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.Silver(input);

		// Assert
		Assert.Equal(21, actual);
	}

	[Fact]
	public void PuzzleSilver()
	{
		//  Arrange
		var input = _.ReadPuzzle();

		// Act
		var actual = _.Silver(input);

		// Assert
		Assert.Equal(1827, actual);
	}

	
	[Fact]
	public void SampleGold()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.Gold(input);
		
		// // Assert
		Assert.Equal(24933642, actual);
	}

	[Fact]
	public void PuzzleGold()
	{
		//  Arrange
		var input = _.ReadPuzzle(true);

		// Act
		var actual = _.Gold(input);

		// Assert
		Assert.Equal(7268994, actual);
	}

}