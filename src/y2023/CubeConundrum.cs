using MoreLinq;

namespace advent.of.code.y2023;

partial

// http://adventofcode.com/2023/day/2

class CubeConundrum : IPuzzle
{
	public static CubeColor ToColor(string value) => Enum.Parse<CubeColor>(value, true);

	public static GameRecord ParseGameRecord(string gameRecord)
	{
		var gameRecordRegex = GameRecordRegex();
		var cubeRegex = CubeRegex();

		var gameRecordMatch = gameRecordRegex.Match(gameRecord);
		if (!gameRecordMatch.Success)
		{
			throw new ArgumentException("Invalid game record format.");
		}

		var gameNumber = int.Parse(gameRecordMatch.Groups[1].Value);
		var cubesString = gameRecordMatch.Groups[2].Value.Split(';');

		return new GameRecord(gameNumber, cubesString
			.Select(cubeString => cubeRegex.Matches(cubeString)
				.Select(cubeMatch => new Cube(
					ToColor(cubeMatch.Groups[2].Value),
					int.Parse(cubeMatch.Groups[1].Value)))
				.ToList())
			.Select(cubes => new SubSet(cubes))
			.ToList());
	}
	public long Silver(IEnumerable<string> input)
	{
		var gameRecords = input.Select(ParseGameRecord).ToList();
		var possibleGames = gameRecords
			.Where(gameRecord => gameRecord.SubSets.All(subSet => subSet.IsPossible(12, 13, 14)))
			.ToList();
		return possibleGames.Sum(game => game.GameNumber);
	}

	public long Gold(IEnumerable<string> input)
	{
		var gameRecords = input.Select(ParseGameRecord).ToList();
		return  gameRecords
			.Select(game => game.MinimumCubeCount())
			.Sum( rgb => rgb.red * rgb.green * rgb.blue);
	}

	[GeneratedRegex(@"Game (\d+): (.+)")]
	private static partial Regex GameRecordRegex();


	[GeneratedRegex(@"(\d+) ((red|green|blue)+)")]
	private static partial Regex CubeRegex();
}

// enum with 3 values red, green, blue specify color of cube

internal record GameRecord(int GameNumber, List<SubSet> SubSets)
{
	public bool IsPossible(int red, int green, int blue)
	{
		var tmp = SubSets.Select(subSet => subSet.IsPossible(red, green, blue)).ToList();
		return SubSets.All(subSet => subSet.IsPossible(red, green, blue));
	}

	public (int red, int green, int blue) MinimumCubeCount() =>(
		SubSets.Select(subSet => subSet.MinimumCubeCount().red).Max(),
		SubSets.Select(subSet => subSet.MinimumCubeCount().green).Max(),
		SubSets.Select(subSet => subSet.MinimumCubeCount().blue).Max());
};

internal record SubSet(List<Cube> Cubes)
{
	// determine possible subset
	public bool IsPossible(int red, int green, int blue)
	{
		var redCubes = Cubes.Where(cube => cube.Color == CubeColor.Red).Sum(cube => cube.Quantity);
		var greenCubes = Cubes.Where(cube => cube.Color == CubeColor.Green).Sum(cube => cube.Quantity);
		var blueCubes = Cubes.Where(cube => cube.Color == CubeColor.Blue).Sum(cube => cube.Quantity);
		return red >= redCubes && green >= greenCubes && blue >= blueCubes;
	}

	public (int red, int green, int blue) MinimumCubeCount() =>(
		Cubes.Where(cube => cube.Color == CubeColor.Red).Sum(cube => cube.Quantity),
		Cubes.Where(cube => cube.Color == CubeColor.Green).Sum(cube => cube.Quantity),
		Cubes.Where(cube => cube.Color == CubeColor.Blue).Sum(cube => cube.Quantity));
};

internal record Cube(CubeColor Color, int Quantity);

internal enum CubeColor { Red, Green, Blue };
