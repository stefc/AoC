using MoreLinq;

namespace advent.of.code.y2022.day1;

// http://adventofcode.com/2022/day/1

class CalorieCounting : IPuzzle
{
	internal int Counting(IEnumerable<int> values) =>
		values.Split(x => x == 0).Select(x => x.Sum()).Max();

	internal int CountingTop3(IEnumerable<int> values) =>
		values.Split(x => x == 0).Select(x => x.Sum()).OrderByDescending(x => x).Take(3).Sum();

	public long Silver(IEnumerable<string> input) => Counting(input.Select(x => string.IsNullOrEmpty(x) ? 0 : Convert.ToInt32(x)));

	public long Gold(IEnumerable<string> input) => CountingTop3(input.Select(x => string.IsNullOrEmpty(x) ? 0 : Convert.ToInt32(x)));
}
