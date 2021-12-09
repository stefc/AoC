using advent.of.code.y2021.day9;

namespace advent.of.code.tests.y2021;

[Trait("Year", "2021")]
[Trait("Day", "9")]
public class TestDay9
{
	private readonly IPuzzle _ = new SmokeBasin();

	private IEnumerable<string> CreateSample() {
		string input = @"
2199943210
3987894921
9856789892
8767896789
9899965678
";	
		return input.Split("\n")
				.Where(line => !String.IsNullOrEmpty(line))
				.ToArray();
	}
	
	[Fact]
	public void SampleSilver() 
	=> Assert.Equal(15, _.Silver(CreateSample()));
	
	[Fact]
	public void PuzzleSilver()
	=> Assert.Equal(633, _.Silver(_.ReadPuzzle()));
	
	[Fact]
	public void SampleGold() 
	=> Assert.Equal(1134, _.Gold(CreateSample()));
	
	[Fact]
	public void PuzzleGold()
	=> Assert.Equal(1050192, _.Gold(_.ReadPuzzle()));

	[Theory]
	[InlineData("1,0", 3)]
	[InlineData("9,0", 9)]
	[InlineData("2,2", 14)]
	[InlineData("6,4", 9)]
	public void TestFlood(string at, int exp) 
	{
		Assert.Equal(exp, SmokeBasin.Parse(CreateSample()).Flood(SmallPoint.FromString(at)).Count);
	}
}