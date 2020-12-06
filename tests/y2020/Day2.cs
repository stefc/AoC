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
				"1-3 b: cdefg",
				"1-3 a: abcde",
				"2-9 c: ccccccccc"
			};

			// Act
			var actual = PasswordPhilosophy.AreValidCount(input);

			// Assert
			Assert.Equal(2, actual);
		}

		[Fact]
		public void PuzzlePartOne() {

			//  Arrange
			var input = File
				.ReadLines("tests/y2020/Day2.Input.txt")
				.ToList();

			// Act
			var actual = PasswordPhilosophy.AreValidCount(input);

			// Assert
			Assert.Equal(398, actual);
		}

		[Fact]
		public void PuzzleTwo()
		{
			var input = new string[]{
				"1-3 b: cdefg",
				"1-3 a: abcde",
				"2-9 c: ccccccccc"
			};

			// Act
			var actual = PasswordPhilosophy.AreValidOccurence(input);

			// Assert
			Assert.Equal(1, actual);
		}

		[Fact]
		public void PuzzlePartTwo() {

			//  Arrange
			var input = File
				.ReadLines("tests/y2020/Day2.Input.txt")
				.ToList();

			// Act
			var actual = PasswordPhilosophy.AreValidOccurence(input);

			// Assert
			Assert.Equal(562, actual);
		}


	}
}
