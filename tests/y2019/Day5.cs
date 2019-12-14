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
	
		public void ImmediateMode(string expected, string value)
		{
			var actual = ProgramAlarm
				.CreateStateMaschine()
				.Run(ProgramAlarm.CreateProgram(value.ToNumbers()))
				.Select( i => i.ToString());
			Assert.Equal(expected, string.Join(",", actual));
		}
	}
}