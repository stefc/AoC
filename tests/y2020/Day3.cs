using Xunit;

using System.Linq;
using System.IO;
using advent.of.code.y2020.day3;
using advent.of.code.common;

namespace advent.of.code.tests.y2020
{

	[Trait("Category","y2020")]
	public class TestDay3
	{
		[Fact]
		public void PuzzleOne()
		{
			// Arrange
			var input = new string[]{
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

			// Act
			var actual = TobogganTrajectory.HowManyTrees(input, new Point(3,1));

			// Assert
			Assert.Equal(7, actual);
		}

		[Fact]
		public void PuzzlePartOne() {

			//  Arrange
			var input = File
				.ReadLines("tests/y2020/Day3.Input.txt")
				.ToList();

			// Act
			var actual = TobogganTrajectory.HowManyTrees(input, new Point(3,1));

			// Assert
			Assert.Equal(148, actual);
		}



		[Theory]
		//[InlineData(1,1,2)]
		[InlineData(3,1,7)]
		//[InlineData(5,1,3)]
		//[InlineData(7,1,4)]
		//[InlineData(1,2,2)]

		public void PuzzleTwo(int x, int y, int expected)
		{
			var input = new string[]{
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
			// Act
			var actual = TobogganTrajectory.HowManyTrees(input, new Point(x,y));


			// Assert
			Assert.Equal(expected, actual);
		}

		// [Fact]
		// public void PuzzlePartTwo() {

		// 	//  Arrange
		// 	var input = File
		// 		.ReadLines("tests/y2020/Day2.Input.txt")
		// 		.ToList();

		// 	// Act
		// 	var actual = PasswordPhilosophy.AreValidOccurence(input);

		// 	// Assert
		// 	Assert.Equal(562, actual);
		// }


	}
}
