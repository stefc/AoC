using MoreLinq;

namespace advent.of.code.y2021.day1;

// http://adventofcode.com/2021/day/1

class SonarSweep : IPuzzle
{	
	internal int CountIncreases(IEnumerable<int> values)
	=> values.Zip(values.Skip(1), (a, b) => b - a).Count(x => x > 0);

	internal int CountIncreasesSliding(IEnumerable<int> values, int windowSize = 3)
	=> CountIncreases(values.Window(windowSize).Select(window => window.Sum()));

	public int Silver(IEnumerable<string> input) => CountIncreases(input.Select(x => Convert.ToInt32(x)));
	
	public int Gold(IEnumerable<string> input) => CountIncreases(input.Select(x => Convert.ToInt32(x)).Window(3).Select(window => window.Sum()));
}
