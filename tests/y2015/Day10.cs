using System;
using System.Numerics;

using Xunit;

using advent.of.code.y2015.day10;

namespace advent.of.code.tests.y2015 {

	[Trait("Category", "y2015")]
	public class TestDay10
    {
        [Theory]
        [InlineData(1,1,"11")]
        [InlineData(11,1,"21")]
        [InlineData(21,1,"1211")]
        [InlineData(1211,1,"111221")]
        [InlineData(111221,1,"312211")]
        [InlineData(1,3,"1211")]
        [InlineData(1,5,"312211")]
        public void PartOne(long number, int iteration, string expected)
        {
            Assert.Equal(expected,
                LookAndSay.Iterate(number, iteration).ToString());
        }

		[Fact]
        public void PuzzlePartOne()
        {
            Assert.Equal(252594,
                 LookAndSay.WhatLength(1113222113, 40));
        }

		[Fact]
        public void PuzzlePartTwo()
        {
            Assert.Equal(3579328,
                LookAndSay.WhatLength(1113222113, 50));
        }
    }
}