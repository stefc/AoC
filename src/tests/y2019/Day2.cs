using Xunit;

using advent.of.code.y2019.day2;
using System.Linq;
using System.IO;
using System;

namespace advent.of.code.tests.y2019
{

	using static F;

	[Trait("Category", "y2019")]
	[Trait("Topic", "intcode")]
    public class TestDay2
	{
		[Theory]
		[InlineData("2,0,0,0,99","1,0,0,0,99")]
		[InlineData("2,3,0,6,99","2,3,0,3,99")]
		[InlineData("2,4,4,5,99,9801","2,4,4,5,99,0")]
		[InlineData("30,1,1,4,2,5,6,0,99","1,1,1,4,99,5,6,0,99")]
		public void PartOne(string expected, string value)
		{
			var actual = ProgramAlarm
				.CreateStateMaschine()
				.Run(ProgramAlarm.CreateProgram(value.ToBigNumbers()))
				.Select( i => i.Value.ToString());
			Assert.Equal(expected, string.Join(",", actual));
		}

		// [Fact]
		// public void InvalidProgram()
		// {
		// 	var actual = ProgramAlarm
		// 		.CreateStateMaschine()
		// 		.Run(ProgramAlarm.CreateProgram("1,0,0,0,98".ToNumbers()))
		// 		;
		// 	Assert.True(actual.IsEmpty);
		// }


		[Fact]
		public void TestPut() {
			var prg = ProgramAlarm.CreateProgram(1,2,3,4,5);
			var newPrg = from a in Some(prg) from b in Some(a.Write(4, 87)) select b;
			Assert.Equal(87, newPrg.GetOrElse(prg).Program.Last().Value);
		}

		[Fact]
		public void PuzzleOne() {
			string input = File.ReadAllText("tests/y2019/Day2.Input.txt");
			var prg = ProgramAlarm.CreateProgram(input.ToBigNumbers());

			Func<int,int,Option<ProgramState>> patching = (noun, verb) =>
				from a in Some(prg)
				from b in Some(a.Write(1,noun))
				from c in Some(b.Write(2,verb))
				select c;


			var actual = ProgramAlarm.CreateStateMaschine()
				.Run(patching(12,2).GetOrElse(prg))
				.FirstOrDefault();

			 Assert.Equal(3760627, actual.Value);
		}

		[Fact]
		public void PuzzleTwo() {
			string input = File.ReadAllText("tests/y2019/Day2.Input.txt");
			var prg = ProgramAlarm.CreateProgram(input.ToBigNumbers());

			var range = Enumerable.Range(0,99);

			Func<int,int,Option<ProgramState>> patching = (noun, verb) =>
				from a in Some(prg)
				from b in Some(a.Write(1,noun))
				from c in Some(b.Write(2,verb))
				select c;

			var programs =
				from noun in range
			    from verb in range
				select (
					Program: patching(noun,verb).GetOrElse(prg),
					Code: 100 * noun + verb);

			var machine = ProgramAlarm.CreateStateMaschine();
			var actual = programs
				.AsParallel()
				.Single(
					x => machine.Run(x.Program).FirstOrDefault().Value == 19690720)
				.Code;

			 Assert.Equal(7195, actual);
		}
	}
}
