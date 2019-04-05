using System;
using Xunit;

using advent.of.code.y2015.day1;
using System.IO;

namespace advent.of.code.tests.y2015 {

    public class TestDay1
    {
        [Theory]
        [InlineData("(())",0)]
        [InlineData("()()",0)]
        [InlineData("(((",3)]
        [InlineData("(()(()(",3)]
        [InlineData("))(((((",3)]
        [InlineData("())",-1)]
        [InlineData("))(",-1)]
        [InlineData(")())())",-3)]
        [InlineData(")))",-3)]
        public void PartOne(string instructions, int expectedFloor) 
            =>
                Assert.Equal(expectedFloor, 
                    NotQuiteLisp.WhatFloor(instructions));

        [Theory]
        [InlineData("()())",5)]
        public void PartTwo(string instructions, int expected) 
            =>
                Assert.Equal(expected,
                    NotQuiteLisp.HowManyMovesToBasement(instructions));

        [Fact]
        public void Puzzle() {
            string instructions = File.ReadAllText("day1.input.txt");
            
            Assert.Equal(2323, NotQuiteLisp.WhatFloor(instructions));
            Assert.Equal(2323, NotQuiteLisp.HowManyMovesToBasement(instructions));
        }
    }
}