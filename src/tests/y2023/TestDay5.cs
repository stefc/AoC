using advent.of.code.y2023;
using Shouldly;

namespace advent.of.code.tests.y2023;

[Trait("Year", "2023")]
[Trait("Day", "5")]
public class TestDay5
{
	private readonly Fertilizer _ = new ();
	private readonly string[] input;

	public TestDay5() => this.input = _.ReadPuzzle(false).ToArray();

	private IEnumerable<string> CreateSample() =>@"
seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4
".Split(Environment.NewLine).Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();

	[Fact]
	public void SampleSilver() => Assert.Equal(13, _.Silver(CreateSample()));

	[Fact]
	public void SampleGold() => Assert.Equal(30, _.Gold(CreateSample()));


	[Fact] public void TestSilver() => Assert.Equal(25571, _.Silver(this.input));

	[Fact] public void TestGold() => Assert.Equal(8805731, _.Gold(this.input));
}
