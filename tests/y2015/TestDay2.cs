
using advent.of.code.y2015.day2;

namespace advent.of.code.tests.y2015
{

	[Trait("Year", "2015")]
    [Trait("Day", "2")]
	public class TestDay2
    {
        [Theory]
        [InlineData("2x3x4",58)]
        [InlineData("1x1x10",43)]
        public void PartOne(string dimensions, int expected)
            =>
                Assert.Equal(expected,
                    WrappingPaper.SquareFeetOfPaper(dimensions));

        [Theory]
        [InlineData("2x3x4",34)]
        [InlineData("1x1x10",14)]
        public void PartTwo(string dimensions, int expected)
            =>
                Assert.Equal(expected,
                    WrappingPaper.FeetOfRibbon(dimensions));

        [Fact]
        public void Puzzle() {
            var input = File
                .ReadLines("tests/y2015/Day2.Input.txt");

            var paper = input
                .Select(WrappingPaper.SquareFeetOfPaper)
                .Sum();

            Assert.Equal(1586300, paper);

            var ribbon = input
                .Select(WrappingPaper.FeetOfRibbon)
                .Sum();

            Assert.Equal(3737498, ribbon);
        }
    }
}