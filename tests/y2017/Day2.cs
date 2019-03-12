using System;
using Xunit;

using advent.of.code.y2017.day2;
using System.Linq;
using System.Collections.Generic;

namespace advent.of.code.tests.y2017 {

    public class TestDay2
    {

        [Fact]
        public void PartOne()
        {
            string input = @"
5 1 9 5
7 5 3
2 4 6 8
";
            Assert.Equal(18, CorruptionChecksum.getMinMaxAggregate(input));
        }
        
        [Fact]
        public void PartTwo()
        {
            string input = @"
5 9 2 8
9 4 7 3
3 8 6 5
";
            Assert.Equal(9, CorruptionChecksum.getDivisionAggregate(input));
        }

        [Theory]
        [InlineData("5 9 2 8", 8, 2)]
        [InlineData("9 4 7 3", 9, 3)]
        [InlineData("3 8 6 5", 6, 3)]
        public void TestDivision(string line, int expectedNumerator, int expectedDenominator) {
            var division = line.getDivision();
            
            Assert.Equal(expectedNumerator, division.Item1);
            Assert.Equal(expectedDenominator, division.Item2);
        }
    }
}