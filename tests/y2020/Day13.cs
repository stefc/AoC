using Xunit;
using System.IO;
using advent.of.code.y2020.day13;

namespace advent.of.code.tests.y2020
{

	[Trait("Category", "y2020")]
	public class TestDay13
	{
		[Fact]
		public void PuzzleOne()
		{
			// Arrange

			// Act
			var actual = ShuttleSearch.EarliestBusMultiplyWaitingTime(939, "7,13,x,x,59,x,31,19");

			// Assert
			Assert.Equal(295, actual);
		}

		[Fact]
		public void PuzzlePartOne()
		{

			// Act
			var actual = ShuttleSearch.EarliestBusMultiplyWaitingTime(
				1002632,
				"23,x,x,x,x,x,x,x,x,x,x,x,x,41,x,x,x,x,x,x,x,x,x,829,x,x,x,x,x,x,x,x,x,x,x,x,13,17,x,x,x,x,x,x,x,x,x,x,x,x,x,x,29,x,677,x,x,x,x,x,37,x,x,x,x,x,x,x,x,x,x,x,x,19");

			// Assert
			Assert.Equal(3385, actual);
		}

		[Fact]
		public void PuzzlePartTwo()
		{
			var actual = ShuttleSearch.WolframAlpha(
				"23,x,x,x,x,x,x,x,x,x,x,x,x,41,x,x,x,x,x,x,x,x,x,829,x,x,x,x,x,x,x,x,x,x,x,x,13,17,x,x,x,x,x,x,x,x,x,x,x,x,x,x,29,x,677,x,x,x,x,x,37,x,x,x,x,x,x,x,x,x,x,x,x,19"
			);

			Assert.Equal("(t+0) mod 23 = 0,(t+13) mod 41 = 0,(t+23) mod 829 = 0,(t+36) mod 13 = 0,(t+37) mod 17 = 0,(t+52) mod 29 = 0,(t+54) mod 677 = 0,(t+60) mod 37 = 0,(t+73) mod 19 = 0", actual);
			// -> "t = 2384517360007913 n + 600689120448303, n element Z"
		}
	}
}
