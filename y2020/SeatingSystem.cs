using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using System;
using advent.of.code.common;

namespace advent.of.code.y2020.day11
{

	using Layout = ImmutableDictionary<Point, SeatingSystem.Seat>;

	// http://adventofcode.com/2020/day/11

	static class SeatingSystem
	{

		internal enum Seat
		{
			Empty,
			Occupied,
			Floor
		}

		public static Layout ToLayout(this IEnumerable<string> lines)
		=> lines
			.SelectMany((row, y) => row.Select((ch, x) => (coord: new Point(x, y), seat: ch.CharToSeat())))
			.Where(x => x.seat != Seat.Floor)
			.Aggregate(Layout.Empty,
				(accu, cur) => accu.Add(cur.coord, cur.seat));

		public static Layout ToLayout(this string input)
		=> input.Trim().Split('\n').ToArray().ToLayout();

		public static int Stabilize(Layout input)
			=> ProcessUntilStable(input, CountOccupies, 4).Last().CountOccupies();

		public static int StabilizeSeeing(Layout input)
			=> ProcessUntilStable(input, CountSightOccupied, 5).Last().CountOccupies();

		public static IEnumerable<Layout> ProcessUntilStable(Layout layout, Func<Layout, Point, int> countEmpty, int tolerance)
		{
			var lastOccupied = -1;
			var occupied = layout.CountOccupies();
			while (lastOccupied != occupied)
			{
				layout = Process(layout, countEmpty, tolerance);
				yield return layout;
				lastOccupied = occupied;
				occupied = layout.CountOccupies();
			}
		}

		private static Seat CharToSeat(this char ch)
		{
			switch (ch)
			{
				case 'L':
					return Seat.Empty;
				case '#':
					return Seat.Occupied;
				default:
					return Seat.Floor;
			}
		}

		private static Layout Process(Layout layout, Func<Layout, Point, int> countEmpty, int tolerance)
		 => layout.Aggregate(layout, (acc, cur) =>
		 {
			 if (cur.Value == Seat.Empty)
			 {
				 if (countEmpty(layout, cur.Key) == 0)
				 {
					 acc = acc.SetItem(cur.Key, Seat.Occupied);
				 }
			 }
			 else if (cur.Value == Seat.Occupied)
			 {
				 if (countEmpty(layout, cur.Key) >= tolerance)
				 {
					 acc = acc.SetItem(cur.Key, Seat.Empty);
				 }

			 };

			 return acc;
		 });

		private static int CountOccupies(this Layout layout)
			=> layout.Values.Count(seat => seat == Seat.Occupied);

		public static Point Dimensions(this Layout layout)
		=> layout.Keys.Aggregate(Point.Zero,
				(acc, cur) => (cur.X > acc.X) || (cur.Y > acc.Y) ?
				new Point(Math.Max(acc.X, cur.X), Math.Max(acc.Y, cur.Y)) : acc);


		public static int CountOccupies(Layout layout, Point at)
		{
			var adjacents = new Point[]{
				Point.North,Point.South,Point.East,Point.West,
				Point.NorthWest,Point.NorthEast,Point.SouthWest,Point.SouthEast
			};

			return adjacents.Count(cur => layout.TryGetValue(at + cur, out var seat) && (seat == Seat.Occupied));

		}

		public static int CountSightOccupied(Layout layout, Point at)
		{
			var adjacents = new Point[]{
				Point.North,Point.South,Point.East,Point.West,
				Point.NorthWest,Point.NorthEast,Point.SouthWest,Point.SouthEast
			};

			Point dimensions = layout.Dimensions();

			var occupied = adjacents.Select(cur =>
			   Enumerable
				   .Range(1, Math.Max(dimensions.X + 1, dimensions.Y + 1))
				   .Select(i => at + (cur * i))
				   .Where(pt => pt.X >= 0 && pt.Y >= 0 && pt.X <= dimensions.X && pt.Y <= dimensions.Y)
				   .Select(pt => layout.GetValueOrDefault(pt, Seat.Floor))
				   .Where( seat => seat != Seat.Floor)
				   .FirstOrDefault() == Seat.Occupied);
			return occupied.Count(isOccupied => isOccupied);
		}

		public static bool IsPointOnLine((Point start, Point end) line, ISet<Point> points)
		=> points
			.Except(line.AsEnumerable())
			.Any(target => target.HitTest(line));
	}
}
