using advent.of.code.y2021.day1;

namespace advent.of.code.tests.y2021;

[Trait("Year", "2021")]
[Trait("Day", "1")]
public class TestDay1
{
	private readonly IPuzzle _ = new SonarSweep();

	[Fact]
	public void SampleSilver()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.Silver(input);

		// Assert
		Assert.Equal(7, actual);
	}

	[Fact]
	public void PuzzleSilver()
	{
		//  Arrange
		var input = _.ReadPuzzle();

		// Act
		var actual = _.Silver(input);

		// Assert
		Assert.Equal(1754, actual);
	}

	[Fact]
	public void SampleGold()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.Gold(input);

		// Assert
		Assert.Equal(5, actual);
	}

	[Fact]
	public void PuzzleGold()
	{
		//  Arrange
		var input = _.ReadPuzzle();

		// Act
		var actual = _.Gold(input);

		// Assert
		Assert.Equal(1789, actual);
	}

	/*private IEnumerable<int> ReadPuzzle() 
	=> 	File.ReadLines("tests/y2021/TestDay1.Input.txt").Select(x => Convert.ToInt32(x)).ToArray();

	private IEnumerable<int> CreateSample()
	=> new int[]{199,200,208,210,200,207,240,269,260,263};
	*/

	
	private IEnumerable<string> CreateSample()
	=> new string[]{
		"199","200","208","210","200","207","240","269","260","263"
		};
}