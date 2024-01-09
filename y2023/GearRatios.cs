using MoreLinq;

namespace advent.of.code.y2023;

// http://adventofcode.com/2023/day/3

using SymbolMap = ImmutableDictionary<Point, char>;

using Ratio = (int ratio, int line, int start, int end);

using Ratios = ImmutableList<(int ratio, int line, int start, int end)>;

class GearRatios : IPuzzle
{
	internal int CountingPart1(IEnumerable<string> values)
	{
		var schematic = Parse(values);
		return schematic.ratios.Where(x => schematic.HasAdjacentSymbols(x)).Select(x => x.ratio).Sum();
	}

	internal int CountingPart2(IEnumerable<string> values)
	{
		var schematic = Parse(values);
		schematic = schematic with
		{ symbolMap = schematic.symbolMap.Where(kvp => kvp.Value == '*').ToImmutableDictionary() };


		// find all ratios with two adjacent symbols

		var gears = schematic.ratios
			.Where(x => schematic.HasAdjacentSymbols(x))
			.Select(x => (ratio: x.ratio, point: schematic.GetAdjacentSymbol(x)))
			.OrderBy(x => x.point)
			.GroupBy(x => x.point, x => x.ratio)
			// Having exact two ratios
			.Where(x => x.Count() == 2)
			.Select(x => x.Aggregate(1, (acc, cur) => acc * cur));

		return gears.Sum();
	}


	internal record struct Schematic(SymbolMap symbolMap, Ratios ratios)
	{
		public bool HasAdjacentSymbols(Ratio ratio)
		=> GetAdjacents(ratio).Intersect(ImmutableHashSet<Point>.Empty.Union(this.symbolMap.Keys)).Any();
		public Point GetAdjacentSymbol(Ratio ratio)
		=> GetAdjacents(ratio).Intersect(ImmutableHashSet<Point>.Empty.Union(this.symbolMap.Keys)).First();

		private static ImmutableHashSet<Point> GetAdjacents(Ratio ratio)
		{
			var (_, y, start, end) = ratio;
			var range = Enumerable.Range(start - 1, (end - start + 1) + 2);
			return ImmutableHashSet<Point>.Empty
							.Add(new Point(start - 1, y))
							.Add(new Point(end + 1, y))
							.Union(range.Select(x => new Point(x, y - 1)))
							.Union(range.Select(x => new Point(x, y + 1)));
		}
	}

	internal static Schematic Parse(IEnumerable<string> values)
	{
		var symbolMap = ExtractSymbolMap(values);
		var ratios = ExtractRatiosFromLines(values);
		return new Schematic(symbolMap, ratios);
	}

	internal static SymbolMap ExtractSymbolMap(IEnumerable<string> lines) => lines.SelectMany((line, y) => line.Select((ch, x) => (at: new Point(x, y), symbol: ch)))
			.Where(tuple => tuple.symbol != '.' && !char.IsDigit(tuple.symbol))
			.Aggregate(SymbolMap.Empty,
				(acc, cur) => acc.Add(cur.at, cur.symbol));

	internal static Ratios ExtractRatiosFromLines(IEnumerable<string> lines)
	=> lines.SelectMany(ExtractRatiosFromLine).ToImmutableList();

	public long Silver(IEnumerable<string> input) => this.CountingPart1(input);

	public long Gold(IEnumerable<string> input) => this.CountingPart2(input);

	internal static IEnumerable<Ratio> ExtractRatiosFromLine(string line, int y)
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
				if (ratio != 0)
					yield return (ratio, y, start, end);

				start = index + 1;
				end = index + 1;
				ratio = 0;
			}
		}
		if (ratio != 0)
			yield return (ratio, y, start, end);
	}
}
