// http://adventofcode.com/2015/day/6

using advent.of.code.y2015.day6;

namespace advent.of.code.tests.y2015;

[Trait("Year", "2015")]
[Trait("Day", "6")]
public class TestDay6
{
	[Theory]
	[InlineData("turn on 0,0 through 999,999", FireHazard.Command.TurnOn, 0, 0, 999, 999)]
	[InlineData("toggle 0,0 through 999,0", FireHazard.Command.Toggle, 0, 0, 999, 0)]
	[InlineData("turn off 499,499 through 500,500", FireHazard.Command.TurnOff, 499, 499, 500, 500)]
	internal void StatementParse(string code, FireHazard.Command cmd, int fromX, int fromY, int throughX, int throughY)
	{
		var stmt = FireHazard.Statement.FromString(code);

		Assert.Equal(cmd, stmt.Command);
		Assert.Equal(fromX, stmt.From.X);
		Assert.Equal(fromY, stmt.From.Y);
		Assert.Equal(throughX, stmt.Through.X);
		Assert.Equal(throughY, stmt.Through.Y);
	}

	[Fact]
	internal void TurnOnLightGrid()
	{
		var grid = new FireHazard.LightGrid(1000, 1000);
		Assert.Equal(0, grid.LightCount);
		grid = grid
			.TurnOn(new Point(0, 0), new Point(0, 0))
			.TurnOn(new Point(999, 999), new Point(999, 999))
			.TurnOn(new Point(499, 499), new Point(500, 500));
		Assert.Equal(1 + 1 + 4, grid.LightCount);
	}

	[Fact]
	internal void TurnOffLightGrid()
	{
		var grid = new FireHazard.LightGrid(1000, 1000)
			.TurnOn(new Point(499, 499), new Point(500, 500))
			.TurnOff(new Point(400, 400), new Point(500, 500));
		Assert.Equal(0, grid.LightCount);
	}

	[Fact]
	internal void ToggleLightGrid()
	{
		var grid = new FireHazard.LightGrid(1000, 1000)
			.TurnOn(new Point(499, 499), new Point(500, 500))
			.Toggle(new Point(498, 498), new Point(501, 501));
		Assert.Equal(16 - 4, grid.LightCount);
	}

	[Fact]
	internal void ToggleBug()
	{
		var grid = Enumerable
			.Repeat("toggle 3,5 through 9,9", 1)
			.Select(FireHazard.Statement.FromString)
			.Aggregate(
				seed: new FireHazard.LightGrid(10, 10),
				func: (accu, current) => accu.Operation(current)
				);
		Assert.Equal(35, grid.LightCount);
	}


	[Fact]
	public void PuzzlePartOne()
	{
		var input = File
			.ReadLines("tests/y2015/Day6.Input.txt");

		var grid = input
			.Select(FireHazard.Statement.FromString)
			.Aggregate(
				seed: new FireHazard.LightGrid(1000, 1000),
				func: (accu, current) => accu.Operation(current)
				);
		Assert.Equal(400410, grid.LightCount);
	}

	[Fact(Skip = "Longrunner")]
	public void PuzzlePartTwo()
	{
		var input = File
			.ReadLines("tests/y2015/Day6.Input.txt");

		var grid = input
			.Select(FireHazard.Statement.FromString)
			.Aggregate(
				seed: new FireHazard.BrightnessGrid(1000, 1000),
				func: (accu, current) => accu.Operation(current)
				);
		Assert.Equal(15343601, grid.TotalBrightness);
	}

	[Fact]
	public void TestBrightnessTurnOn()
	{
		var grid = new FireHazard.BrightnessGrid(1000, 1000);
		Assert.Equal(0, grid.TotalBrightness);
		grid = grid
			.TurnOn(new Point(0, 0), new Point(0, 0));
		Assert.Equal(1, grid.TotalBrightness);
	}

	[Fact]
	public void TestBrightnessToggle()
	{
		var grid = new FireHazard.BrightnessGrid(1000, 1000);
		Assert.Equal(0, grid.TotalBrightness);
		grid = grid
			.Toggle(new Point(0, 0), new Point(999, 999));
		Assert.Equal(2000000, grid.TotalBrightness);
	}
}
