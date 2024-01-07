using Xunit;

using advent.of.code.y2017;

namespace advent.of.code.tests.y2017;


[Trait("Category", "y2017")]
[Trait("Day", "2")]
public class TestDay2
{

	private readonly string[] input;

	public TestDay2() => this.input = this.ReadPuzzle().ToArray();

	private IEnumerable<string> CreateSampleSilver() =>@"
5 1 9 5
7 5 3
2 4 6 8
".Split(Environment.NewLine).Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();

private IEnumerable<string> CreateSampleGold() =>@"
5 9 2 8
9 4 7 3
3 8 6 5
".Split(Environment.NewLine).Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();

	[Fact]
	public void PartOne()
	{
		Assert.Equal(18, CorruptionChecksum.GetMinMaxAggregate(CreateSampleSilver()));
	}

	[Fact]
	public void PartTwo()
	{
		Assert.Equal(9, CorruptionChecksum.GetDivisionAggregate(CreateSampleGold()));

	}

	[Theory]
	[InlineData("5 9 2 8", 8, 2)]
	[InlineData("9 4 7 3", 9, 3)]
	[InlineData("3 8 6 5", 6, 3)]
	public void TestDivision(string line, int expectedNumerator, int expectedDenominator)
	{
		var division = line.GetDivision();

		Assert.Equal(expectedNumerator, division.Item1);
		Assert.Equal(expectedDenominator, division.Item2);
	}

	[Fact]
	public void SolvePuzzle()
	{
		Assert.Equal(58975, CorruptionChecksum.GetMinMaxAggregate(this.input));
		Assert.Equal(308, CorruptionChecksum.GetDivisionAggregate(this.input));
	}

}
