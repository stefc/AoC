// http://adventofcode.com/2017/day/2

using Division = (int numerator, int denominator);

namespace advent.of.code.y2017;

static class CorruptionChecksum
{

	public static int[][] GetSpreadsheet(this IEnumerable<string> input)
		=> input
			.Select(line => line.ToNumbers().ToArray())
			.ToArray();

	public static Division GetDivision(this string line) => GetDivisionNumbers(line.ToNumbers().OrderByDescending(x => x));

	private static Division GetDivisionNumbers(IEnumerable<int> numbers)
	{
		var numerator = 0;
		var denominator = 0;

		while (denominator == 0 && numbers.IsNotEmpty())
		{
			numerator = numbers.Head();
			numbers = numbers.Tail();
			denominator = numbers.SingleOrDefault(x => (numerator % x) == 0);
		}

		return (numerator, denominator);
	}

	public static int GetMinMaxAggregate(IEnumerable<string> input)
	{
		var matrix = GetSpreadsheet(input);

		var result = matrix
			.Select(row => row.Max() - row.Min())
			.Sum();
		return result;
	}

	// https://www.tabsoverspaces.com/233633-head-and-tail-like-methods-on-list-in-csharp-and-fsharp-and-python-and-haskell

	// https://gist.github.com/d11wtq/5234515

	// https://www.tabsoverspaces.com/233915-sum-function-using-generic-math-and-head-and-tail-functions

	public static bool Deconstruct<T>(this IEnumerable<T> seq, out (T head, IEnumerable<T> tail) list)
	{
		list = (head: seq.FirstOrDefault(), tail: seq.Skip(1));
		return (seq.Count() != 0);
	}

	public static Nullable<(T head, IEnumerable<T> tail)> Split<T>(this IEnumerable<T> seq)
	{
		if (seq.Count() == 0)
			return null;
		return (head: seq.FirstOrDefault(), tail: seq.Skip(1));
	}

	public static int GetDivisionAggregate(IEnumerable<string> input)
	{
		var result = GetSpreadsheet(input)
			.Select(row => GetDivisionNumbers(row.OrderByDescending(x => x)))
			;

		return result
			.Select(division => division.numerator / division.denominator)
			.Sum();
	}
}
