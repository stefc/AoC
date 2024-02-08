using advent.of.code.y2021.day11;

namespace advent.of.code.tests.y2021;

[Trait("Year", "2021")]
[Trait("Day", "11")]
public class TestDay11 : IPuzzleTest
{
	private readonly IPuzzle _ = new DumboOctopus();

	private IEnumerable<string> CreateSample() {
		string input = @"
5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526
";
		return input.Split(Environment.NewLine)
				.Where(line => !String.IsNullOrEmpty(line))
				.ToArray();
	}

	private IEnumerable<string> CreateSampleSmall() {
		string input = @"
11111
19991
19191
19991
11111
";
		return input.Split(Environment.NewLine)
				.Where(line => !String.IsNullOrWhiteSpace(line))
				.ToArray();
	}

	[Fact]
	public void SampleSilver()
	=> Assert.Equal(1656, _.Silver(CreateSample()));

	[Fact]
	public void PuzzleSilver()
	=> Assert.Equal(1652, _.Silver(_.ReadPuzzle()));

	[Fact]
	public void SampleGold()
	=> Assert.Equal(195, _.Gold(CreateSample()));


	[Fact]
	public void PuzzleGold()
	=> Assert.Equal(220, _.Gold(_.ReadPuzzle()));

}
