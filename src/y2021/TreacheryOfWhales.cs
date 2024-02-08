using MathNet.Numerics.Statistics;

namespace advent.of.code.y2021.day7;

// http://adventofcode.com/2021/day/7

public class TreacheryOfWhales : IPuzzle 
{
	public long Silver(IEnumerable<string> values)
	{
		var samples = values.Single().ToNumbers().ToArray();
		var median = Convert.ToInt32(Statistics.Median(samples.Select(Convert.ToDouble)));
		return Enumerable.Range(median-1,3)
			.Select( src => samples.Select( dest => Math.Abs(dest-src)).Sum())
			.Min();
	}
		
	public long Gold(IEnumerable<string> values) {

		var positions = values.Single().ToNumbers().ToArray();
		return Enumerable.Range(Convert.ToInt32(positions.Average())-1,3)
			.Select( src => positions.Select( dest => Oeis.Binomial(Math.Abs(dest-src))).Sum())
			.Min();
	}
}