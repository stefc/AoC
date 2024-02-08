using advent.of.code.y2022;

namespace advent.of.code.tests.y2022;

[Trait("Year", "2022")]
[Trait("Day", "1")]
public class TestDay1 : IPuzzleTest
{
	private readonly IPuzzle _ = new CalorieCounting();
	private readonly string[] input;

	public TestDay1() => this.input = _.ReadPuzzle(true).ToArray();

	private IEnumerable<string> CreateSample()
	=> new string[]{"1000","2000","3000","","4000","","5000","6000","","7000","8000","9000","","10000"};

	[Fact]
	public void SampleSilver() => Assert.Equal(24000, _.Silver(CreateSample()));

	[Fact]
	public void SampleGold() => Assert.Equal(45000, _.Gold(CreateSample()));

	public void PuzzleSilver() => _.Silver(this.input);
	public void PuzzleGold() => _.Gold(this.input);

	[Fact] public void TestSilver() => Assert.Equal(71506, _.Silver(this.input));

	[Fact] public void TestGold() => Assert.Equal(209603, _.Gold(this.input));
}