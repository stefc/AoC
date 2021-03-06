using System.IO;
using Xunit;

using advent.of.code.y2018.day1;

namespace advent.of.code.tests.y2018
{

	[Trait("Category", "y2018")]
	public class TestDay1
    {
        [Theory(Skip="not yet")]
        [InlineData(3,"+1,-2,+3,+1")]
        [InlineData(3,"+1,+1,+1")]
        [InlineData(0,"+1,+1,-2")]
        [InlineData(-6,"-1,-2,-3")]
        public void PartOne(int expected, string value)
        {
            Assert.Equal(expected, ChronalCalibration.ChangeFrequency(
                value));
        }

        [Fact(Skip="not yet")]
        public void PuzzleOne() {
            string input = File.ReadAllText("tests/y2018/Day1.Input.txt");
            Assert.Equal(493, ChronalCalibration.ChangeFrequency(input));
        }

        [Theory(Skip="not yet")]
        [InlineData(0,"+1,-1")]
        [InlineData(10,"+3,+3,+4,-2,-4")]
        [InlineData(5,"-6,+3,+8,+5,-6")]
        [InlineData(14,"+7,+7,-2,-7,-4")]
        public void PartTwo(int expected, string value)
        {
            Assert.Equal(expected, ChronalCalibration.DetectTwice(
                value));
        }

        [Fact(Skip="not yet")]
        public void PuzzleTwo() {
            string input = File.ReadAllText("tests/y2018/Day1.Input.txt");
            Assert.Equal(413, ChronalCalibration.DetectTwice(input));
        }
    }
}