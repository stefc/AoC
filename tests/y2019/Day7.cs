using Xunit;

using advent.of.code.y2019.day2;
using advent.of.code.y2019.day7;
using System.Linq;
using System.IO;
using System.Collections.Immutable;
using System.Collections.Generic;
using System;

namespace advent.of.code.tests.y2019
{
    using Computation =
        StatefulComputation<Option<ProgramState>, ImmutableArray<int>>;

    [Trait("Year", "2019")]
    public class TestDay7
    {
        [Theory]
        [InlineData(3, 6, 012, 210)]
        [InlineData(5, 120, 01234, 43210)]
        public void TestPermutation(int n, int count, int min, int max)
        {
            var permutation = AmplificationCircuit.Permutate(n);
            Assert.Equal(count, permutation.Count());
            Assert.Equal(permutation.Count(),permutation.Distinct().Count());
            Assert.Equal(min, permutation.Min());
            Assert.Equal(max, permutation.Max());
        }

        [Theory]
        [InlineData(43210, 43210,
        "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0")]
        [InlineData(54321, 01234,
        "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0")]
        [InlineData(65210, 10432,
        "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0")]
        public void Chain(int expected, int sequence, string program)
        {
            var prg = ProgramAlarm.CreateProgram(program.ToNumbers());
            var computer = ProgramAlarm.CreateStateMaschine();
            var actual = Compute(computer, prg, sequence, 10000, 0);
            Assert.Equal(expected, actual);
        }

        private int Compute(Computation computer, ProgramState program,
            int sequence, int ary, int input)
        {
            var output = computer(program.WithInput((sequence / ary) % 10, input))
                .State.Match(() => -99, s => s.Output.Peek());
            if (ary == 1)
                return output;

            return Compute(computer, program, sequence, ary / 10, output);
        }




        [Fact]
        public void PuzzleOne()
        {
            string input = File.ReadAllText("tests/y2019/Day7.Input.txt");
            var prg = ProgramAlarm.CreateProgram(input.ToNumbers());


            var computer = ProgramAlarm.CreateStateMaschine();

			var permutations = AmplificationCircuit.Permutate(5);
			var actual = permutations
				.AsParallel()
				.Select( sequence => Compute(computer, prg, sequence, 10000, 0))
				.Max();

            Assert.Equal(38500, actual);

        }

        [Fact]
        public void PuzzleTwo()
        {
            string input = File.ReadAllText("tests/y2019/Day7.Input.txt");
            var prg = ProgramAlarm.CreateProgram(input.ToNumbers(), 5);


            var computer = ProgramAlarm.CreateStateMaschine();

            Assert.Equal(8684145, computer(prg).State.Match(
                () => 0, s => s.Output.Peek()));
        }


    }
}