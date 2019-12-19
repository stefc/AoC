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
				select string.Join(",", (from r in v select r.Value.ToString()));

			var actual = computer.Run(
				ProgramAlarm.CreateProgram(value.ToBigNumbers()));

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData(42, "42,0,4,0,99","3,0,4,0,99")]
		
		public void InputOpCode(int input, string expected, string value)
		{
			var prg = ProgramAlarm.CreateProgram(value.ToBigNumbers(), input);
			var computer = ProgramAlarm
				.CreateStateMaschine();

			var result = computer(prg);

			var actual = string.Join(",", 
				result.Value.Select( i => i.Value.ToString()));

			Assert.Equal(expected, actual);

			Assert.Equal(input, result.State.Match(
				()=> 0, s => s.Output.Peek()));

		}

		[Theory]
		[InlineData(8,1,"3,9,8,9,10,9,4,9,99,-1,8")]
		[InlineData(7,0,"3,9,8,9,10,9,4,9,99,-1,8")]
		[InlineData(9,0,"3,9,8,9,10,9,4,9,99,-1,8")]

		[InlineData(7,1,"3,9,7,9,10,9,4,9,99,-1,8")]
		[InlineData(8,0,"3,9,7,9,10,9,4,9,99,-1,8")]
		[InlineData(9,0,"3,9,7,9,10,9,4,9,99,-1,8")]

		[InlineData(8,1,"3,3,1108,-1,8,3,4,3,99")]
		[InlineData(7,0,"3,3,1108,-1,8,3,4,3,99")]
		[InlineData(9,0,"3,3,1108,-1,8,3,4,3,99")]

		[InlineData(7,1,"3,3,1107,-1,8,3,4,3,99")]
		[InlineData(8,0,"3,3,1107,-1,8,3,4,3,99")]
		[InlineData(9,0,"3,3,1107,-1,8,3,4,3,99")]
		
		public void Compare(int input, int output, string value)
		{
			var prg = ProgramAlarm.CreateProgram(value.ToBigNumbers(), input);
			var computer = ProgramAlarm
				.CreateStateMaschine();

			var result = computer(prg);
			var actual = result.State.Match(
				()=> -99, s => s.Output.Peek());

			Assert.Equal(output, actual);

		}

		[Theory]
		[InlineData(0,0,"3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9")]
		[InlineData(2,1,"3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9")]
		[InlineData(0,0,"3,3,1105,-1,9,1101,0,0,12,4,12,99,1")]
		[InlineData(2,1,"3,3,1105,-1,9,1101,0,0,12,4,12,99,1")]
		public void Jump(int input, int output, string value)
		{
			var prg = ProgramAlarm.CreateProgram(value.ToBigNumbers(), input);
			var computer = ProgramAlarm
				.CreateStateMaschine();

			var result = computer(prg);
			var actual = result.State.Match(
				()=> -99, s => s.Output.Peek());

			Assert.Equal(output, actual);
		}

		[Fact]
		public void PuzzleOne() {
			string input = File.ReadAllText("tests/y2019/Day5.Input.txt");
			var prg = ProgramAlarm.CreateProgram(input.ToBigNumbers(), 1);
			

			var computer = ProgramAlarm.CreateStateMaschine();

			Assert.Equal(14155342, computer(prg).State.Match(
				()=> 0, s => s.Output.Peek()));


			
		}

		[Fact]
		public void PuzzleTwo() {
			string input = File.ReadAllText("tests/y2019/Day5.Input.txt");
			var prg = ProgramAlarm.CreateProgram(input.ToBigNumbers(), 5);
			

			var computer = ProgramAlarm.CreateStateMaschine();

			Assert.Equal(8684145, computer(prg).State.Match(
				()=> 0, s => s.Output.Peek()));			
		}


	}
}