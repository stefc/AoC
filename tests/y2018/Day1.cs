using System;
using Xunit;

using advent.of.code.y2018.day1;


namespace advent.of.code.tests.y2018 {

    public class TestDay1
    {
        [Theory]
        [InlineData(3,"+1, +1, +1")]
        [InlineData(0,"+1, +1, -2")]
        [InlineData(-6,"-1, -2, -3")]
        
        public void PartOne(int expected, string value)
        {
            Assert.Equal(expected, ChronalCalibration.ChangeFrequency(
                value));
        }
    }
}