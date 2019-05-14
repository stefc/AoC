/*
Because your neighbors keep defeating you in the holiday house decorating contest year after year, you've decided to deploy one million lights in a 1000x1000 grid.

Furthermore, because you've been especially nice this year, Santa has mailed you instructions on how to display the ideal lighting configuration.

Lights in your grid are numbered from 0 to 999 in each direction; the lights at each corner are at 0,0, 0,999, 999,999, and 999,0. The instructions include whether to turn on, turn off, or toggle various inclusive ranges given as coordinate pairs. Each coordinate pair represents opposite corners of a rectangle, inclusive; a coordinate pair like 0,0 through 2,2 therefore refers to 9 lights in a 3x3 square. The lights all start turned off.

To defeat your neighbors this year, all you have to do is set up your lights by doing the instructions Santa sent you in order.

For example:

turn on 0,0 through 999,999 would turn on (or leave on) every light.
toggle 0,0 through 999,0 would toggle the first line of 1000 lights, turning off the ones that were on, and turning on the ones that were off.
turn off 499,499 through 500,500 would turn off (or leave off) the middle four lights.
After following the instructions, how many lights are lit?
*/

// http://adventofcode.com/2015/day/5

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using advent.of.code.common;
using System.Text.RegularExpressions;
using System.Numerics;
using System.Collections;

namespace advent.of.code.y2015.day6
{

	static class FireHazard
	{

		public static Statement Parse(string statement)
		{
			return new Statement(Command.TurnOn, Point.Zero, new Point(999, 999));
		}

		internal enum Command
		{
			TurnOn,
			TurnOff,
			Toggle
		}
		internal class Statement
		{
			public Command Command { get; private set; }
			public Point From { get; private set; }
			public Point Through { get; private set; }

			public Statement(Command command, Point from, Point through)
			{
				this.Command = command;
				this.From = from;
				this.Through = through;
			}

			public static Statement FromString(string code)
			{
				const string pattern = @"(?'command'turn on|turn off|toggle) (?'from'\d{1,3},\d{1,3}) through (?'to'\d{1,3},\d{1,3})";

				RegexOptions options = RegexOptions.IgnoreCase;

				Match match = Regex.Matches(code, pattern, options).First();


				Group commandGroup = match.Groups["command"];
				Group fromGroup = match.Groups["from"];
				Group toGroup = match.Groups["to"];

				Command command = commandGroup.Value.Contains("toggle") ?
					Command.Toggle : commandGroup.Value.Contains("off") ?
					Command.TurnOff : Command.TurnOn;

				Point from = Point.FromString(fromGroup.Value);
				Point to = Point.FromString(toGroup.Value);

				return new Statement(command, from, to);
			}

		}
			public class LightGrid {
				private readonly int width;
				private readonly int height;

				private BitArray bitmap;

				public LightGrid(int width, int height):
					this(width, height, new BitArray(width*height, false))
				{
				}

				internal LightGrid(int width, int height, BitArray bitmap) {
					this.width = width;
					this.height = height;

					this.bitmap = bitmap;
				}

				public int LightCount {
					get {
						return Enumerable
							.Range(0,width*height)
							.Select( index => bitmap.Get(index))
							.Where( bit => bit )
							.Count();
					}
				}

			private LightGrid GridOperation(Point from, Point to, Func<BitArray,int,BitArray> operation)
			{
				var indices = Enumerable
					.Range(Math.Min(from.Y,to.Y), Math.Abs(to.Y-from.Y)+1)
					.SelectMany( y => Enumerable
						.Range(Math.Min(from.X,to.X), Math.Abs(to.X-from.Y)+1)
						.Select( x => y * this.width + x));

				var newBitmap = indices.Aggregate(
					seed: new BitArray(this.bitmap),
					func: (bits, index) => operation(bits, index));

				return new LightGrid(this.width, this.height, newBitmap);
			}

			internal LightGrid TurnOff(Point from, Point to)
			{
				return GridOperation(from, to, OperationClear);
			}
			internal LightGrid TurnOn(Point from, Point to)
			{
				return GridOperation(from, to, OperationSet);
			}

			internal LightGrid Toggle(Point from, Point to)
			{
				return GridOperation(from, to, OperationToggle);
			}

			private static BitArray OperationSet(BitArray bits, int index)
			{
				bits.Set(index, true);
				return bits;
			}
			private static BitArray OperationClear(BitArray bits, int index)
			{
				bits.Set(index, false);
				return bits;
			}
			private static BitArray OperationToggle(BitArray bits, int index)
			{
				bits.Set(index, !bits.Get(index));
				return bits;
			}
		}
	}
}