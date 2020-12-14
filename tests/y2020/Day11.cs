using Xunit;

using System.Linq;
using System.IO;
using advent.of.code.y2020.day11;
using advent.of.code.common;
using System.Collections.Immutable;
using System;

namespace advent.of.code.tests.y2020
{
	using Layout = ImmutableDictionary<Point, SeatingSystem.Seat>;

	[Trait("Category", "y2020")]
	public class TestDay11
	{
		[Fact]
		public void PuzzleOne()
		{
			// Arrange
			var input = CreateLayout(0);

			// Act
			var actual = SeatingSystem.Stabilize(input);

			// Assert
			Assert.Equal(37, actual);
		}

		[Fact]
		public void PuzzlePartOne() {

			//  Arrange
			var input = File
				.ReadLines("tests/y2020/Day11.Input.txt")
				.ToLayout();

			// Act
			var actual = SeatingSystem.Stabilize(input);

			// Assert
			Assert.Equal(2361, actual);
		}

		[Theory]
		[InlineData(1,"3,4",8)]
		[InlineData(2,"1,1",1)]
		[InlineData(3,"3,3",0)]
		public void SightSeeing(int variant, string point, int expected) {
			var layout = CreateLayout(variant);
			var at = Point.FromString(point);
			var actual = SeatingSystem.CountSightSeeings(layout, at);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void PuzzleTwo()
		{
			// Arrange
			var input = CreateLayout(0);

			// Act
			var next = SeatingSystem.ProcessUntilStable(input, SeatingSystem.CountSightSeeings,
				SeatingSystem.CountSightEmpty, 5).Take(3).ToArray();

			File.WriteAllText("next01.txt", Visualize(next[0]));
			File.WriteAllText("next02.txt", Visualize(next[1]));
			File.WriteAllText("next03.txt", Visualize(next[2]));

			var actual = SeatingSystem.StabilizeSeeing(input);





			// Assert
			Assert.Equal(26, actual);
		}

		// [Fact]
		// public void PuzzlePartTwo() {

		// 	//  Arrange
		// 	var input = File
		// 		.ReadLines("tests/y2020/Day11.Input.txt");

		// 	// Act
		// 	var actual = SeatingSystem.StabilizeSeeing(input);

		// 	// Assert
		// 	Assert.Equal(2361, actual);
		// }

		[Fact]
		public void IsPointOnLine()
		{
			var points = new Point[]{
				new Point(0,7),
				new Point(1,2),
				new Point(2,4),
				new Point(3,8),
				new Point(3,1),
				new Point(4,5),
				new Point(7,0),
				new Point(8,4)
			};

			var at = new Point(3,4);

			var allPoints = points.ToHashSet();


			Assert.False(SeatingSystem.IsPointOnLine(
				(at,points[0]), allPoints));
			Assert.False(SeatingSystem.IsPointOnLine(
				(at,points[1]), allPoints));
			Assert.False(SeatingSystem.IsPointOnLine(
				(at,points[2]), allPoints));
			Assert.False(SeatingSystem.IsPointOnLine(
				(at,points[3]), allPoints));
			Assert.False(SeatingSystem.IsPointOnLine(
				(at,points[4]), allPoints));
			Assert.False(SeatingSystem.IsPointOnLine(
				(at,points[5]), allPoints));
			Assert.False(SeatingSystem.IsPointOnLine(
				(at,points[6]), allPoints));
		}


		private static Layout CreateLayout(int variant = 0)
		{
			if (variant == 0) { return @"
L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL".ToLayout();
			} else if (variant == 1) { return @"
.......#.
...#.....
.#.......
.........
..#L....#
....#....
.........
#........
...#.....".ToLayout();
			} else if (variant == 2) { return @"
.............
.L.L.#.#.#.#.
.............".ToLayout();
			} else if (variant == 3) { return @"
.##.##.
#.#.#.#
##...##
...L...
##...##
#.#.#.#
.##.##.".ToLayout();
			};
			return Layout.Empty;
		}

		private string Visualize(Layout layout) {
			Point dimensions = layout.Dimensions();

			//layout = layout.RemoveRange( layout.Where( kvp => kvp.Value != SeatingSystem.Seat.Floor).Select( kvp => kvp.Key));

			var lines = Enumerable.Range(0, dimensions.Y+1)
				.Select( y =>
					String.Concat(Enumerable.Range(0, dimensions.X+1)
						.Select( x => !layout.TryGetValue(new Point(x,y), out var seat) ? '.' :
							( seat == SeatingSystem.Seat.Empty ? 'L' : '#' ))));

			return String.Join('\n', lines);
		}



	}
}
