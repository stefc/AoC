using MoreLinq;

namespace advent.of.code.y2022;

// http://adventofcode.com/2022/day/1

class CalorieCounting : IPuzzle
{
	internal int Counting(IEnumerable<int> values) =>
		values.Split(x => x == 0).Select(x => x.Sum()).Max();

	internal int CountingTop3(IEnumerable<int> values) =>
		values.Split(x => x == 0).Select(x => x.Sum()).OrderByDescending(x => x).Take(3).Sum();

	private int ToInt(string x) => (x, int.TryParse(x, out var i)) switch
	{
		(null or "", _) => 0,
		(_, true) => i,
		_ => 0
	};

	public long Silver(IEnumerable<string> input) => Counting(input.Select(ToInt));

	public long Gold(IEnumerable<string> input) => CountingTop3(input.Select(ToInt));
}
