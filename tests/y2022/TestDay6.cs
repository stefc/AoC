using advent.of.code.y2022;
namespace advent.of.code.tests.y2022;

[Trait("Year", "2022")] 
[Trait("Day", "6")]
public class TestDay6 : IPuzzleTest
{
	private readonly IPuzzle _ = new TuningTrouble();
	private readonly string[] input;

	public TestDay6() => this.input = _.ReadPuzzle().ToArray();

	[Theory]
	[InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7)]
	[InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
	[InlineData("nppdvjthqldpwncqszvftbrmjlhg", 6)]
	[InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
	[InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
	public void SampleSilver(string input, long expected)
	=> Assert.Equal(expected, _.Silver(new string[]{input}));

	[Theory]
	[InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
	[InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
	[InlineData("nppdvjthqldpwncqszvftbrmjlhg", 23)]
	[InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
	[InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
	public void SampleGold(string input, long expected)
	=> Assert.Equal(expected, _.Gold(new string[]{input}));

	public void PuzzleSilver() => _.Silver(this.input);
	public void PuzzleGold() => _.Gold(this.input);

	[Fact] public void TestSilver() => Assert.Equal(1282, _.Silver(this.input));
	[Fact] public void TestGold() => Assert.Equal(3513, _.Gold(this.input));
}