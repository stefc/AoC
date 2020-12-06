using Xunit;

using System.Linq;
using System;
using System.IO;
using advent.of.code.y2020.day2;

namespace advent.of.code.tests.y2020
{

	[Trait("Category","y2020")]
	public class TestDay2
	{
		[Fact]
		public void PuzzleOne()
		{
			// Arrange
			var input = new string[]{
				"1-3 a: abcde",
				"1-3 b: cdefg",
				"2-9 c: ccccccccc"
			};

			// Act
			var actual = PasswordPhilosophy.AreValid(input);

			// Assert
			Assert.Equal(2, actual);
		}

		[Fact]
		public void PuzzlePartOne() {

			//  Arrange
			var input = File
				.ReadLines("tests/y2020/Day1.Input.txt")
				.Select( x => Convert.ToInt32(x));

			// Act
			//var actual = ReportRepair.MultiplyNumbers(input, 2020, 2);

			// Assert
			//Assert.Equal(1020036, actual);
		}

		[Fact]
		public void PuzzleTwo()
		{
			// Arrange
			var input = new int[]{
				1721,979,366,299,675,1456
			};

			// Act
			//var actual = ReportRepair.MultiplyNumbers(input, 2020, 3);

			// Assert
			//Assert.Equal(241861950, actual);
		}

		[Fact]
		public void PuzzlePartTwo() {

			//  Arrange
			var input = File
				.ReadLines("tests/y2020/Day1.Input.txt")
				.Select( x => Convert.ToInt32(x));

			// Act
			//var actual = ReportRepair.MultiplyNumbers(input, 2020, 3);

			// Assert
			//Assert.Equal(286977330, actual);
		}


	}
}