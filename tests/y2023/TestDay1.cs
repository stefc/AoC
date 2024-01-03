using advent.of.code.y2023;

namespace advent.of.code.tests.y2023;

[Trait("Year", "2023")]
[Trait("Day", "1")]
public class TestDay1 : IPuzzleTest
{
	private readonly IPuzzle _ = new Trebuchet();
	private readonly string[] input;

	public TestDay1() => this.input = _.ReadPuzzle(true).ToArray();

	private IEnumerable<string> CreateSampleA()
	=> new string[]{"1abc2","pqr3stu8vwx","a1b2c3d4e5f","treb7uchet"};
	private IEnumerable<string> CreateSampleB()
	=> new string[]{"two1nine","eightwothree","abcone2threexyz",
		"xtwone3four","4nineeightseven2","zoneight234","7pqrstsixteen"};


	[Fact]
	public void SampleSilver() => Assert.Equal(142, _.Silver(CreateSampleA()));

	[Fact]
	public void SampleGold() => Assert.Equal(281, _.Gold(CreateSampleB()));

	public void PuzzleSilver() => _.Silver(this.input);
	public void PuzzleGold() => _.Gold(this.input);

	[Fact] public void TestSilver() => Assert.Equal(54331, _.Silver(this.input));

	[Fact] public void TestGold() => Assert.Equal(54518, _.Gold(this.input));
}
