using advent.of.code.y2022;

namespace advent.of.code.tests.y2022;

[Trait("Year", "2022")]
[Trait("Day", "9")]
public class TestDay9 : IPuzzleTest
{
	private readonly IPuzzle _ = new RopeBridge();
	private readonly string[] input;

	public TestDay9() => this.input = _.ReadPuzzle().ToArray();

	private IEnumerable<string> CreateSample()
	=> @"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2".Split("\n").ToArray();

	private IEnumerable<string> CreateSampleGold()
	=> @"R 5
U 8
L 8
D 3
R 17
D 10
L 25
U 20".Split("\n").ToArray();


	[Fact]	public void SampleSilver() => Assert.Equal(13, _.Silver(CreateSample()));

	[Fact]	public void SampleGold() => Assert.Equal(36, _.Gold(CreateSampleGold()));

	public void PuzzleSilver() => _.Silver(this.input);
	public void PuzzleGold() => _.Gold(this.input);

	[Fact] public void TestSilver() => Assert.Equal(5710, _.Silver(this.input));

	[Fact] public void TestGold()  => Assert.Equal(2259, _.Gold(this.input));
}