using System.Linq;
using System.Collections.Generic;
using Combinatorics.Collections;
using System.Collections.Immutable;

namespace advent.of.code.y2020.day9
{

	// http://adventofcode.com/2020/day/9

	static class EncodingError
	{

		public static long FirstInvalidNumber(IEnumerable<long> values, int preamble = 25)
		{
			return values.GetWindows(preamble)
				.Select(window => window.ToArray())
				.Zip(values.Skip(preamble), (a, b) => (isInvalid: !IsValid(a, b), number: b))
				.First(tuple => tuple.isInvalid).number;
		}

		public static long FindWeakness(IEnumerable<long> values, long match)
		{
			var queue = ImmutableQueue<long>.Empty;
			var enumerator = values.GetEnumerator();
			while ((queue.Sum() != match) || (queue.Count() < 2))
			{

				if (queue.Sum() > match)
				{
					queue = queue.Dequeue();
				}
				else
				{
					enumerator.MoveNext();
					queue = queue.Enqueue(enumerator.Current);
				}
			}

			return queue.Min() + queue.Max();
		}

		private static bool IsValid(long[] prepend, long current)
		=> new Combinations<long>(prepend, 2).ToList().Any(row => row.Sum() == current);

		private static IEnumerable<IEnumerable<T>> GetWindows<T>(this IEnumerable<T> values, int windowWidth)
		{
			var slide = values.Take(windowWidth).Aggregate(ImmutableQueue<T>.Empty,
					(acc, cur) => acc.Enqueue(cur));

			yield return slide;

			foreach (var cur in values.Skip(windowWidth))
			{
				slide = slide.Dequeue().Enqueue(cur);
				yield return slide;
			}
		}
	}
}
