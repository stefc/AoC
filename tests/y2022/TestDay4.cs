using advent.of.code.y2022;

namespace advent.of.code.tests.y2022;

[Trait("Year", "2022")]
[Trait("Day", "4")]
public class TestDay4 : IPuzzleTest
{
	private readonly IPuzzle _ = new CampCleanup();
	private readonly string[] input;
	public TestDay4() => this.input = _.ReadPuzzle().ToArray();

	private IEnumerable<string> CreateSample()
	=> new string[]{
		"2-4,6-8","2-3,4-5","5-7,7-9","2-8,3-7","6-6,4-6","2-6,4-8"};

	[Fact] public void SampleSilver() => Assert.Equal(2, _.Silver(CreateSample()));

	[Fact] public void SampleGold() => Assert.Equal(4, _.Gold(CreateSample()));

	public void PuzzleSilver() => _.Silver(this.input);
	public void PuzzleGold() => _.Gold(this.input);

	[Fact] public void TestSilver() => Assert.Equal(580, _.Silver(this.input));
	[Fact] public void TestGold() => Assert.Equal(895, _.Gold(this.input));
}