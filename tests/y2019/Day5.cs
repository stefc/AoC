using Xunit;

using advent.of.code.y2019.day2;
using System.Linq;
using System.IO;

namespace advent.of.code.tests.y2019
{

	[Trait("Year", "2019")]
    public class TestDay5
	{
		
		[Theory]
		[InlineData("1002,4,3,4,99","1002,4,3,4,33")]
		[InlineData("1101,100,-1,4,99","1101,100,-1,4,0")]
	
		public void ImmediateMode(string expected, string value)
		{
			var computer = 
				from v in ProgramAlarm.CreateStateMaschine()
				select string.Join(",", (from r in v select r.ToString()));

			var actual = computer.Run(
				ProgramAlarm.CreateProgram(value.ToNumbers()));

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData(42, "42,0,4,0,99","3,0,4,0,99")]
		
		public void InputOpCode(int input, string expected, string value)
		{
			var prg = ProgramAlarm.CreateProgram(value.ToNumbers(), input);
			var computer = ProgramAlarm
				.CreateStateMaschine();

			var result = computer(prg);

			var actual = string.Join(",", 
				result.Value.Select( i => i.ToString()));

			Assert.Equal(expected, actual);

			Assert.Equal(input, result.State.Match(
				()=> 0, s => s.Output.Peek()));

		}

		[Fact]
		public void PuzzleOne() {
			string input = File.ReadAllText("tests/y2019/Day5.Input.txt");
			var prg = ProgramAlarm.CreateProgram(input.ToNumbers(), 1);
			

			var computer = ProgramAlarm.CreateStateMaschine();

			Assert.Equal(14155342, computer(prg).State.Match(
				()=> 0, s => s.Output.Peek()));


			
		}


	}
}