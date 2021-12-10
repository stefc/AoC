namespace advent.of.code.y2021.day10;

// http://adventofcode.com/2021/day/10

public class SyntaxScoring : IPuzzle
{

	private static Dictionary<char, char> OpenBrackets = new Dictionary<char, char> 
        { ['('] = ')',[ '<'] = '>',[ '{']='}',[ '[']=']'};
	private static Dictionary<char, char> ClosedBrackets = new Dictionary<char, char> 
        { [')'] = '(',[ '>'] = '<',[ '}']='{',[ ']']='['};

	private static Dictionary<char, int> Points = new Dictionary<char, int> 
        { [')'] = 3,[ ']'] = 57,[ '}']=1197,[ '>']=25137};
	private static Dictionary<char, long> AutocompletePoints = new Dictionary<char, long> 
        { [')'] = 1,[ ']'] = 2, [ '}']=3,[ '>']=4};

	public long Silver(IEnumerable<string> values)
	=> values.AsParallel().Where(IsCorrupted).Sum(CalcScore);
	
	public long Gold(IEnumerable<string> values) 
	{
		var scores = values
			.AsParallel()
			.Where(IsIncomplete)
			.Select(CalcAutocompleteScore)
			.OrderBy( x => x)
			.ToArray();
		return scores[scores.Length/2];
	}
	
	public static bool IsCorrupted(string line) 
	=> line.Aggregate((stack: ImmutableStack<char>.Empty, state: false), 
		(acc,cur) => {
			if (!acc.state)	{

				if(OpenBrackets.ContainsKey(cur))
				{
					return (acc.stack.Push(cur), acc.state);
				} else if (ClosedBrackets.TryGetValue(cur, out var correspond)) 
				{
					if (!acc.stack.IsEmpty() && acc.stack.Peek()==correspond)
					{
						return (acc.stack.Pop(), acc.state);
					}
					return (acc.stack, true);
				}
				return acc;
			}
			else {
				return acc;
			}
		},
		acc => acc.state);
	public static bool IsIncomplete(string line) 
	=> line.Aggregate((stack: ImmutableStack<char>.Empty, state: false), 
		(acc,cur) => {
			if (!acc.state)	{

				if(OpenBrackets.ContainsKey(cur))
				{
					return (acc.stack.Push(cur), acc.state);
				} else if (ClosedBrackets.TryGetValue(cur, out var correspond)) 
				{
					if (!acc.stack.IsEmpty() && acc.stack.Peek()==correspond)
					{
						return (acc.stack.Pop(), acc.state);
					}
					return (acc.stack, true);
				}
				return acc;
			}
			else {
				return acc;
			}
		},
		acc => acc.state ? false : !acc.stack.IsEmpty );

	public static int CalcScore(string line)
	=> line.Aggregate((stack: ImmutableStack<char>.Empty, expected: new Nullable<char>()), 
		(acc,cur) => {

			if (acc.expected.HasValue)
				return acc;
			else if(OpenBrackets.ContainsKey(cur))
			{
				return (acc.stack.Push(cur), acc.expected);
			} else if (ClosedBrackets.TryGetValue(cur, out var correspond)) 
			{
				if (!acc.stack.IsEmpty() && acc.stack.Peek()==correspond)
				{
					return (acc.stack.Pop(), acc.expected);
				}
				return (ImmutableStack<char>.Empty, cur);
			}
			return acc;
		},
		acc => Points[acc.expected.Value]);

	public static string Autocomplete(string line)
	=> line.Aggregate(
		(stack: ImmutableStack<char>.Empty, state: true), 
		(acc,cur) => {
			if (acc.state)	{

				if(OpenBrackets.ContainsKey(cur))
				{
					return (acc.stack.Push(cur), acc.state);
				} 
				else if (ClosedBrackets.TryGetValue(cur, out var correspond)) 
				{
					if (!acc.stack.IsEmpty() && acc.stack.Peek()==correspond)
					{
						return (acc.stack.Pop(), acc.state);
					}
					return (acc.stack, false);
				}
				return acc;
			}
			return acc;
		},
		acc => String.Concat(acc.stack.Select( ch => OpenBrackets[ch])));

	public static long CalcAutocompleteScore(string s) 
	=> Autocomplete(s).Aggregate( 0L, (acc,cur) => acc * 5 + AutocompletePoints[cur]);
}


