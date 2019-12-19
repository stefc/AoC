using Xunit;

using advent.of.code.y2019.day8;
using System.IO;
using advent.of.code.common;

namespace advent.of.code.tests.y2019
{

    [Trait("Year", "2019")]
    public class TestDay8
    {
        [Theory]
        [InlineData("123456789012", 1)]
        [InlineData("000000780012120112", 3*2)]
        public void Checksum(string input, int expected)
        {
            var dim = new Point(3,2);
            var actual = input.CalcCheckSum(dim);
            Assert.Equal(expected, actual);
        }
       
        [Fact]
        public void PuzzleOne()
        {
            string input = File.ReadAllText("tests/y2019/Day8.Input.txt");
            var dim = new Point(25,6);
            var actual = input.CalcCheckSum(dim);
            Assert.Equal(1905, actual);
        }

        [Fact]
        public void PuzzleTwo()
        {
            string input = File.ReadAllText("tests/y2019/Day8.Input.txt");
           
        }
    }
}