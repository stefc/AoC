using System.Collections.Concurrent;

namespace advent.of.code.common;

public static class Memoizer
{
	public static Func<T, TResult> Memoize<T, TResult>(this Func<T, TResult> f)
	{
		var cache = new ConcurrentDictionary<T, TResult>();
		return a => cache.GetOrAdd(a, f);
	}
}