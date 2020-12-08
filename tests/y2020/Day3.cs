using Xunit;

using System.Linq;
using System.IO;
using advent.of.code.y2020.day3;
using advent.of.code.common;
using System.Collections.Generic;

namespace advent.of.code.tests.y2020
{

	[Trait("Category", "y2020")]
	public class TestDay3
	{
		[Fact]
		public void PuzzleOne()
		{
			// Arrange
			var input = CreateTestData();

			// Act
			var actual = TobogganTrajectory.HowManyTrees(input, new Point(3, 1));

			// Assert
			Assert.Equal(7, actual);
		}


		[Fact]
		public void PuzzlePartOne()
		{

			//  Arrange
			var input = File
				.ReadLines("tests/y2020/Day3.Input.txt")
				.ToList();

			// Act
			var actual = TobogganTrajectory.HowManyTrees(input, new Point(3, 1));

			// Assert
			Assert.Equal(148, actual);
		}


		[Fact]
		public void PuzzleTwo()
		{
			//  Arrange
			var input = CreateTestData();

			// Act
			var actual = TobogganTrajectory.HowManyTrees(input, CreateSlopes().ToArray());

			// Assert
			Assert.Equal(336, actual);
		}

		[Fact]
		public void PuzzlePartTwo()
		{
			//  Arrange
			var input = File
				.ReadLines("tests/y2020/Day3.Input.txt")
				.ToList();

			// Act
			var actual = TobogganTrajectory.HowManyTrees(input, CreateSlopes().ToArray());

			// Assert
			Assert.Equal(727923200, actual);
		}

		private IEnumerable<Point> CreateSlopes()
		=> new Point[] { new Point(1, 1), new Point(3, 1), new Point(5, 1), new Point(7, 1), new Point(1, 2) };

		private IEnumerable<string> CreateTestData()
		=> new string[]{
				"..##.......",
				"#...#...#..",
				".#....#..#.",
				"..#.#...#.#",
				".#...##..#.",
				"..#.##.....",
				".#.#.#....#",
				".#........#",
				"#.##...#...",
				"#...##....#",
				".#..#...#.#"
			};

	}
}
