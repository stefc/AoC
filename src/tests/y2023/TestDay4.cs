using advent.of.code.y2023;
using Shouldly;

namespace advent.of.code.tests.y2023;

[Trait("Year", "2023")]
[Trait("Day", "4")]
public class TestDay4 : IPuzzleTest
{
	private readonly IPuzzle _ = new Scratchcards();
	private readonly string[] input;

	public TestDay4() => this.input = _.ReadPuzzle(false).ToArray();

	private IEnumerable<string> CreateSample() =>@"
Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
".Split(Environment.NewLine).Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();

	[Fact]
	public void SampleSilver() => Assert.Equal(13, _.Silver(CreateSample()));

	[Fact]
	public void SampleGold() => Assert.Equal(30, _.Gold(CreateSample()));

	[Fact]
	public void TestParse() {
		var card = Scratchcards.ParseCard("Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83");
		card.CardNumber.ShouldBe(4);
		card.Numbers.ShouldBe(new [] { 59, 84, 76, 51, 58,  5, 54, 83 }, ignoreOrder: true);
		card.Winning.ShouldBe(new [] { 41, 92, 73, 84, 69 }, ignoreOrder: true);
	}

	void IPuzzleTest.PuzzleSilver() => _.Silver(this.input);
	void IPuzzleTest.PuzzleGold() => _.Gold(this.input);

	[Fact] public void TestSilver() => Assert.Equal(25571, _.Silver(this.input));

	[Fact] public void TestGold() => Assert.Equal(8805731, _.Gold(this.input));
}
