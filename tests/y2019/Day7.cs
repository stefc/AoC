using Xunit;

using advent.of.code.y2019.day2;
using advent.of.code.y2019.day7;
using System.Linq;
using System.IO;

namespace advent.of.code.tests.y2019
{

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

        [Fact]
        public void TestNewPermutation()
        {
            var permutation = AmplificationCircuit.Permutate(56789.ToDigits());
            Assert.Equal(120, permutation.Count());
            Assert.Equal(permutation.Count(),permutation.Distinct().Count());
            Assert.Equal(56789, permutation.Min());
            Assert.Equal(98765, permutation.Max());
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
            var actual = computer.Compute(prg, sequence, 10000, 0);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(139629729, 98765,
        "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5")]
        [InlineData(18216, 97856,
        "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10")]
        public void Feedback(int expected, int sequence, string program)
        {
            var prg = ProgramAlarm.CreateProgram(program.ToNumbers());
            var computer = ProgramAlarm.CreateStateMaschine();

            var states = AmplificationCircuit.SetUpStates(prg, sequence);
            Assert.Equal(expected, computer.ComputeLoop(states, 0));
        }

        [Fact]
        public void PuzzleOne()
        {
            string input = File.ReadAllText("tests/y2019/Day7.Input.txt");
            var prg = ProgramAlarm.CreateProgram(input.ToNumbers());
            var computer = ProgramAlarm.CreateStateMaschine();
			var actual = AmplificationCircuit.Permutate(5)
				.AsParallel()
				.Select( sequence => computer.Compute(prg, sequence, 10000, 0))
				.Max();
            Assert.Equal(38500, actual);
        }

        [Fact]
        public void PuzzleTwo()
        {
            string input = File.ReadAllText("tests/y2019/Day7.Input.txt");
            var prg = ProgramAlarm.CreateProgram(input.ToNumbers());
            var computer = ProgramAlarm.CreateStateMaschine();
            var permutations = AmplificationCircuit
                .Permutate(56789.ToDigits()).ToList();

            var actual = permutations
				.AsParallel()
				.Select( sequence => AmplificationCircuit.SetUpStates(prg, sequence))
                .Select( states => computer.ComputeLoop(states, 0))
				.Max();

            Assert.Equal(33660560, actual);
        }
    }
}