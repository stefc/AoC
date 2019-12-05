using Xunit;

using advent.of.code.y2019.day1;
using System.IO;

namespace advent.of.code.tests.y2019
{
	[Trait("Year", "2019")]
    public class TestDay1
    {
        [Theory]
        [InlineData(2,12)]
        [InlineData(2,14)]
        [InlineData(654,1969)]
        [InlineData(33583,100756)]
        public void PartOne(int expected, int value)
        {
			var f = RocketEquation.GetFuel();

            Assert.Equal(expected, f(value));
        }

		[Theory]
        [InlineData(2,14)]
        [InlineData(966,1969)]
        [InlineData(50346,100756)]
        public void PartTwo(int expected, int value)
        {
			var f = RocketEquation.GetFuelIncluding();

            Assert.Equal(expected, f(value));
        }

		[Fact]
		public void PuzzleOne() {
			 string input = File.ReadAllText("tests/y2019/Day1.Input.txt");

			 Assert.Equal(3404722, RocketEquation.CalcTotal(input, false));
		}
		[Fact]
		public void PuzzleTwo() {
			 string input = File.ReadAllText("tests/y2019/Day1.Input.txt");

			 Assert.Equal(5104215, RocketEquation.CalcTotal(input, true));
		}

    }
}