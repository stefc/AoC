using System.Linq;
using advent.of.code.y2023;

namespace advent.of.code.tests.y2023;

[Trait("Year", "2023")]
[Trait("Day", "2")]
public class TestDay2 : IPuzzleTest
{
	private readonly IPuzzle _ = new CubeConundrum();
	private readonly string[] input;

	public TestDay2() => this.input = _.ReadPuzzle(false).ToArray();

	private IEnumerable<string> CreateSample() =>@"
Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
".Split(Environment.NewLine)
		.Where(line => !string.IsNullOrWhiteSpace(line))
				.ToArray();

	[Fact]
	public void ParseGameRecord()
	{
		// Parses a game record string and returns a CubeConundrum.Game object.
		// The game record string should be in the format: "Game <gameNumber>: <cube1>, <cube2>, ...; <cubeN>"
		// Each cube is represented by a color and a quantity, separated by a space.
		// Example: "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green"

		var game = CubeConundrum.ParseGameRecord("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green");
		Assert.Equal(1, game.GameNumber);
		Assert.Equal(3, game.SubSets.Count);
		Assert.Equal(18, game.SubSets.SelectMany( subSet => subSet.Cubes).Sum(cube => cube.Quantity));
	}
	[Fact]
	public void IsPossibleSubSet()
	{
		var subSet = new SubSet([
			new(CubeColor.Red, 1),
			new(CubeColor.Green, 2),
			new(CubeColor.Blue, 6)
		]);
		Assert.True(subSet.IsPossible(1, 2, 6));
		Assert.False(subSet.IsPossible(1, 2, 5));
		Assert.False(subSet.IsPossible(1, 1, 6));
		Assert.False(subSet.IsPossible(0, 2, 6));
	}

	[Fact]
	public void IsPossibleGame()
	{
		var game = CubeConundrum.ParseGameRecord("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green");
		Assert.True(game.IsPossible(4, 2, 6));
		Assert.False(game.IsPossible(1, 1, 1));
	}

	[Fact]
	public void SampleSilver() => Assert.Equal(8, _.Silver(CreateSample()));

	[Fact]
	public void SampleGold() => Assert.Equal(2286, _.Gold(CreateSample()));

	public void PuzzleSilver() => _.Silver(this.input);
	public void PuzzleGold() => _.Gold(this.input);

	[Fact] public void TestSilver() => Assert.Equal(2176, _.Silver(this.input));

	[Fact] public void TestGold() => Assert.Equal(63700, _.Gold(this.input));
}
