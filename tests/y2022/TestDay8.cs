using advent.of.code.y2022;

namespace advent.of.code.tests.y2022;

[Trait("Year", "2022")]
[Trait("Day", "8")]
public class TestDay8 : IPuzzleTest
{
	private readonly IPuzzle _ = new TreeTop();
	private readonly string[] input;

	public TestDay8() => this.input = _.ReadPuzzle().ToArray();

	private IEnumerable<string> CreateSample()
	=> @"30373
25512
65332
33549
35390".Split("\n").ToArray();


	[Fact]	public void SampleSilver() => Assert.Equal(21, _.Silver(CreateSample()));

	[Fact]	public void SampleGold() => Assert.Equal(8, _.Gold(CreateSample()));

	public void PuzzleSilver() => _.Silver(this.input);
	public void PuzzleGold() => _.Gold(this.input);

	[Fact] public void TestSilver() => Assert.Equal(1827, _.Silver(this.input));

	[Fact] public void TestGold()  => Assert.Equal(335580, _.Gold(this.input));
}