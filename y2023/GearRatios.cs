using MoreLinq;

namespace advent.of.code.y2023;

// http://adventofcode.com/2023/day/3

using SymbolMap = ImmutableSortedDictionary<Point,char>;

using Ratio = (int ratio, int line, int start, int end);

using Ratios = ImmutableList<(int ratio, int line, int start, int end)>;

class GearRatios : IPuzzle
{
	internal int CountingPart1(IEnumerable<string> values)
	=> 0;

	internal int CountingPart2(IEnumerable<string> values)
	=> 0;


	internal record struct Schematic(SymbolMap symbolMap, Ratios ratios)
	{
		public bool this[int x, int y] => symbolMap.ContainsKey(new Point(x,y));
	}

	internal static Schematic Parse(IEnumerable<string> values)
	{
		var symbolMap = ExtractSymbolMap(values);

		var ratios = ExtractRatiosFromLines(values).ToImmutableList();
		return new Schematic(symbolMap, ratios);
	}

	internal static SymbolMap ExtractSymbolMap(IEnumerable<string> lines)
	{
		var height = lines.Count();
		var width = lines.First().Length;

		var symbolMap = lines.SelectMany( (line, y) => line.Select( (ch, x) => (at:new Point(x,y), symbol:ch)))
			.Where( tuple => tuple.symbol != '.' && !char.IsDigit(tuple.symbol))
			.Aggregate(SymbolMap.Empty,
				(acc, cur) => acc.Add(cur.at, cur.symbol));

		return symbolMap;
	}

	internal static IEnumerable<Ratio> ExtractRatiosFromLines(IEnumerable<string> lines)
	=> lines.SelectMany((line, y) => ExtractRatiosFromLine(line).Select(tuple => (tuple.ratio, y, tuple.start, tuple.end)));

	public long Silver(IEnumerable<string> input) => this.CountingPart1(input);

	public long Gold(IEnumerable<string> input) => this.CountingPart2(input);

	internal static IEnumerable<(int ratio, int start, int end)> ExtractRatiosFromLine(string line)
	{
		// iterate over digits in line and make a group break on non digits and persist start and end index
		var start = 0;
		var end = 0;
		var ratio = 0;
		foreach (var (ch, index) in line.Select((ch, index) => (ch, index)))
		{
			if (char.IsDigit(ch))
			{
				end = index;
				ratio = (ratio * 10) + (ch - '0');
			}
			else
			{
				if (end > start)
					yield return (ratio, start, end);

				start = index + 1;
				end = index + 1;
				ratio = 0;
			}
		}
	}
}
