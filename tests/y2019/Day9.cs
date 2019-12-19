using Xunit;

using advent.of.code.y2019.day2;
using System.IO;
using advent.of.code.common;
using System.Linq;

namespace advent.of.code.tests.y2019
{

    [Trait("Year", "2019")]
    public class TestDay9
    {

       	[Fact]
	
	
		public void RelativeMode()
		{
            var modes = 21001l.ModesFromInstruction();

            Assert.Equal(3, modes.Count());

            Assert.Equal(Mode.Position, modes.ElementAt(0));
            Assert.Equal(Mode.Immediate, modes.ElementAt(1));
            Assert.Equal(Mode.Relative, modes.ElementAt(2));
		}

		
       
        [Fact]
        public void PuzzleOne()
        {
            string input = File.ReadAllText("tests/y2019/Day9.Input.txt");
        }

        [Fact]
        public void PuzzleTwo()
        {
            string input = File.ReadAllText("tests/y2019/Day9.Input.txt");
        }
    }
}