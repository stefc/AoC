using System;
using System.IO;
using System.Linq;
using System.Collections.Immutable;
using System.Collections.Generic;

using Xunit;

using advent.of.code.y2019.day14;
using advent.of.code.common;
using SixLabors.Primitives;


namespace advent.of.code.tests.y2019
{
    [Trait("Year", "2019")]
    public class TestDay14
    {

        [Fact]
        public void ConvertSequence()
        {

            var reactions = CreateReactions(1)
                .ToImmutableDictionary( x => x.Chemical);


            var newReactions = reactions
                .Substitute()
                .Substitute()
                .Substitute()
                .Substitute();

          

            Assert.Equal(6, reactions.Count());
           
        }

        private static IEnumerable<Reaction> CreateReactions(int variant = 0) {
			if (variant==1) {
				return new string[]{
                    "10 ORE => 10 A",
                    "1 ORE => 1 B",
                    "7 A, 1 B => 1 C", // -> 7A, 1 ORE 
                    "7 A, 1 C => 1 D", // -> 7A (7A 1 ORE) -> 14xA 1xORE -> 11xORE, 4xA 
                    "7 A, 1 D => 1 E", // -> 7A (11xORE, 4xA) -> 11xA, 11xORE -> 21xORE, 1xA 
                    "7 A, 1 E => 1 FUEL" // -> 7A,  21xORE, 1xA -> 8xA 21xORE -> 10 ORE + 21 ORE
				}
				.GetReactions();
			}
            return null;
        }


        [Fact]
        public void PuzzleOne()
        {
            var input = File.ReadAllLines("tests/y2019/Day14.Input.txt");
           
           // Assert.Equal(213, count);
        }

        [Fact]
        public void PuzzleTwo()
        {

        }
    }

}