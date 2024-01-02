using advent.of.code.y2022;

namespace advent.of.code.tests.y2022;

[Trait("Year", "2022")]
[Trait("Day", "5")]
public class TestDay5 : IPuzzleTest
{
	private readonly IPuzzle<string> _ = new SupplyStacks();

	private readonly string[] input;

	public TestDay5() => this.input = _.ReadPuzzle(true).ToArray();

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

	[Fact( Skip="Not work")] public void SampleSilver() => Assert.Equal("CMZ", _.Silver(CreateSample()));

	[Fact( Skip="Not work")] public void SampleGold() => Assert.Equal("MCD", _.Gold(CreateSample()));

	public void PuzzleSilver() => _.Silver(this.input);
	public void PuzzleGold() => _.Gold(this.input);

	[Fact] public void TestSilver() => Assert.Equal("VCTFTJQCG", _.Silver(this.input));
	[Fact] public void TestGold() => Assert.Equal("GCFGLDNJZ", _.Gold(this.input));

}
