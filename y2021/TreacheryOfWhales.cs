namespace advent.of.code.y2021.day7;

// http://adventofcode.com/2021/day/7

public class TreacheryOfWhales : IPuzzle 
{
	public long Silver(IEnumerable<string> values)
	{

		var positions = values.Single().ToNumbers().ToArray();
		return positions.Distinct()
			.Select( src => positions.Select( dest => Math.Abs(dest-src)).Sum())
			.Min();
	}
		
	public long Gold(IEnumerable<string> values) {

		var positions = values.Single().ToNumbers().ToArray();
		return Enumerable.Range(Convert.ToInt32(positions.Average())-1,3)
			.Select( src => positions.Select( dest => Binomial(Math.Abs(dest-src))).Sum())
			.Min();
	}

	private static int Binomial(int n, int b = 2) => n*(n+1)/b;	// 
}