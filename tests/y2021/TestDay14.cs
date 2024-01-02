using System.Reflection;
using advent.of.code.y2021.day14;

namespace advent.of.code.tests.y2021;

[Trait("Year", "2021")]
[Trait("Day", "14")]
public class TestDay14 : IPuzzleTest
{
	private readonly IPuzzle _ = new ExtPolymerization();

	private IEnumerable<string> CreateSample() {
		string input = @"NNCB
CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C";
		return input.Split("\n")
				.ToArray();
	}

	[Fact( Skip="Not work")]
	public void SampleSilver() => Assert.Equal(1588, _.Silver(CreateSample()));

	[Fact]
	public void PuzzleSilver() => Assert.Equal(2703, _.Silver(_.ReadPuzzle()));

	[Fact( Skip="Not work")]
	public void SampleGold() => Assert.Equal(2188189693529, _.Gold(CreateSample()));

	[Fact]
	public void PuzzleGold() => Assert.Equal(2984946368465, _.Gold(_.ReadPuzzle()));
}
