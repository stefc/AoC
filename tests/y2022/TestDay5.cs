using advent.of.code.y2022;

namespace advent.of.code.tests.y2022;

[Trait("Year", "2022")]
[Trait("Day", "5")]
public class TestDay5 : IPuzzleTest
{
	private readonly IPuzzle<string> _ = new SupplyStacks();

	private IEnumerable<string> CreateSample()
	=> @"
    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2".Split("\n").ToArray();

	[Fact]
	public void SampleSilver()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.Silver(input);

		// Assert
		Assert.Equal("CMZ", actual);
	}

	[Fact]
	public void PuzzleSilver()
	{
		//  Arrange
		var input = _.ReadPuzzle(true);

		// Act
		var actual = _.Silver(input);

		// Assert
		Assert.Equal("VCTFTJQCG", actual);
	}

	[Fact]
	public void SampleGold()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.Gold(input);

		// Assert
		Assert.Equal("MCD", actual);
	}

	[Fact]
	public void PuzzleGold()
	{
		//  Arrange
		var input = _.ReadPuzzle(true);

		// Act
		var actual = _.Gold(input);

		// Assert
		Assert.Equal("GCFGLDNJZ", actual);
	}

}