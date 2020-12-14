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

		[Fact]
		public void TestOrMask()
		{
			var actual = DockingData.OrMaskFromString("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X");
			Assert.True(actual.Get(6));
			Assert.Equal(35, actual.AsEnumerable().Count( bit => bit == false));
		}

		[Fact]
		public void TestAndMask()
		{
			var actual = DockingData.AndMaskFromString("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X");
			Assert.True(actual.Get(1));
			Assert.Equal(35, actual.AsEnumerable().Count( bit => bit == false));
		}

		[Fact]
		public void TestFloatMask()
		{
			var actual = Mask.Create("00000000000000000000000000000000X0XX");

			var floatMask = actual.Floating;

			var floats = floatMask.AsEnumerable()
				.Zip(36.PowersOf2(), (bit,ary) => bit ? ary: 0)
				.Where( x => x > 0)
				.ToArray();


			var operators = floats.Aggregate( ImmutableList<int>.Empty,
				(acc,cur) => {
					if (acc.IsEmpty) {
						return acc.Add((int)cur).Add(0);
					} else {
						return acc.Aggregate( acc, (a,c) => a.Add(c | (int)cur));
					}
				})
				.Select( x => Convert.ToString(x,2))
				.ToArray();

			var n = Convert.ToUInt64(Math.Pow(2,floats.Length));

		}

		private IEnumerable<ulong> Foo(ulong n) {

			var x = 1ul;
			for(var cur=0ul; cur<n; cur++) {
				x = x ^ cur;
				yield return x;
			}
		}

		[Fact]
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


	}
}
