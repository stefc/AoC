using MoreLinq;

namespace advent.of.code.y2021.day14;

// http://adventofcode.com/2021/day/14

public class ExtPolymerization : IPuzzle
{
	public long Silver(IEnumerable<string> values) => Parse(values).Calc(10);
		
	public long Gold(IEnumerable<string> values) => Parse(values).Calc(40);

	internal static Instructions Parse(IEnumerable<string> values)
	=> new Instructions(values.First(), 
			values
			.Skip(1)
			.Select(l => l.Split(" -> "))
			.ToImmutableSortedDictionary(parts => parts.First(), parts => parts.Last().First()));

	internal record struct Instructions(string Template, ImmutableSortedDictionary<string, char> Rules)
	{
		public long Calc(int steps)
		{

			var rules = Rules;
			var pairs = GetPairs(Template, Rules);

			var counter = Template.Aggregate(
				ImmutableDictionary<char, long>.Empty,
				(acc, cur) => acc.TryGetValue(cur, out var val) ? acc.SetItem(cur, val + 1) : acc.Add(cur, 1));

			var nextPairs = ImmutableDictionary<Pair, long>.Empty;

			var total = Enumerable.Range(0, steps).Aggregate(counter,
				(acc, _) =>
				{
					nextPairs = nextPairs.Clear();
					foreach (var kvp in pairs)
					{
						var rule = rules[kvp.Key.Id];
						var count = kvp.Value;

						var leftPair = new Pair(kvp.Key.Left, rule);
						nextPairs = nextPairs
							.TryGetValue(leftPair, out var v1) ? nextPairs.SetItem(leftPair, v1 + count) : nextPairs.Add(leftPair, count);

						var rightPair = new Pair(rule, kvp.Key.Right);
						nextPairs = nextPairs
							.TryGetValue(rightPair, out var v2) ? nextPairs.SetItem(rightPair, v2 + count) : nextPairs.Add(rightPair, count);

						acc = acc.TryGetValue(rule, out var val) ? acc.SetItem(rule, val + count) : acc.Add(rule, count);
					}
					pairs = nextPairs;

					return acc;
				},
				acc => acc.Values.Max() - acc.Values.Min()
			);
			return total;
		}
	}

	public static ImmutableDictionary<Pair, long> GetPairs(string template, ImmutableSortedDictionary<string, char> rules)
	{
		var pairs = template
			.Window(2)
			.Select(chars => new Pair(chars.First(), chars.Last()))
			.GroupBy(pair => pair, (k, xs) => KeyValuePair.Create<Pair, long>(k, xs.Count()));
		return ImmutableDictionary<Pair, long>.Empty.AddRange(pairs);
	}

	public record struct Pair(char Left, char Right)
	{
		public string Id => new String(new char[] { Left, Right });
	};
}
