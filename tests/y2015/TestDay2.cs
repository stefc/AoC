using advent.of.code.y2015.day2;

namespace advent.of.code.tests.y2015
{
	[Trait("Year", "2015")]
    [Trait("Day", "2")]
	public class TestDay2
    {
       	private readonly IPuzzle _ = new WrappingPaper();

        [Theory]
        [InlineData("2x3x4",58)]
        [InlineData("1x1x10",43)]
        public void PartOne(string dimensions, int expected)
            => Assert.Equal(expected, _.Silvered(dimensions));

        [Theory]
        [InlineData("2x3x4",34)]
        [InlineData("1x1x10",14)]
        public void PartTwo(string dimensions, int expected)
            => Assert.Equal(expected, _.Golded(dimensions));

        [Fact]
        public void Puzzle() {
            var input = _.ReadPuzzle();

            var paper = _.Silver(input);

            Assert.Equal(1586300, paper);

            var ribbon = _.Gold(input);

            Assert.Equal(3737498, ribbon);
        }
    }
}