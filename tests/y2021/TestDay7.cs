using advent.of.code.y2021.day7;

namespace advent.of.code.tests.y2021;

[Trait("Year", "2021")]
[Trait("Day", "7")]
public class TestDay7
{
	private readonly IPuzzle _ = new TreacheryOfWhales();

	private IEnumerable<string> CreateSample() 
	=> "16,1,2,0,4,2,7,1,2,14".Split("\n").ToArray();
	
	[Fact]
	public void SampleSilver() 
	=> Assert.Equal(37, _.Silver(CreateSample()));
	
	[Fact]
	public void PuzzleSilver()
	=> Assert.Equal(343605, _.Silver(_.ReadPuzzle()));
	
	[Fact]
	public void SampleGold() 
	=> Assert.Equal(168, _.Gold(CreateSample()));
	
	[Fact]
	public void PuzzleGold()
	=> Assert.Equal(96_744_904, _.Gold(_.ReadPuzzle()));

}