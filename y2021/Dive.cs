using System.Text.RegularExpressions;
using advent.of.code.common;

namespace advent.of.code.y2021.day2;

// http://adventofcode.com/2021/day/2

class Dive : IPuzzle
{
	public int Silver(IEnumerable<string> values)
	=> values.Select(ToCommand).Aggregate(new Level(0, 0),
			 (accu, cmd) => cmd.Direction switch
			 {
				 Direction.Up or Direction.Down => accu with { Depth = accu.Depth + cmd.Direction.Sign() * cmd.Steps },

				 _ => accu with { Horizontal = accu.Horizontal + cmd.Steps }
			 },
			 accu => accu.Horizontal * accu.Depth);

	public int Gold(IEnumerable<string> values)
	=> values.Select(ToCommand)
		.Aggregate(new Track(0, 0, 0),
			(accu, cmd) =>
				(cmd.Direction != Direction.Forward) ?
					accu with { Aim = accu.Aim + cmd.Direction.Sign() * cmd.Steps }
					:
					accu with { Horizontal = accu.Horizontal + cmd.Steps, Depth = accu.Depth + accu.Aim * cmd.Steps },
				accu => accu.Horizontal * accu.Depth);




	public static Command ToCommand(string input)
	{
		string pattern =
			@"^(up|down|forward)\s(\d*)$";

		Match m = Regex.Match(input, pattern);
		return (m.Success) ?
			new Command(Enum.Parse<Direction>(m.Groups[1].Value, true), Convert.ToInt32(m.Groups[2].Value))
			:
			new Command(Direction.Forward, 0);
	}
}

internal enum Direction { Up, Down, Forward };

internal record Command(Direction Direction, int Steps);

internal record Level(int Horizontal, int Depth);
internal record Track(int Horizontal, int Depth, int Aim) : Level(Horizontal, Depth);

internal static class DirectionExtensions
{
	public static int Sign(this Direction d) => d == Direction.Up ? -1 : +1;
}