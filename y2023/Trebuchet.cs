using MoreLinq;

namespace advent.of.code.y2023;

// http://adventofcode.com/2023/day/1

class Trebuchet : IPuzzle
{
	internal int CountingPart1(IEnumerable<string> values)
	=> values.Select(x => (first: x.First(ch => char.IsDigit(ch)), last: x.Last(ch => char.IsDigit(ch))))
		.Sum(x => int.Parse(new string(new[] { x.first, x.last })));

	internal int CountingPart2(IEnumerable<string> values)
	=> values.Select(ProcessValue).Sum();

	private static int ProcessValue(string value)
	{
		var match = FindAllWords(value).Concat(FindDigits(value)).OrderBy(x => x.index).Select(x => x.number);
		return (match.First() * 10) + match.Last();
	}
	private static IEnumerable<(int number, int index)> FindWords(string value, string word, int number)
	=> Enumerable.Range(0, Math.Max(value.Length - word.Length, 0))
		.Select(index => value.IndexOf(word, index))
		.Distinct()
		.Where(x => x > -1)
		.Select(index => (number, index));

	private static IEnumerable<(int number, int index)> FindAllWords(string value)
	=> FindWords(value, "zero", 0).Concat(FindWords(value, "one", 1).
		Concat(FindWords(value, "two", 2).Concat(FindWords(value, "three", 3).
		Concat(FindWords(value, "four", 4).Concat(FindWords(value, "five", 5).
		Concat(FindWords(value, "six", 6).Concat(FindWords(value, "seven", 7).
		Concat(FindWords(value, "eight", 8).Concat(FindWords(value, "nine", 9))))))))))
		.ToList();

	private static IEnumerable<(int number, int index)> FindDigits(string value)
	=> Enumerable.Range(0, value.Length)
		.Select(index => (digit: value[index], index))
		.Where(x => char.IsDigit(x.digit))
		.Select(x => (int.Parse(x.digit.ToString()), x.index))
		.ToList();

	public long Silver(IEnumerable<string> input) => this.CountingPart1(input);

	public long Gold(IEnumerable<string> input) => this.CountingPart2(input);
}
