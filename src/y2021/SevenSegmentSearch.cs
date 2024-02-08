namespace advent.of.code.y2021.day8;

// http://adventofcode.com/2021/day/8

public class SevenSegmentSearch : IPuzzle
{
	public long Silver(IEnumerable<string> values) {
		var unique = values.AsParallel().SelectMany( line => Parse(line).Output).Where
			( x => x.Length == 2 || x.Length == 4 || x.Length == 3 || x.Length == 7 );
		return unique.Count();
	}
		
	public long Gold(IEnumerable<string> values) 
	=> values.AsParallel().Select( Parse ).Sum(GetOutput);

	internal static Row Parse(string line)
	{
		var parts = line.Split('|', 2, StringSplitOptions.TrimEntries);
		return new Row( 
			parts.First().Split(' ', 10, StringSplitOptions.TrimEntries)
				.Select( Ordered).ToArray(), 
			parts.Last().Split(' ', 4, StringSplitOptions.TrimEntries)
				.Select( Ordered).ToArray()
		);
	}

	internal static string Ordered(string segments) 
	=> new String(segments.OrderBy(ch => ch).ToArray());

	private record Accu( ImmutableDictionary<int,string> match,  ImmutableHashSet<string> rest) {

	}

	private static Accu Reduce(Accu acc, int current, Func<string, bool> predicate) {
		var digit = acc.rest.Single( predicate ); 
		return acc with { match = acc.match.Add( current, digit),
			rest = acc.rest.Remove( digit ) };
	}

	public static ImmutableDictionary<int,string> Analyse(Row row) {

		var match = new Accu( 
			ImmutableDictionary<int,string>.Empty, 
			row.Signal.ToImmutableHashSet());

		match = Reduce(match, 1, p => p.Length == 2 );
		match = Reduce(match, 4, p => p.Length == 4 );
		match = Reduce(match, 7, p => p.Length == 3 );
		match = Reduce(match, 8, p => p.Length == 7 );

		//var seven = match.match[7].ToImmutableHashSet();
		var one = match.match[1].ToImmutableHashSet();
		//var eight = match.match[8].ToImmutableHashSet();
		var four = match.match[4].ToImmutableHashSet();

		var bd = four.Except(one);
		var cf = one;

		match = Reduce(match, 6, p=> (p.Length == 6) && (p.ToImmutableHashSet().Except(cf).Count() == 5));
		match = Reduce(match, 0, p => (p.Length == 6) && (p.ToImmutableHashSet().Except(bd).Count() == 5));
		match = Reduce(match, 9, p => p.Length == 6 );
		
		match = Reduce(match, 5, p => p.ToImmutableHashSet().Except(bd).Count() == 3);
		
		match = Reduce(match, 3, p => p.ToImmutableHashSet().Except(cf).Count() == 3);
		match = Reduce(match, 2, _ => true);
		
		return match.match;
	}

	public record Row ( string[] Signal, string[] Output) {
	}

	internal static int GetOutput(Row row)
	{
		var digits = Analyse(row).ToDictionary( x => x.Value, x => x.Key);
		var output = row.Output.Aggregate(0, (accu, current) => accu * 10 + digits[current]);
		return output;
	}
}