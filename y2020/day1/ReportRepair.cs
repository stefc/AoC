
// http://adventofcode.com/2018/day/1

using System.Linq;
using System.Collections.Generic;
using Combinatorics.Collections;

namespace advent.of.code.y2020.day1
{

	static class ReportRepair
	{

		public static int MultiplyNumbers(IEnumerable<int> values, int sum, int numbers)
		{
			Combinations<int> combinations = new Combinations<int>(values.ToList(), numbers);
			var list = combinations.ToList();
			var result = list.Single( row => row.Sum() == sum);
			return result.Aggregate( 1, (accu,current) => accu * current);
		}
	}
}