// http://adventofcode.com/2015/day/3

using System;
using System.IO;
using System.Linq;
using Xunit;
using advent.of.code.y2015.day4;

namespace advent.of.code.tests.y2015 {

    public class TestDay4
    {
        [Theory]
        [InlineData("abcdef",609043)]
        [InlineData("pqrstuv",1048970)]
        public void PartOne(string secret, int expected) 
            =>
                Assert.Equal(expected, 
                    StockingSuffer.FindLowestNumber(secret));

        [Theory]
        [InlineData("abcdef",6742839)]
        [InlineData("pqrstuv",5714438)]
        public void PartTwo(string secret, int expected) 
            =>
                Assert.Equal(expected, 
                    StockingSuffer.FindLowestNumber(secret, prefix: 6));

        [Fact]
        public void Puzzle() {
            Assert.Equal(117946, 
                    StockingSuffer.FindLowestNumber("ckczppom"));
            Assert.Equal(3938038, 
                    StockingSuffer.FindLowestNumber("ckczppom", prefix: 6));
        }
    }
}