using advent.of.code.y2022;

namespace advent.of.code.tests.y2022;

[Trait("Year", "2022")]
[Trait("Day", "2")]
public class TestDay2 : IPuzzleTest
{
	private readonly IPuzzle _ = new RockPaperScissors();
	private readonly string[] input;
	public TestDay2() => this.input = _.ReadPuzzle().ToArray();

	private IEnumerable<string> CreateSample()
	=> new string[]{"A Y","B X","C Z"};

	[Fact]
	public void SampleSilver() => Assert.Equal(15, _.Silver(CreateSample()));

	[Fact]
	public void SampleGold() => Assert.Equal(12, _.Gold(CreateSample()));

	public void PuzzleSilver()  => _.Silver(this.input);
	public void PuzzleGold()  => _.Gold(this.input);

	[Fact] 	public void TestSilver() => Assert.Equal(13809, _.Silver(this.input));
	
	[Fact] 	public void TestGold() => Assert.Equal(12316, _.Gold(this.input));	
}