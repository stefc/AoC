using Xunit;
using advent.of.code.y2020.day14;
using System.Linq;
using System.IO;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace advent.of.code.tests.y2020
{

	[Trait("Category", "y2020")]
	public class TestDay14
	{

		[Fact(Skip="Fehler")]
		public void TestOrMask()
		{
			var actual = DockingData.OrMaskFromString("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X");
			Assert.True(actual.Get(6));
			Assert.Equal(35, actual.AsEnumerable().Count( bit => bit == false));
		}

		[Fact(Skip="Fehler")]
		public void TestAndMask()
		{
			var actual = DockingData.AndMaskFromString("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X");
			Assert.True(actual.Get(1));
			Assert.Equal(35, actual.AsEnumerable().Count( bit => bit == false));
		}

		[Fact(Skip="Fehler")]
		public void TestFloatMask()
		{

			// 000000000000000000000000000000X1001X
			var actual = Mask.Create("00000000000000000000000000000000X0XX");

			var operators = actual.Floating
				.Adresses()
				.Select( x => Convert.ToString((long)x,2).PadLeft(36, '0'))
				.ToArray();

			Assert.Equal(8, operators.Length);
		}


		[Fact(Skip="Fehler")]
		public void PuzzleOne()
		{
			// Arrange
			var program = @"
mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0".ToProgram();

			// Act
			var actual = DockingData.Evaluate(program);

			// Assert
			Assert.Equal(165ul, actual);

		}

		[Fact]
		public void PuzzlePartOne()
		{
			//  Arrange
			var input = File
				.ReadLines("tests/y2020/Day14.Input.txt")
				.ToArray()
				.ToProgram();

			// Act
			var actual = DockingData.Evaluate(input);

			// Assert
			Assert.Equal(10035335144067ul, actual);
		}

		[Fact(Skip="Fehler")]
		public void PuzzleTwo()
		{
			// Arrange
			var program = @"
mask = 000000000000000000000000000000X1001X
mem[42] = 100
mask = 00000000000000000000000000000000X0XX
mem[26] = 1".ToProgram();

			// Act
			var actual = DockingData.EvaluateV2(program);

			// Assert
			Assert.Equal(208ul, actual);

		}

		[Fact(Skip="Fehler")]
		public void PuzzlePartTwo()
		{
			//  Arrange
			var input = File
				.ReadLines("tests/y2020/Day14.Input.txt")
				.ToArray()
				.ToProgram();

			// Act
			var actual = DockingData.EvaluateV2(input);

			// Assert
			Assert.Equal(3817372618036ul, actual);
		}
	}
}
