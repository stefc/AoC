using System.Collections.Generic;
using System.Linq;
using advent.of.code.common;

namespace advent.of.code.y2020.day3
{

	// http://adventofcode.com/2020/day/3
	static class TobogganTrajectory
	{

		public static int HowManyTrees(IEnumerable<string> lines, Point slope)
		{
			var height = lines.Count();
			var width = lines.First().Count();
			var coords = Enumerable
				.Range(1, height-1)
				.Select( index => new Point((3 * index) % width, index % height));

			var trees = coords.Count( point =>
				lines.ElementAt(point.Y).ElementAt(point.X) == '#');

			return trees;
		}

	}
}
