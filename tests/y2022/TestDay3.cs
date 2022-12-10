using advent.of.code.y2022;

namespace advent.of.code.tests.y2022;

[Trait("Year", "2022")]
[Trait("Day", "3")]
public class TestDay3 : IPuzzleTest
{
	private readonly IPuzzle _ = new RucksackReorg();
	private readonly string[] input;

	public TestDay3() => this.input = _.ReadPuzzle().ToArray();

	private IEnumerable<string> CreateSample()
	=> new string[]{
		"vJrwpWtwJgWrhcsFMMfFFhFp",
		"jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
		"PmmdzqPrVvPwwTWBwg",
		"wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
		"ttgJtRGJQctTZtZT",
		"CrZsJsPPZsGzwwsLwLmpwMDw"};

	[Fact] public void SampleSilver() => Assert.Equal(157, _.Silver(CreateSample()));

	[Fact] public void SampleGold() => Assert.Equal(70, _.Gold(CreateSample()));

	public void PuzzleSilver() => _.Silver(this.input);
	public void PuzzleGold() => _.Gold(this.input);

	[Fact] public void TestSilver() => Assert.Equal(7821, _.Silver(this.input));
	[Fact] public void TestGold() => Assert.Equal(2752, _.Gold(this.input));
}