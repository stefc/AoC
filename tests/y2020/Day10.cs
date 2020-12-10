using Xunit;

using System.Linq;
using System;
using System.IO;
using advent.of.code.y2020.day10;

namespace advent.of.code.tests.y2020
{

	[Trait("Category", "y2020")]
	public class TestDay10
	{
		[Fact]
		public void PuzzleOne()
		{
			// Arrange
			var input = new int[]{
				16,10,15,5,1,11,7,19,6,12,4
			};

			// Act
			var actual = AdapterArray.CountJolt(input);

			// Assert
			Assert.Equal(5*7, actual);
		}

		[Fact]
		public void PuzzlePartOne() {

			//  Arrange
			var input = File
				.ReadLines("tests/y2020/Day10.Input.txt")
				.Select( x => Convert.ToInt32(x));

			// Act
			var actual = AdapterArray.CountJolt(input);

			// Assert
			Assert.Equal(1998, actual);
		}

		[Fact]
		public void PuzzleTwo()
		{
			// Arrange
			var input = new int[]{
				// 16,10,15,5,1,11,7,19,6,12,4

				28,33,18,42,31,14,46,20,48,47,24,23,49,45,19,38,39,11,1,32,25,35,8,17,7,9,4,2,34,10,3
			};

			// ****-****--***-**--****-*--****
			//   4    4    3   2    4  1    4
			//   7  x 7  x 4 x 2  x 7  x 7

			// Act
			var actual = AdapterArray.CountUnique(input);
			// Assert
			Assert.Equal(19208, actual);
		}

		[Fact]
		public void PuzzlePartTwo() {

			//  Arrange
			var input = File
				.ReadLines("tests/y2020/Day10.Input.txt")
				.Select( x => Convert.ToInt32(x));

			// Act
			var actual = AdapterArray.CountUnique(input);

			// Assert
			Assert.Equal(2107417088L, actual);
		}

	}
}
