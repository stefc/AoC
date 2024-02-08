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

		[Fact]
		public void TestExcelAdr()
		{
			Assert.Equal("A1", new SmallPoint(0,0).CellAdr);
			Assert.Equal("B1", new SmallPoint(1,0).CellAdr);
			Assert.Equal("A2", new SmallPoint(0,1).CellAdr);
			Assert.Equal("B2", new SmallPoint(1,1).CellAdr);
			Assert.Equal("Z1", new SmallPoint(25,0).CellAdr);
			Assert.Equal("AA1", new SmallPoint(26,0).CellAdr);
			Assert.Equal("AB1", new SmallPoint(27,0).CellAdr);
		}
	}
}
