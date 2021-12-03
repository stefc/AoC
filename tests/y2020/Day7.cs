using Xunit;

using System.Linq;
using System;
using System.IO;
using advent.of.code.y2020.day7;

namespace advent.of.code.tests.y2020
{

	[Trait("Category", "y2020")]
	public class TestDay7
	{
		[Fact]
		public void PuzzleOne()
		{
			// Arrange
			var input = new string[]{
"nop +0",
"acc +1",
"jmp +4",
"acc +3",
"jmp -3",
"acc -99",
"acc +1",
"jmp -4",
"acc +6"
		};

			// Act
				//	var actual = HandheldHalting.GetAccumulator(input);

			// Assert
				//	Assert.Equal(5, actual);
		}

		[Fact(Skip="Fehler")]
		public void PuzzlePartOne()
		{

			//  Arrange
			var input = File
				.ReadLines("tests/y2020/Day7.Input.txt");


			// Act
				//	var actual = HandheldHalting.GetAccumulator(input);

			// Assert
				//	Assert.Equal(12, actual);
		}

	}
}
