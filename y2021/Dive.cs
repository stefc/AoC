using System.Text.RegularExpressions;
using advent.of.code.common;

namespace advent.of.code.y2021.day2;

// http://adventofcode.com/2021/day/2

class Dive
{
	public int A(IEnumerable<string> values)
	{
		var cmds = values.Select(ToCommand);

		var p = cmds.Aggregate( Point.Zero, (accu, cmd) => 
		accu + cmd.Direction * cmd.Steps);
			
		return p.X * p.Y;
	}

	public int B(IEnumerable<string> values)
	{
		var cmds = values.Select(ToCommand);

		return cmds.Aggregate( new Track(0,0,0), 
			(accu, cmd) => 
				(cmd.Direction.Y != 0) ? 
					accu with { Aim = accu.Aim + cmd.Direction.Y * cmd.Steps } 
					:
					accu with { Horizontal  = accu.Horizontal + cmd.Steps, Depth = accu.Depth  + accu.Aim * cmd.Steps },
				accu => accu.Horizontal * accu.Depth);
	}

	private static Point InstructionToDir(string instruction)
	=> instruction switch
	{ "up" => Point.North, "down" => Point.South, _ => Point.East };

	internal record Command ( Point Direction, int Steps);

	internal record Track( int Horizontal, int Depth, int Aim);

	internal enum Direction { Up, Down, Forward };

	public static Command ToCommand(string input) {
		string pattern = 
			@"^(up|down|forward)\s(\d*)$";
		
		Match m = Regex.Match(input, pattern);
		if (m.Success)
		{
			return new Command(InstructionToDir(m.Groups[1].Value), 
				Convert.ToInt32(m.Groups[2].Value));
		}
		return new Command(Point.Zero,0);
         }

}
