
// http://adventofcode.com/2018/day/1

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace advent.of.code.y2020.day1
{

    static class ReportRepair {

        public static int Multiply(IEnumerable<int> values, int sum) {


			int n = values.Count();

			var tuple = Enumerable.Range(1, n-1)
				.SelectMany( x => values.Skip(x).Select( y => (a:values.ElementAt( x-1), b:y)))
				.Single( x => x.a + x.b == sum);

			return tuple.a * tuple.b;
        }
    }
}