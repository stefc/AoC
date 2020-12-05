using Xunit;

using System.Linq;
using System;
using System.IO;
using advent.of.code.y2020.day1;

namespace advent.of.code.tests.y2020
{

	[Trait("Category","2020")]
	public class TestDay1
	{
		[Fact]
		public void PuzzleOne()
		{
			// Arrange
			var input = new int[]{
				1721,979,366,299,675,1456
			};

			// Act
			var actual = ReportRepair.MultiplyNumbers(input, 2020, 2);

			// Assert
			Assert.Equal(514579, actual);
		}

		[Fact]
		public void PuzzlePartOne() {

			//  Arrange
			var input = File
				.ReadLines("tests/y2020/Day1.Input.txt")
				.Select( x => Convert.ToInt32(x));

			// Act
			var actual = ReportRepair.MultiplyNumbers(input, 2020, 2);

			// Assert
			Assert.Equal(1020036, actual);
		}

		[Fact]
		public void PuzzleTwo()
		{
			// Arrange
			var input = new int[]{
				1721,979,366,299,675,1456
			};

			// Act
			var actual = ReportRepair.MultiplyNumbers(input, 2020, 3);

			// Assert
			Assert.Equal(241861950, actual);
		}

		[Fact]
		public void PuzzlePartTwo() {

			//  Arrange
			var input = File
				.ReadLines("tests/y2020/Day1.Input.txt")
				.Select( x => Convert.ToInt32(x));

			// Act
			var actual = ReportRepair.MultiplyNumbers(input, 2020, 3);

			// Assert
			Assert.Equal(286977330, actual);
		}


	}
}