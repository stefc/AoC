using System;
using System.IO;
using System.Linq;

using Xunit;

using advent.of.code.y2019.day13;
using advent.of.code.y2019.day2;
using advent.of.code.common;

namespace advent.of.code.tests.y2019
{
    [Trait("Year", "2019")]
    [Trait("Topic", "intcode")]
    public class TestDay13
    {

        [Fact]
        public void ConvertSequence() {
            var seq = new long[]{1,2,3,6,5,4}.AsEnumerable()
                .Select(Convert.ToInt32);

            var actual = seq.ToTiles();

            Assert.Equal(2, actual.Count());

            Assert.Equal(new Point(1,2), actual.First().Position);
            Assert.Equal(TileId.Paddle, actual.First().TileId);
            Assert.Equal(new Point(6,5), actual.Last().Position);
            Assert.Equal(TileId.Ball, actual.Last().TileId);

            var count = actual.Count( x => x.TileId == TileId.Block);
            Assert.Equal(0, count);
        }
        
        [Fact]
        public void PuzzleOne()
        {
            string input = File.ReadAllText("tests/y2019/Day13.Input.txt");
            var prg = ProgramAlarm.CreateProgram(input.ToBigNumbers());
			var computer = ProgramAlarm
				.CreateStateMaschine();

             var seq = computer(prg).State.Match( 
                    ()=> Enumerable.Empty<int>(),
                    x => x.Output.Reverse().Select(Convert.ToInt32));

             var actual = seq.ToTiles();


            var count = actual.Count( x => x.TileId == TileId.Block);
            Assert.Equal(213, count);
        }

        [Fact]
        public void PuzzleTwo()
        {
            
        }
    }
}