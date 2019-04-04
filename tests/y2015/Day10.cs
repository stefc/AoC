using System;
using Xunit;

using advent.of.code.y2015.day10;
using System.Numerics;
/*
example:

1 becomes 11 (1 copy of digit 1).
11 becomes 21 (2 copies of digit 1).
21 becomes 1211 (one 2 followed by one 1).
1211 becomes 111221 (one 1, one 2, and two 1s).
111221 becomes 312211 (three 1s, two 2s, and one 1).

Starting with the digits in your puzzle input, apply this process 40 times. What is the length of the result?

Your puzzle input is 1113222113.
*/

namespace advent.of.code.tests.y2015 {

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
                LookAndSay.Iterate(number, iteration));
        }

        [Fact]
        public void PuzzleA()
        {

            //Assert.Equal(252594,
            //    LookAndSay.Iterate(1113222113, 40).Length);
        }

        [Fact]
        public void Puzzle()
        {
            var result = LookAndSay.Polynomial(4);


            //var partA = LookAndSay.Iterate(new BigInteger(1113222113), 40);

            //Assert.Equal(252594, partA.ToString().Length);
            
            //var partB = LookAndSay.Iterate(partA, 50);

            //Assert.Equal(252594, partB.ToString().Length);

        }
    }
}