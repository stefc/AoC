
// http://adventofcode.com/2015/day/5

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace advent.of.code.y2015.day5 {

	static class StringClassifier {

		public static bool IsNice(string value)  =>
			value.CountVowels() >= 3 &&
			value.HasDuplicates() &&
			!value.HasSpecialStrings();

		public static bool IsNicer(string value)  =>
			value.HasPair() &&
			value.HasSurounding();

		private static int CountVowels(this string value) => value
			.Where(ch => ch == 'a' || ch == 'e' || ch == 'o' || ch == 'u' || ch == 'i')
			.Count();

		private static bool HasDuplicates(this string value) => value
			.Aggregate(
				seed: (result: false, state: new Nullable<char>()),
				func: (accu,current) => (!accu.state.HasValue)
					?
					(result: accu.result, state: current)
					:
					(result: accu.result || current.Equals(accu.state.Value), state: current),
				resultSelector: accu => accu.result );

		private static bool HasSpecialStrings(this string value) =>
			value.Contains("ab") || value.Contains("cd") ||
			value.Contains("pq") || value.Contains("xy");

		public static bool HasPair(this string value) => value
			.Aggregate(

				seed: (
					hashset: ImmutableHashSet<string>.Empty,
					result: false,
					state: new Nullable<char>()),

				func: (accu,current) => {
					if (!accu.state.HasValue) {
						return (hashset: accu.hashset, result: accu.result, state: current);
					} else {
						var tuple = accu.state.Value.ToString() + current;
						if (accu.hashset.Contains(tuple))
							return (hashset: accu.hashset, result: true, state: current);
						return (hashset: accu.hashset.Add(tuple), result: accu.result, state: current);
					}
				},

				resultSelector: accu => accu.result
			);

		public static bool HasSurounding(this string value) => value
			.Aggregate(
				seed: (
					queue: ImmutableQueue<char>.Empty,
					result: false
				),

				func: (accu,current) => {
					if (accu.result)
						return accu;
					var newQueue = accu.queue.Enqueue(current);
					if (newQueue.Count()>3)
						newQueue = newQueue.Dequeue();
					return (queue: newQueue,
						result: CheckSurrounding(newQueue));
				},

				resultSelector: accu => accu.result
			);

		private static bool CheckSurrounding(IEnumerable<char> queue) {

			if (queue.Count() <=2)
				return false;
			var content = String.Concat(queue);
			var hasSurounding = content.Last() == content.First();
			return hasSurounding;
		}
	}
}