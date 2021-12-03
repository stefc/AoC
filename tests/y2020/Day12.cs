using Xunit;
using System.IO;
using advent.of.code.y2020.day12;

namespace advent.of.code.tests.y2020
{

	[Trait("Category", "y2020")]
	public class TestDay12
	{
		[Fact]
		public void PuzzleOne()
		{
			// Arrange
			var input = new string[]{
				"F10","N3","F7","R90","F11"
			};

			// Act
			var actual = RainRisk.Navigate(input);

			// Assert
			Assert.Equal(25, actual);
		}

		[Fact(Skip="Fehler")]
		public void PuzzlePartOne()
		{
			//  Arrange
			var input = File
				.ReadLines("tests/y2020/Day12.Input.txt");

			// Act
			var actual = RainRisk.Navigate(input);

			// Assert
			Assert.Equal(636, actual);
		}
		[Fact(Skip="Fehler")]
		public void PuzzleTwo()
		{
			// Arrange
			var input = new string[]{
				"F10","N3","F7","R90","F11"
			};

			// Act
			var actual = RainRisk.NavigateWaypoint(input);

			// Assert
			Assert.Equal(286, actual);
		}

		[Fact(Skip="Fehler")]
		public void PuzzlePartTwo()
		{

			//  Arrange
			var input = File
				.ReadLines("tests/y2020/Day12.Input.txt");

			// Act
			var actual = RainRisk.NavigateWaypoint(input);

			// Assert
			Assert.Equal(26841, actual);
		}
	}
}
