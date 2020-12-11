using System.Linq;
using System.Collections.Generic;
using Combinatorics.Collections;
using System.Collections.Immutable;
using System;
using advent.of.code.common;

namespace advent.of.code.y2020.day11
{

	using Layout = ImmutableDictionary<Point,SeatingSystem.Seat>;
	// http://adventofcode.com/2020/day/11

	static class SeatingSystem
	{

		internal enum Seat {
			Empty,
			Occupied,
			Floor
		}

		public static int Stabilize(IEnumerable<string> lines) {

			var seatLayout = lines
				.SelectMany( (row, y) => row.Select( (ch, x) => ( coord: new Point(x,y), seat: ch.CharToSeat())))
				.Where( x => x.seat != Seat.Floor)
				.Aggregate(Layout.Empty,
					(accu, cur) => accu.Add( cur.coord, cur.seat));



			var lastLayout = ProcessUntilStable(seatLayout).Last();

			return lastLayout.CountOccupies();
		}

		private static IEnumerable<Layout> ProcessUntilStable(Layout layout)
		{
			var lastOccupied = -1;
			var occupied = layout.CountOccupies();
			while (lastOccupied != occupied) {
				layout = Process(layout);
				yield return layout;
				lastOccupied = occupied;
				occupied = layout.CountOccupies();
			}
		}

		private static Seat CharToSeat(this char ch) {
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

		private static Layout Process(Layout layout) {

			var newLayout = layout.Aggregate(layout, (acc, cur) => {
				if (cur.Value == Seat.Empty) {
					if (CountOccupies(layout, cur.Key) == 0) {
						acc = acc.SetItem(cur.Key, Seat.Occupied);
					}
				} else if (cur.Value == Seat.Occupied) {
					if (CountOccupies(layout, cur.Key) >= 4) {
						acc = acc.SetItem(cur.Key, Seat.Empty);
					}

				} else throw new ArgumentException();

				return acc;
			});

			return newLayout;
		}

		private static int CountOccupies(this Layout layout)
			=> layout.Values.Count( seat => seat == Seat.Occupied);


		private static int CountOccupies(Layout layout, Point at) {

			var adjacents = new Point[]{
				Point.North,
				Point.South,
				Point.East,
				Point.West,
				new Point(+1,+1),
				new Point(-1,-1),
				new Point(+1,-1),
				new Point(-1,+1)
			};

			return adjacents.Count( cur => layout.TryGetValue(at + cur, out var seat) && (seat == Seat.Occupied));

		}
	}
}
