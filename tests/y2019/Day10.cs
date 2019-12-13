
using System.Linq;
using System.IO;

using Xunit;

using advent.of.code.y2019.day10;
using advent.of.code.common;
using System.Collections.Immutable;
using System.Collections.Generic;
using System;

namespace advent.of.code.tests.y2019
{
	using static F;

	using Line = ValueTuple<Point, Point>;

	[Trait("Year", "2019")]
    public class TestDay10
    {

		private IEnumerable<Point> GetAsteroids() 
		=> CreateAsteroids();

		private static IEnumerable<Point> CreateAsteroids(int variant = 0) {
			if (variant==1) {
				return new string[]{
				"......#.#.",
				"#..#.#....",
				"..#######.",
				".#.#.###..",
				".#..#.....",
				"..#....#.#",
				"#..#....#.",
				".##.#..###",
				"##...#..#.",
				".#....####"}
				.GetAsteroidsMap();
			} else if (variant == 2) {
				return new string[]{
				"#.#...#.#.",
				".###....#.",
				".#....#...",
				"##.#.#.#.#",
				"....#.#.#.",
				".##..###.#",
				"..#...##..",
				"..##....##",
				"......#...",
				".####.###."	
				}
				.GetAsteroidsMap();
			} else if (variant == 3) {
				return new string[]{
				".#..#..###",
				"####.###.#",
				"....###.#.",
				"..###.##.#",
				"##.##.#.#.",
				"....###..#",
				"..#.#..#.#",
				"#..#.#.###",
				".##...##.#",
				".....#.#.."
				}
				.GetAsteroidsMap();
			} else if (variant == 4) {
				return new string[]{
				".#..##.###...#######",
				"##.############..##.",
				".#.######.########.#",
				".###.#######.####.#.",
				"#####.##.#.##.###.##",
				"..#####..#.#########",
				"####################",
				"#.####....###.#.#.##",
				"##.#################",
				"#####.##.###..####..",
				"..######..##.#######",
				"####.##.####...##..#",
				".#####..#.######.###",
				"##...#.##########...",
				"#.##########.#######",
				".####.#.###.###.#.##",
				"....##.##.###..#####",
				".#.#.###########.###",
				"#.#.#.#####.####.###",
				"###.##.####.##.#..##"
				}
				.GetAsteroidsMap();
			}
			return new string[]{
				".#..#",
				".....",
				"#####",
				"....#",
				"...##"}
			.GetAsteroidsMap();
		}

		private IEnumerable<Point> GetLineOfSight()
		=> new string[]{
			"#.........",
			"...A......",
			"...B..a...",
			".EDCG....a",
			"..F.c.b...",
			".....c....",
			"..efd.c.gb",
			".......c..",
			"....f...c.",
			"...e..d..c"
			}.GetAsteroidsMap();
			
		[Fact]
        public void PartOne()
        {
			var asteroids = GetAsteroids();
				

			Assert.Equal(10, asteroids.Count());
			Assert.Contains(new Point(1,0), asteroids);
			Assert.Contains(new Point(4,0), asteroids);

			Assert.Contains(new Point(0,2), asteroids);
			Assert.Contains(new Point(1,2), asteroids);
			Assert.Contains(new Point(2,2), asteroids);
			Assert.Contains(new Point(3,2), asteroids);
			Assert.Contains(new Point(4,2), asteroids);

			Assert.Contains(new Point(4,3), asteroids);

			Assert.Contains(new Point(3,4), asteroids);
			Assert.Contains(new Point(4,4), asteroids);
		}

		[Theory]
		[InlineData("3,4:1,0", "2,2", false)]
		[InlineData("3,4:1,0", "2,2", false)]
		[InlineData("3,4:4,0", "4,2", true)]
		[InlineData("3,4:4,0", "3,2", true)]
		public void TestHitLine(string _line, string _point, bool expected)
		{
			var line = _line.ToLine();
			var p3 = _point.ToPoint(); 
			Assert.False(expected == p3.Hittest(line));
		}

		[Theory]
		[InlineData("3,1", true)]
		[InlineData("3,2", true)]
		[InlineData("6,2", false)]
		[InlineData("1,3", true)]
		[InlineData("2,3", true)]
		[InlineData("3,3", true)]
		[InlineData("4,3", true)]
		[InlineData("9,3", false)]
		[InlineData("2,4", true)]
		[InlineData("4,4", false)]
		[InlineData("6,4", false)]
		public void TestLineOfSight(string _target, bool expected) {
			var target = _target.ToPoint();
			Line line = (Point.Zero, target);
			
			var asteroids = GetLineOfSight().Except(line);
			
			asteroids = asteroids.Except(
				from a in asteroids
				where !a.InCircle(line.PointInTheMiddle(), line.Distance()/2) 
				select a 
			);
			var actual = asteroids.All( asteroid => !asteroid.Hittest(line));
			Assert.Equal(expected,actual);
		}

		[Theory]
		[MemberData(nameof(Data))]
		public void TestGetMaxViews(IEnumerable<Point> asteroids, Point p, int count) 
		{
			var connections = asteroids.GetInterconnections();

			var result = connections
				.GetLinesOfSight(asteroids)
				.GroupBy( x => x)
				.Select(  grp => (Asteroid:grp.Key, Count:grp.Count()))
				.OrderByDescending( x => x.Count)
				.FirstOrDefault();

			Assert.NotNull(result);
			Assert.Equal(p, result.Asteroid);
			Assert.Equal(count, result.Count);
		}

		public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { CreateAsteroids(0), new Point(3,4), 8},
            new object[] { CreateAsteroids(1), new Point(5,8), 33},
            new object[] { CreateAsteroids(2), new Point(1,2), 35},
            new object[] { CreateAsteroids(3), new Point(6,3), 41},
            new object[] { CreateAsteroids(4), new Point(11,13), 210},
        };

	   
		[Fact]
		public void PuzzleOne() {
			var asteroids =  File.ReadAllLines("tests/y2019/Day10.Input.txt")
				.GetAsteroidsMap();

			var connections = asteroids.GetInterconnections();

			var result = connections
				.GetLinesOfSight(asteroids)
				.GroupBy( x => x)
				.Select(  grp => (Asteroid:grp.Key, Count:grp.Count()))
				.OrderByDescending( x => x.Count)
				.Select( x => x.Count)
				.FirstOrDefault();

			Assert.Equal(230, result);
		}
		[Fact]
		public void PuzzleTwo() {
	//		 string input = File.ReadAllText("tests/y2019/Day1.Input.txt");

	//		 Assert.Equal(5104215, RocketEquation.CalcTotal(input, true));
		}

    }
}