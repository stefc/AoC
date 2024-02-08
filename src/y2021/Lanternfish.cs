namespace advent.of.code.y2021.day6;

// http://adventofcode.com/2021/day/6

public class Lanternfish : IPuzzle
{
	public long Silver(IEnumerable<string> values) => Calc(values,80);
		
	public long Gold(IEnumerable<string> values) => Calc(values, 256);

	private long Calc(IEnumerable<string> values, int days) 
	{
		var initState = values.Single().ToNumbers();
		var circularShiftRegister = ImmutableArray<long>.Empty
			.AddRange( Enumerable.Range(0, 9)
				.Select( t => Convert.ToInt64(initState.Count( x => x == t))));

		return Enumerable.Range(0,days)
			.Aggregate( circularShiftRegister, 
				(accu,day) => accu.SetItem((day+ 7) % 9, accu[(day + 7) % 9] + accu[day % 9]))
			.Sum();
	}
}