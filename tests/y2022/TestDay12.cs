using System.Text;
using advent.of.code.y2022;
using Xunit.Abstractions;

namespace advent.of.code.tests.y2022;

[Trait("Year", "2022")]
[Trait("Day", "9")]
public class TestDay12 : IPuzzleTest
{
	private readonly IPuzzle _ = new HillClimbing();
	private readonly string[] input;


	public TestDay12() => this.input = _.ReadPuzzle().ToArray();

	private IEnumerable<string> CreateSample()
	=> @"Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi".Split("\n").ToArray();



	[Fact( Skip="Not work")]	public void SampleSilver() => Assert.Equal(31, _.Silver(CreateSample()));

	[Fact( Skip="Not work")]	public void SampleGold() => Assert.Equal(29, _.Gold(CreateSample()));

	public void PuzzleSilver() => _.Silver(this.input);
	public void PuzzleGold() => _.Gold(this.input);

	[Fact] public void TestSilver() => Assert.Equal(490, _.Silver(this.input));

	[Fact] public void TestGold()  => Assert.Equal(488, _.Gold(this.input));
}
