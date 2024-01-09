using advent.of.code.y2023;
using Shouldly;

namespace advent.of.code.tests.y2023;

[Trait("Year", "2023")]
[Trait("Day", "3")]
public class TestDay3 : IPuzzleTest
{
	private readonly IPuzzle _ = new GearRatios();
	private readonly string[] input;

	public TestDay3() => this.input = _.ReadPuzzle(false).ToArray();

	private IEnumerable<string> CreateSample() =>@"
467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..
".Split(Environment.NewLine).Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();

	[Fact]
	public void SampleSilver() => Assert.Equal(4361, _.Silver(CreateSample()));

	[Fact]
	public void SampleGold() => Assert.Equal(467835, _.Gold(CreateSample()));

	[Fact]
	public void TestSymbolMap() {
		var symbolMap = GearRatios.ExtractSymbolMap(CreateSample());
		Assert.Equal(3, symbolMap.Count( kvp => kvp.Value == '*'));
		Assert.Equal(1, symbolMap.Count( kvp => kvp.Value == '$'));
		Assert.Equal(1, symbolMap.Count( kvp => kvp.Value == '+'));
		Assert.Equal(1, symbolMap.Count( kvp => kvp.Value == '#'));
		symbolMap.ContainsKey(new Point(3,1)).ShouldBeTrue();
		symbolMap.ContainsKey(new Point(3,2)).ShouldBeFalse();
	}

	[Fact]
	public void ExtractRatiosFromLine() {
		var line = "467..114..";
		var ratios = GearRatios.ExtractRatiosFromLine(line,42).ToArray();
		Assert.Equal(2, ratios.Count());
		Assert.Equal(467, ratios.First().ratio);
		Assert.Equal(0, ratios.First().start);
		Assert.Equal(2, ratios.First().end);
		Assert.Equal(114, ratios.Last().ratio);
		Assert.Equal(5, ratios.Last().start);
		Assert.Equal(7, ratios.Last().end);
	}

	void IPuzzleTest.PuzzleSilver() => _.Silver(this.input);
	void IPuzzleTest.PuzzleGold() => _.Gold(this.input);

	[Fact] public void TestSilver() => Assert.Equal(521515, _.Silver(this.input));

	[Fact] public void TestGold() => Assert.Equal(69527306, _.Gold(this.input));
}
