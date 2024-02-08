using System.Linq;
using System.Collections.Generic;
using Combinatorics.Collections;
using System.Collections.Immutable;
using System;

namespace advent.of.code.y2020.day10
{

	// http://adventofcode.com/2020/day/10

	static class AdapterArray
	{

		public static long CountJolt(IEnumerable<int> values)
		{
			var adapters = values.OrderBy(x=>x).Prepend(0).Append(values.Max()+3);
			var deltas = adapters.Zip( adapters.Skip(1),
				(a,b) => b-a);

			var delta3 = deltas.Count( x => x == 3);
			var delta1 = deltas.Count( x => x == 1);

			return delta3 * delta1;
		}

		public static long CountUnique(IEnumerable<int> values)
		{
			var adapters = values.OrderBy(x=>x).Prepend(0);
			var deltas = adapters.Zip( adapters.Skip(1),
				(a,b) => b-a).ToList();

			var s = new String(deltas.Select( d => d == 1 ? '*' : '-' ).ToArray());
			s = s.Replace("-", "1")
				.Replace("****", "7")
				.Replace("***", "4")
				.Replace("**", "2")
				.Replace("*", "")
				.Replace("1", "")
				;

			var factors = s.Select( ch => long.Parse(ch.ToString()));
			return factors.Aggregate( 1L, (acc,cur) => acc*cur);

		}


	}
}
