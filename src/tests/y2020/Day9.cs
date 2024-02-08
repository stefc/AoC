using Xunit;

using System.Linq;
using System;
using System.IO;
using advent.of.code.y2020.day9;

namespace advent.of.code.tests.y2020
{

	[Trait("Category", "y2020")]
	public class TestDay9
	{
		[Fact]
		public void PuzzleOne()
		{
			// Arrange
			var input = new long[]{
				35,20,15,25,47,40,62,55,65,95,102,117,150,182,127,219,299,277,309,576
			};

			// Act
			var actual = EncodingError.FirstInvalidNumber(input, 5);

			// Assert
			Assert.Equal(127, actual);
		}

		[Fact]
		public void PuzzlePartOne() {

			//  Arrange
			var input = File
				.ReadLines("tests/y2020/Day9.Input.txt")
				.Select( x => Convert.ToInt64(x));

			// Act
			var actual = EncodingError.FirstInvalidNumber(input, 25);

			// Assert
			Assert.Equal(2089807806, actual);
		}

		[Fact]
		public void PuzzleTwo()
		{
			// Arrange
			var input = new long[]{
				35,20,15,25,47,40,62,55,65,95,102,117,150,182,127,219,299,277,309,576
			};

			// Act
			var actual = EncodingError.FindWeakness(input, 127);

			// Assert
			Assert.Equal(62, actual);
		}

		[Fact]
		public void PuzzlePartTwo() {

			//  Arrange
			var input = File
				.ReadLines("tests/y2020/Day9.Input.txt")
				.Select( x => Convert.ToInt64(x));

			// Act
			var actual = EncodingError.FindWeakness(input, 2089807806);

			// Assert
			Assert.Equal(245848639, actual);
		}



	}
}
