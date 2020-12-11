using Xunit;

using System.Linq;
using System;
using System.IO;
using advent.of.code.y2020.day11;

namespace advent.of.code.tests.y2020
{

	[Trait("Category", "y2020")]
	public class TestDay11
	{
		[Fact]
		public void PuzzleOne()
		{
			// Arrange
			var input = new string[]{
"L.LL.LL.LL",
"LLLLLLL.LL",
"L.L.L..L..",
"LLLL.LL.LL",
"L.LL.LL.LL",
"L.LLLLL.LL",
"..L.L.....",
"LLLLLLLLLL",
"L.LLLLLL.L",
"L.LLLLL.LL"
			};

			// Act
			var actual = SeatingSystem.Stabilize(input);

			// Assert
			Assert.Equal(37, actual);
		}

		[Fact]
		public void PuzzlePartOne() {

			//  Arrange
			var input = File
				.ReadLines("tests/y2020/Day11.Input.txt");

			// Act
			var actual = SeatingSystem.Stabilize(input);

			// Assert
			Assert.Equal(2361, actual);
		}


	}
}
