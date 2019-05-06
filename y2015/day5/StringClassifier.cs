
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

		public static bool HasPair(this string value) {
			int i = 0;
			while (i < value.Length - 3) {
				var sub = value.Substring(i, 2);
				if (value.Substring(i+2).Contains(sub)) {
					return true;
				}
				i++;
			}
			return false;
		}

		
		private static bool CheckPair(
			IEnumerable<char> head, IEnumerable<char> tail)
		{
			var pattern = String.Concat(head);
			var content = String.Concat(tail);

			if (pattern.Length!=2) return false;

			return content.Contains(pattern);
		}

		public static bool HasSurounding(this string value) => value
			.Aggregate(
				seed: (
					tail: value
						.Aggregate(ImmutableQueue<char>.Empty, (a,c) => a.Enqueue(c))
						.Dequeue(),
					result: false
				),

				func: (accu,current) => {
					if (accu.result || accu.tail.IsEmpty)
						return accu;

					return (
						tail: accu.tail.Dequeue(),
						result: CheckSurrounding(current, accu.tail));
				},

				resultSelector: accu => accu.result
			);

		private static bool CheckSurrounding(char head, IEnumerable<char> tail)
			=> String.Concat(tail).IndexOf(head,1) == 1;

	}
}