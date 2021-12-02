using MoreLinq;

namespace advent.of.code.y2021.day1;

// http://adventofcode.com/2021/day/1

static class SonarSweep
{
	public static int CountIncreases(IEnumerable<int> values)
	=> values.Zip(values.Skip(1), (a, b) => b - a).Count(x => x > 0);

	public static int CountIncreasesSliding(IEnumerable<int> values, int windowSize = 3)
	=> CountIncreases(values.Window(windowSize).Select(window => window.Sum()));
}
