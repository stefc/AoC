// http://adventofcode.com/2015/day/2
namespace advent.of.code.y2015.day2
{
	public class WrappingPaper : IPuzzle
	{

		public static int SquareFeetOfPaper(string dimension)
		{
			var dim = ToDimension(dimension);

			var surfaces = ImmutableArray<int>.Empty
				.Add(dim.l * dim.w)
				.Add(dim.w * dim.h)
				.Add(dim.l * dim.h);

			return surfaces
				.Select(side => side * 2)
				.Sum() + surfaces.Min();
		}

		public static int FeetOfRibbon(string dimension)
		{
			var lengths = GetLengths(dimension);
			return (lengths.Sum() - lengths.Max()) * 2 +
				lengths.Aggregate(1, (accu, current) => accu * current);
		}

		private static IEnumerable<int> GetLengths(string dimension) => dimension
					.Split('x')
					.Select(x => Convert.ToInt32(x));

		public static (int l, int w, int h) ToDimension(string dimension)
		{
			var lengths = GetLengths(dimension);
			return (
				l: lengths.ElementAt(0),
				w: lengths.ElementAt(1),
				h: lengths.ElementAt(2));
		}

		public long Silver(IEnumerable<string> values) 
        => values
				.Select(WrappingPaper.SquareFeetOfPaper)
				.Sum();

		public long Gold(IEnumerable<string> values) 
        => values
				.Select(WrappingPaper.FeetOfRibbon)
				.Sum();
	}
}