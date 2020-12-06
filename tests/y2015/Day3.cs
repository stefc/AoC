// http://adventofcode.com/2015/day/3

using System;
using System.IO;
using System.Linq;
using Xunit;
using advent.of.code.y2015.day3;

namespace advent.of.code.tests.y2015 {

    [Trait("Category","y2015")]
	public class TestDay3
    {
        [Theory]
        [InlineData(">",2)]
        [InlineData("^>v<",4)]
        [InlineData("^v^v^v^v^v",2)]
        public void PartOne(string instructions, int expected)
            =>
                Assert.Equal(expected,
                    SphericalHouses.AtLeastOnePresent(instructions));

        [Theory]
        [InlineData("^v",3)]
        [InlineData("^>v<",3)]
        [InlineData("^v^v^v^v^v",11)]
        public void PartTwo(string instructions, int expected)
            =>
                Assert.Equal(expected,
                    SphericalHouses.TogetherWithRobodog(instructions));

        [Fact]
        public void Puzzle() {
            var input = File
                .ReadAllText("tests/y2015/Day3.Input.txt");

            Assert.Equal(2081,
                SphericalHouses.AtLeastOnePresent(input));

            Assert.Equal(2341,
                SphericalHouses.TogetherWithRobodog(input));
        }
    }
}