
using System.Linq;
using System.IO;

using Xunit;

using advent.of.code.common;
using advent.of.code.y2019.day4;

namespace advent.of.code.tests.y2019
{
    [Trait("Category", "y2019")]
    public class TestDay4
	{
		[Theory]
		[InlineData(123789, false)]
		[InlineData(122345, true)]
		[InlineData(111111, true)]
		public void TwoAdjacentDigits(int value, bool expected)
		{
			var actual = SecureContainer.HasTwoAdjacentDigits(value);

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData(123789, true)]
		[InlineData(122345, true)]
		[InlineData(111111, true)]
		[InlineData(223450, false)]
		public void NeverDecrease(int value, bool expected)
		{
			var actual = SecureContainer.NeverDecrease(value);
			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData(112211, true)]
		[InlineData(112233, true)]
		[InlineData(111166, true)]
		[InlineData(443444, true)]
		[InlineData(111111, false)]
		[InlineData(111167, false)]
		[InlineData(111333, false)]
		[InlineData(123444, false)]
		[InlineData(111116, false)]

		public void NotPartOfGroup(int value, bool expected)
		{
			var actual = SecureContainer.NotPartOfGroup(value);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void PuzzleOne() {
			var actual = (234208..765869)
				.AsEnumerable()
				.Where(SecureContainer.HasTwoAdjacentDigits)
				.Where(SecureContainer.NeverDecrease)
				.Count();
			Assert.Equal(1246, actual);
		}

		[Fact]
		public void PuzzleTwo() {
			var range = 234208..765869;
			var actual = range.AsEnumerable()
				.Where(SecureContainer.HasTwoAdjacentDigits)
				.Where(SecureContainer.NeverDecrease)
				.Where(SecureContainer.NotPartOfGroup)
				.Count();
			Assert.Equal(814, actual);
		}
	}
}
