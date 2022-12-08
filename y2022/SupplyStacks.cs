using MoreLinq;

namespace advent.of.code.y2022;

// http://adventofcode.com/2022/day/5

class SupplyStacks : IPuzzle<string>
{
	internal string Calc(IEnumerable<string> input, bool isCrane9001)
	{
		var splitted = input.Split(x => string.IsNullOrEmpty(x)).Where(x => x.Count() > 0);

		var stacks = splitted
			.First()
			.Transpose()
			.Select(row => new string(row.Reverse().ToArray()).Trim())
			.Where(col => col.IsNotEmpty() && char.IsDigit(col.First()))
			.ToArray()
			.ToImmutableDictionary(
				col => Convert.ToInt32(col.First().ToString()),
				col => ToStack(col));

		var commands = splitted
			.Last()
			.Select(ToCommand)
			.ToArray();

		return new string(commands
			.Aggregate(stacks, (acc, cur) => Execute(acc, cur, isCrane9001))
			.OrderBy(kvp => kvp.Key)
			.Select(kvp => kvp.Value.Peek())
			.ToArray());
	}

	[System.Diagnostics.DebuggerDisplay("{DebuggerDisplay,nq}")]
	internal record Command(int Move, int From, int To)
	{
		private string DebuggerDisplay => $"move {this.Move} from {this.From} to {this.To}";
	}

	public static ImmutableStack<char> ToStack(string item)
	=> item.Skip(1).Aggregate(ImmutableStack<char>.Empty, (acc, cur) => acc.Push(cur));


	public static Command ToCommand(string code)
	{
		const string pattern = @"move (?'move'\d{1,3}) from (?'from'\d) to (?'to'\d)";

		RegexOptions options = RegexOptions.IgnoreCase;
		Match match = Regex.Matches(code, pattern, options).First();

		Group moveGroup = match.Groups["move"];
		Group fromGroup = match.Groups["from"];
		Group toGroup = match.Groups["to"];

		return new Command(Convert.ToInt32(moveGroup.Value), Convert.ToInt32(fromGroup.Value), Convert.ToInt32(toGroup.Value));
	}

	public ImmutableDictionary<int, ImmutableStack<char>> Execute(ImmutableDictionary<int, ImmutableStack<char>> acc, Command command, bool isCrane9001 = false)
	{
		var removed = Enumerable.Range(0, command.Move).Aggregate(
			(fetched: ImmutableList<char>.Empty, src: acc[command.From]),
			(acc, _) =>
			{
				var newSrc = acc.src.Pop(out var current);
				return (fetched: acc.fetched.Add(current), src: newSrc);
			},
			(acc) => (fetched: isCrane9001 ? acc.fetched.Reverse() : acc.fetched, src: acc.src));

		var added = removed.fetched.Aggregate(acc[command.To], (acc, cur) => acc.Push(cur));

		return acc
			.SetItem(command.From, removed.src)
			.SetItem(command.To, added);
	}

	public string Silver(IEnumerable<string> input) => Calc(input, false);

	public string Gold(IEnumerable<string> input) => Calc(input, true);


}
