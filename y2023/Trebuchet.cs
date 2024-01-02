using MoreLinq;

namespace advent.of.code.y2023;

// http://adventofcode.com/2023/day/1

class Trebuchet : IPuzzle
{
	internal int Counting(IEnumerable<string> values) =>
		values.Select( x => (first : x.First( ch => char.IsDigit(ch) ), last : x.Last( ch => char.IsDigit(ch) )))
		.Sum(x => int.Parse(new string(new[]{x.first, x.last})));

	internal int CountingPartTwo(IEnumerable<string> values) =>
		Counting(values.Select( TransformWordsToDigits ).ToArray());

	internal string TransformWordsToDigits(string value) => value
		.Replace("zero", "0")
		.Replace("one", "1")
		.Replace("two", "2")
		.Replace("three", "3")
		.Replace("four", "4")
		.Replace("five", "5")
		.Replace("six", "6")
		.Replace("seven", "7")
		.Replace("eight", "8")
		.Replace("nine", "9");



	public long Silver(IEnumerable<string> input) => Counting(input);

	public long Gold(IEnumerable<string> input) => CountingPartTwo(input);
}
