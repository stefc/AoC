// http://adventofcode.com/2015/day/3

using System;
using System.IO;
using System.Linq;
using Xunit;
using advent.of.code.y2015.day3;

namespace advent.of.code.tests.y2015 {

    public class TestDay4
    {
        [Theory]
        [InlineData(">",2)]
        public void PartOne(string instructions, int expected) 
            =>
                Assert.Equal(expected, 
                    SphericalHouses.AtLeastOnePresent(instructions));

        [Fact]
        public void Puzzle() {
            var input = File
                .ReadAllText("tests/y2015/Day4.Input.txt");
        }
    }
}