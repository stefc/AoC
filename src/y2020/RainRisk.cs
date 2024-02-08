using System.Linq;
using System.Collections.Generic;
using System;
using advent.of.code.common;

namespace advent.of.code.y2020.day12
{
	// http://adventofcode.com/2020/day/12

	public readonly struct Instruction
	{
		public char Code { get; init; }
		public int Length { get; init; }
		private Instruction(char code, int length) => (Code, Length) = (code, length);
		private Instruction(string stmt) => (Code, Length) = FromString(stmt);

		public override string ToString() => $"{Code}{Length}";

		private static (char, int) FromString(string stmt)
		{
			var ch = char.ToUpper(stmt.FirstOrDefault());
			if (int.TryParse(new String(stmt.Skip(1).ToArray()), out var len))
			{
				return (char.ToUpper(ch), len);
			};
			return (ch, 0);
		}

		public static Instruction Create(string stmt) => new Instruction(stmt);


	}


	static class RainRisk
	{
		public static int Navigate(IEnumerable<string> input)
		=> input
			.Select(Instruction.Create)
			.Aggregate((Point.East, location: Point.Zero), CalcA, acc => acc.location)
			.ManhattenDistance();

		public static int NavigateWaypoint(IEnumerable<string> input)
		=> input
			.Select(Instruction.Create)
			.Aggregate((new Point(10, -1), location: Point.Zero), CalcB, acc => acc.location)
			.ManhattenDistance();

		private static (Point waypoint, Point location) CalcA((Point waypoint, Point location) acc, Instruction cur)
		{
			switch (cur.Code)
			{
				case 'N':
					return (acc.waypoint, acc.location + Point.North * cur.Length);

				case 'S':
					return (acc.waypoint, acc.location + Point.South * cur.Length);

				case 'W':
					return (acc.waypoint, acc.location + Point.West * cur.Length);

				case 'E':
					return (acc.waypoint, acc.location + Point.East * cur.Length);

				case 'F':
					return (acc.waypoint, acc.location + acc.waypoint * cur.Length);

				case 'L':
					return (acc.waypoint.RotateLeft(cur.Length / 90), acc.location);

				case 'R':
					return (acc.waypoint.RotateRight(cur.Length / 90), acc.location);

				default:
					return acc;
			}
		}

		private static (Point waypoint, Point location) CalcB((Point waypoint, Point location) acc, Instruction cur)
		{
			switch (cur.Code)
			{
				case 'N':
					return (acc.waypoint + Point.North * cur.Length, acc.location);

				case 'S':
					return (acc.waypoint + Point.South * cur.Length, acc.location);

				case 'W':
					return (acc.waypoint + Point.West * cur.Length, acc.location);

				case 'E':
					return (acc.waypoint + Point.East * cur.Length, acc.location);

				case 'F':
					return (acc.waypoint, acc.location + acc.waypoint * cur.Length);

				case 'L':
					return (acc.waypoint.RotateLeft(cur.Length / 90), acc.location);

				case 'R':
					return (acc.waypoint.RotateRight(cur.Length / 90), acc.location);

				default:
					return acc;
			}
		}
	}
}
