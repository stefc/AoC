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

		public static int Stabilize(IEnumerable<string> lines)
			=> ProcessUntilStable(lines.ToLayout(), CountOccupies).Last().CountOccupies();

		public static int StabilizeSeeing(IEnumerable<string> lines)
			=> ProcessUntilStable(lines.ToLayout(), CountSightSeeings).Last().CountOccupies();

		private static IEnumerable<Layout> ProcessUntilStable(Layout layout, Func<Layout, Point, int> count)
		{
			var lastOccupied = -1;
			var occupied = layout.CountOccupies();
			while (lastOccupied != occupied)
			{
				layout = Process(layout, count);
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

		private static Layout Process(Layout layout, Func<Layout, Point, int> count)
		 => layout.Aggregate(layout, (acc, cur) =>
		 {
			 if (cur.Value == Seat.Empty)
			 {
				 if (count(layout, cur.Key) == 0)
				 {
					 acc = acc.SetItem(cur.Key, Seat.Occupied);
				 }
			 }
			 else if (cur.Value == Seat.Occupied)
			 {
				 if (count(layout, cur.Key) >= 4)
				 {
					 acc = acc.SetItem(cur.Key, Seat.Empty);
				 }

			 };

			 return acc;
		 });

		private static int CountOccupies(this Layout layout)
			=> layout.Values.Count(seat => seat == Seat.Occupied);

		public static int CountOccupies(Layout layout, Point at)
		{

			var adjacents = new Point[]{
				Point.North,Point.South,Point.East,Point.West,
				Point.NorthWest,Point.NorthEast,Point.SouthWest,Point.SouthEast
			};

			return adjacents.Count(cur => layout.TryGetValue(at + cur, out var seat) && (seat == Seat.Occupied));

		}

		public static int CountSightSeeings(Layout layout, Point at)
		{

			var allSeats = layout
				.Select(kvp => kvp.Key)
				.Where(pt => !pt.Equals(at))
				.Where(pt => pt.IsAdjacent(at))
				.ToImmutableHashSet();

			var lines = allSeats
				.Select(pt => (start: pt, end: at));

			return lines.Count(line => !IsPointOnLine(line, allSeats));
		}

		public static bool IsPointOnLine((Point start, Point end) line, ISet<Point> points)
		=> points
			.Except(line.AsEnumerable())
			.Any(target => target.HitTest(line));
	}
}
