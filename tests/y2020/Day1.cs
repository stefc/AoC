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

			var input = new int[]{
				1721,979,366,299,675,1456
			};

			var actual = ReportRepair.Multiply(input, 2020);
			Assert.Equal(514579, actual);
		}

		[Fact]
		public void PuzzlePartOne() {
			var input = File
				.ReadLines("tests/y2020/Day1.Input.txt")
				.Select( x => Convert.ToInt32(x));

			var actual = ReportRepair.Multiply(input, 2020);

			Assert.Equal(1020036, actual);
		}

	}
}