using Xunit;

using advent.of.code.y2019.day2;
using System.IO;
using System.Linq;

namespace advent.of.code.tests.y2019
{

	[Trait("Year", "2019")]
    public class TestDay2
	{
		[Theory]
		[InlineData("2,0,0,0,99","1,0,0,0,99")]
		public void PartOne(string expected, string value)
		{
			var program = value.ToNumbers();
			var result = expected.ToNumbers();

			// Assert.Equal(program, result);
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
			Assert.Equal(1, f(prg,0).GetOrElse(-99));
			Assert.False(f(prg,5).IsSome());
			Assert.True(f(prg,4).IsSome());
		}

		[Fact]
		public void TestPut() {
			var prg = ProgramAlarm.CreateProgram(1,2,3,4,5);
			var f = ProgramAlarm.PutInt();
			var newPrg = f(prg, 4, 87);
			Assert.Equal(87, newPrg.Program.Last());
		}
	}
}