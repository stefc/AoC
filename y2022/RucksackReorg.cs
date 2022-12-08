using MoreLinq;

namespace advent.of.code.y2022.day3;

// http://adventofcode.com/2022/day/3

class RucksackReorg : IPuzzle
{
	internal int TotalScore(IEnumerable<string> input) => Scoring(input.Select(ToCompartments));

	internal int TotalScoreGrouped(IEnumerable<string> input) =>
	Scoring(input.Select((value, index) => new { PairNum = index / 3, value })
			.GroupBy(pair => pair.PairNum)
			.Select(grp => grp.Select(g => g.value.Select(ch => ch).ToImmutableHashSet()))
			.Select(grp => Intersect(grp)));

	private ImmutableHashSet<char> Intersect(IEnumerable<ImmutableHashSet<char>> group)
	=> group.Skip(1).Aggregate(group.First(), (acc, cur) => acc.Intersect(cur));

	private int Scoring(IEnumerable<IEnumerable<char>> group)
	=> group.SelectMany(x => x, (_, ch) => (ch >= 'a' ? ch - 'a' + 1 : ch - 'A' + 27)).Sum();
	private ImmutableHashSet<char> ToCompartments(string line)
	=> Intersect(line
		.Select((value, index) => new { PairNum = index / (line.Length / 2), value })
		.GroupBy(pair => pair.PairNum)
		.Select(grp => grp.Select(g => g.value).ToImmutableHashSet()));

	public long Silver(IEnumerable<string> input) => TotalScore(input);

	public long Gold(IEnumerable<string> input) => TotalScoreGrouped(input);


}
