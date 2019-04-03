using System;
using Xunit;

using advent.of.code.y2015.day10;
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
        [InlineData(111221,"312211")]
        public void PartOne(ulong number, string expected)
        {
            Assert.Equal(expected,
                LookAndSay.Iterate(number, iteration, 1));
        }
    }
}