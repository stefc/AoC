using advent.of.code.y2021.day11;

namespace advent.of.code.tests.y2021;

[Trait("Year", "2021")]
[Trait("Day", "11")]
public class TestDay11 : IPuzzleTest
{
	private readonly IPuzzle _ = new Prepare();

	private IEnumerable<string> CreateSample() {
		string input = @"
";	
		return input.Split("\n")
				.Where(line => !String.IsNullOrEmpty(line))
				.ToArray();
	}
	
	[Fact]
	public void SampleSilver() 
	=> Assert.Equal(1, _.Silver(CreateSample()));
	
	[Fact]
	public void PuzzleSilver()
	=> Assert.Equal(1, _.Silver(_.ReadPuzzle()));
	
	[Fact]
	public void SampleGold() 
	=> Assert.Equal(1, _.Gold(CreateSample()));
	
	
	[Fact]
	public void PuzzleGold()
	=> Assert.Equal(1, _.Gold(_.ReadPuzzle()));
}