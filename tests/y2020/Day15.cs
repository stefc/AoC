using Xunit;
using System.Linq;
using System.IO;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using advent.of.code.y2020.day15;

namespace advent.of.code.tests.y2020
{

	[Trait("Category", "y2020")]
	public class TestDay15
	{

		[Fact]
		public void PuzzleOne()
		{
			// Arrange
			var input = new[]{0,3,6};

			// Act
			var actual = RambunctiousRecitation.Spoken(input)
				.ElementAt(2020-1);

			// Assert
			Assert.Equal(436, actual);
		}

		[Fact]
		public void PuzzlePartOne()
		{
			// Arrange
			var input = new[]{1,2,16,19,18,0};

			// Act
			var actual = RambunctiousRecitation.Spoken(input)
				.ElementAt(2020-1);

			// Assert
			Assert.Equal(536, actual);
		}

		[Fact(Skip="Huge Memory Impact")]
		public void PuzzleTwo()
		{
			// Arrange
			var input = new[]{1,2,16,19,18,0};

			// Act
			var actual = RambunctiousRecitation.Spoken(input)
				.ElementAt(30000000-1);

			// Assert
			Assert.Equal(175594, actual);
		}
	}
}

