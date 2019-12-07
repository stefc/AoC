using Xunit;

using advent.of.code.y2019.day2;
using System.Linq;
using System.IO;

namespace advent.of.code.tests.y2019
{

	[Trait("Year", "2019")]
    public class TestDay2
	{
		[Theory]
		[InlineData("2,0,0,0,99","1,0,0,0,99")]
		[InlineData("2,3,0,6,99","2,3,0,3,99")]
		[InlineData("2,4,4,5,99,9801","2,4,4,5,99,0")]
		[InlineData("30,1,1,4,2,5,6,0,99","1,1,1,4,99,5,6,0,99")]
		public void PartOne(string expected, string value)
		{
			var actual = ProgramAlarm.CreateStateMaschine()
				.Run(ProgramAlarm.CreateProgram(value.ToNumbers()))
				.Select( i => i.ToString());
			Assert.Equal(expected, string.Join(",", actual));
		}

		[Fact]
		public void TestMultiply() {
			var f = ProgramAlarm.Mul();
			var actual = f(2,4);
			Assert.Equal(8, actual);
		}

		[Fact]
		public void TestAdd() {
			var f = ProgramAlarm.Add();
			var actual = f(2,4);
			Assert.Equal(6, actual);
		}

		[Fact]
		public void TestGetOpCode() {
			var prg = ProgramAlarm.CreateProgram(1,2,3,4,99);
			var f = ProgramAlarm.GetOpCode();
			Assert.Equal(1, f(prg).GetOrElse(-99));
			Assert.False(f(prg.WithSingleStep(5)).IsSome());
			Assert.True(f(prg.WithSingleStep()).IsSome());
		}

		[Fact]
		public void TestPut() {
			var prg = ProgramAlarm.CreateProgram(1,2,3,4,5);
			var f = ProgramAlarm.PutInt();
			var newPrg = f(prg, 4, 87);
			Assert.Equal(87, newPrg.Program.Last());
		}

		[Fact]
		public void PuzzleOne() {
			string input = File.ReadAllText("tests/y2019/Day2.Input.txt");
			var prg = ProgramAlarm.CreateProgram(input.ToNumbers());
			var f = ProgramAlarm.PutInt();
			var patched = f(f(prg,1,12),2,2);

			var actual = ProgramAlarm.CreateStateMaschine()
				.Run(patched)
				.FirstOrDefault();

			 Assert.Equal(3760627, actual);
		}

		[Fact]
		public void PuzzleTwo() {
			string input = File.ReadAllText("tests/y2019/Day2.Input.txt");
			var prg = ProgramAlarm.CreateProgram(input.ToNumbers());
			var f = ProgramAlarm.PutInt();

			var range = Enumerable.Range(0,99);

			var programs =
				from noun in range
			    from verb in range
				select (
					Program: f(f(prg,1,noun),2,verb),
					Code: 100 * noun + verb);

			var machine = ProgramAlarm.CreateStateMaschine();
			var actual = programs
				.AsParallel()
				.Single(
					x => machine.Run(x.Program).FirstOrDefault() == 19690720)
				.Code;

			 Assert.Equal(7195, actual);
		}
	}
}