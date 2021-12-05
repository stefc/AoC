namespace advent.of.code.common;

public static class EnumerableExtension
{

	public static T Head<T>(this IEnumerable<T> sequence)
	{
		return sequence.FirstOrDefault();
	}

	public static IEnumerable<T> Tail<T>(this IEnumerable<T> sequence)
	{
		return sequence.Skip(1);
	}

	public static bool IsEmpty<T>(this IEnumerable<T> sequence) =>
		sequence.Count() == 0;
	public static bool IsNotEmpty<T>(this IEnumerable<T> sequence) =>
		sequence.Count() != 0;

	///<summary>Finds the index of the first item matching an expression in an enumerable.</summary>
	///<param name="items">The enumerable to search.</param>
	///<param name="predicate">The expression to test the items against.</param>
	///<returns>The index of the first matching item, or -1 if no items match.</returns>
	public static int FindIndex<T>(this IEnumerable<T> items, Func<T, bool> predicate)
	{
		if (items == null) throw new ArgumentNullException("items");
		if (predicate == null) throw new ArgumentNullException("predicate");

		int retVal = 0;
		foreach (var item in items)
		{
			if (predicate(item)) return retVal;
			retVal++;
		}
		return -1;
	}

	public static IEnumerable<T> Infinite<T>(this ICollection<T> sequence)
	{
		return new InfiniteGenerator<T>(sequence);
	}

	private class InfiniteGenerator<T> : IEnumerable<T>
	{
		private readonly ICollection<T> collection;

		public InfiniteGenerator(ICollection<T> collection)
		{
			this.collection = collection;
		}
		public IEnumerator<T> GetEnumerator()
		{
			return new InfiniteEnumerator<T>(this.collection);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	private class InfiniteEnumerator<T> : IEnumerator<T>
	{
		private int state = -1;
		private readonly ICollection<T> collection;

		public InfiniteEnumerator(ICollection<T> collection)
		{
			this.collection = collection;
		}

		public T Current => this.collection.ElementAtOrDefault(state);

		object IEnumerator.Current => this.Current;

		public void Dispose()
		{
		}

		public bool MoveNext()
		{
			int count = this.collection.Count;
			this.state = (this.state + 1) % count;
			return count != 0;
		}

		public void Reset()
		{
			this.state = -1;
		}
	}

	// https://stackoverflow.com/a/3683217/1259996
	// I don't like this name :(
	public static IEnumerable<TResult> SelectWithPrevious<TSource, TResult>(
		this IEnumerable<TSource> source,
		Func<TSource, TSource, TResult> projection)
	{
		using (var iterator = source.GetEnumerator())
		{
			if (!iterator.MoveNext())
			{
				yield break;
			}
			TSource previous = iterator.Current;
			while (iterator.MoveNext())
			{
				yield return projection(previous, iterator.Current);
				previous = iterator.Current;
			}
		}
	}

	public static IEnumerable<long> PowersOf10(this int n)
	=> Enumerable
		.Range(0, n)
		.Select(x => Convert.ToInt64(Math.Pow(10, x)));

	public static IEnumerable<ulong> PowersOf2(this int n)
	=> Enumerable
		.Range(0, n)
		.Select(x => Convert.ToUInt64(Math.Pow(2, x)));

	// https://stackoverflow.com/a/12816817/1259996
	public static IEnumerable<int> AsEnumerable(this Range range)
	=> Enumerable.Range(range.Start.Value, range.End.Value - range.Start.Value + 1);

	// https://stackoverflow.com/a/914198/1259996
	public static TSource MinBy2<TSource, TKey>(
		this IEnumerable<TSource> source, Func<TSource, TKey> selector)
	{
		return source.MinBy2(selector, null);
	}

	public static TSource MinBy2<TSource, TKey>(
		this IEnumerable<TSource> source,
		Func<TSource, TKey> selector, IComparer<TKey> comparer)
	{
		if (source == null) throw new ArgumentNullException("source");
		if (selector == null) throw new ArgumentNullException("selector");
		comparer = comparer ?? Comparer<TKey>.Default;

		using (var sourceIterator = source.GetEnumerator())
		{
			if (!sourceIterator.MoveNext())
			{
				throw new InvalidOperationException(
					"Sequence contains no elements");
			}
			var min = sourceIterator.Current;
			var minKey = selector(min);
			while (sourceIterator.MoveNext())
			{
				var candidate = sourceIterator.Current;
				var candidateProjected = selector(candidate);
				if (comparer.Compare(candidateProjected, minKey) < 0)
				{
					min = candidate;
					minKey = candidateProjected;
				}
			}
			return min;
		}
	}
}
