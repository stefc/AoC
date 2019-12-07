using Xunit;

// using advent.of.code.y2019.day6;
using System.IO;

namespace advent.of.code.tests.y2019
{
	[Trait("Year", "2019")]
    public class TestDay6
    {
        public void PartOne(int expected, int value)
        {
		//	var f = RocketEquation.GetFuel();

          //  Assert.Equal(expected, f(value));
        }

	   public void PartTwo(int expected, int value)
        {
	//		var f = RocketEquation.GetFuelIncluding();

      //      Assert.Equal(expected, f(value));
        }

		[Fact]
		public void PuzzleOne() {
	//		 string input = File.ReadAllText("tests/y2019/Day1.Input.txt");

	//		 Assert.Equal(3404722, RocketEquation.CalcTotal(input, false));
		}
		[Fact]
		public void PuzzleTwo() {
	//		 string input = File.ReadAllText("tests/y2019/Day1.Input.txt");

	//		 Assert.Equal(5104215, RocketEquation.CalcTotal(input, true));
		}

    }
}