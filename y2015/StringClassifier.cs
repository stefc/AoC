// http://adventofcode.com/2015/day/5

namespace advent.of.code.y2015.day5;

public class StringClassifier : IPuzzle
{
	public long Silver(IEnumerable<string> values)
	=> values.Count(StringExtensions.IsNice);

	public long Gold(IEnumerable<string> values)
	=> values.Count(StringExtensions.IsNicer);
}

public static class StringExtensions {
	public static bool IsNice(this string value) =>
		value.CountVowels() >= 3 &&
		value.HasDuplicates() &&
		!value.HasSpecialStrings();

	public static bool IsNicer(this string value) =>
		value.HasPair() &&
		value.HasSurounding();

	private static int CountVowels(this string value) => value
		.Where(ch => ch == 'a' || ch == 'e' || ch == 'o' || ch == 'u' || ch == 'i')
		.Count();

	private static bool HasDuplicates(this string value) => value
		.Aggregate(
			seed: (result: false, state: new Nullable<char>()),
			func: (accu, current) => (!accu.state.HasValue)
				?
				(result: accu.result, state: current)
				:
				(result: accu.result || current.Equals(accu.state.Value), state: current),
			resultSelector: accu => accu.result);

	private static bool HasSpecialStrings(this string value) =>
		value.Contains("ab") || value.Contains("cd") ||
		value.Contains("pq") || value.Contains("xy");

	public static bool HasPair(this IEnumerable<char> value) =>
		value.IsEmpty() ? false :
			(String.Concat(value.Skip(2))
				.Contains(String.Concat(value.Take(2)))) ? true : value.Skip(1).HasPair();

	public static bool HasSurounding(this IEnumerable<char> value) =>
		value.IsEmpty() || value.Count() <= 2 ? false :
			value.First() == value.ElementAt(2) ? true : value.Skip(1).HasSurounding();

}