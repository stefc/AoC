using Xunit;

using System.Linq;
using System;
using System.IO;
using advent.of.code.common;
using System.Collections.Immutable;

namespace advent.of.code.tests.common
{

	[Trait("Category", "common")]
	public class TestPoint
	{

		[Fact]
		public void HitTest()
		{
			var p1 = new Point(3,4);
			var p2 = new Point(0,7);
			var p3 = new Point(2,4);
			var p4 = new Point(2,5);
			var p5 = new Point(1,6);

			var line = (p1,p2);

			Assert.True(p4.HitTest(line));
			Assert.True(p5.HitTest(line));
			Assert.False(p3.HitTest(line));


		}
	}
}
