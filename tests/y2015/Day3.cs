using System;
using Xunit;

using advent.of.code.y2015.day3;
using System.IO;
using System.Linq;

namespace advent.of.code.tests.y2015 {

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
        [InlineData(">",3)]
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


         /*    var paper = input
                .Select(WrappingPaper.SquareFeetOfPaper)
                .Sum();

            Assert.Equal(1586300, paper); */
        }
    }
}

/*
The next year, to speed up the process, Santa creates a robot version of himself, Robo-Santa, to deliver presents with him.

Santa and Robo-Santa start at the same location (delivering two presents to the same starting house), then take turns moving based on instructions from the elf, who is eggnoggedly reading from the same script as the previous year.

This year, how many houses receive at least one present?

For example:

^v delivers presents to 3 houses, because Santa goes north, and then Robo-Santa goes south.
^>v< now delivers presents to 3 houses, and Santa and Robo-Santa end up back where they started.
^v^v^v^v^v now delivers presents to 11 houses, with Santa going one direction and Robo-Santa going the other.
 */