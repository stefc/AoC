using System;
using Xunit;

using advent.of.code.y2016.day1;

namespace advent.of.code.tests.y2016 {

    public class TestDay1
    {
        [Theory]
        [InlineData("R2, L3", 5)]
        [InlineData("R2, R2, R2", 2)]
        [InlineData("R5, L5, R5, R3", 12)]
        public void PartOne(string instructions, int expectedBlocks)
        {
            Assert.Equal(expectedBlocks, 
                NoTimeForTaxicap.HowFarBlocksAway(instructions));
        }
        
    
    }
}