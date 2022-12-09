using MoreLinq;

namespace advent.of.code.y2022;

// http://adventofcode.com/2022/day/6

class TuningTrouble : IPuzzle
{
	internal long Find(IEnumerable<string> input, int differs) => input
			.FirstOrDefault()
			.Window(differs)
			.Select(x => x.Distinct().Count() == differs)
			.Select((f, i) => new { f, i })
			.FirstOrDefault(a => a.f).i + differs;
	public long Silver(IEnumerable<string> input) => Find(input, 4);

	public long Gold(IEnumerable<string> input) => Find(input, 14);
}
