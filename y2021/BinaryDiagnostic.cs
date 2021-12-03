using System.Collections;
using advent.of.code.common;

namespace advent.of.code.y2021.day3;

// http://adventofcode.com/2021/day/3

class  BinaryDiagnostic
{
	public int A(IEnumerable<string> values)
	{

		BitMatrix m = new BitMatrix(values);

		var gamma = new BitArray(m.GetColumns().Select( bits => bits.MostCommonBit()).ToArray());

		// var gamma = new BitArray(m.Transpose().Select(MostCommonBit).ToArray());
		var epsilon = gamma.Negate();

		return gamma.ToNumeral() * epsilon.ToNumeral();
	}

	public int B(IEnumerable<string> values)
	{
		var m = new BitMatrix(values);


		var generatorRating = CalcGeneratorRating(m);
		var scrubberRating = CalcScrubberRating(m);

		return scrubberRating * generatorRating;

	}

	private int CalcGeneratorRating(BitMatrix m)
	{
		var index = 0;
		while (m.Height != 1)
		{
			var mcb = m.GetColumn(index).MostCommonBit();
			m = new BitMatrix(m.GetRows().Where(ba => ba.Get(index) == mcb));
			index++;
		}
		return m.GetRows().Single().ToNumeral();
	}

	private int CalcScrubberRating(BitMatrix m)
	{
		var index = 0;
		while (m.Height != 1)
		{
			var lcb = m.GetColumn(index).LeastCommonBit();
			m = new BitMatrix(m.GetRows().Where(ba => ba.Get(index) == lcb));
			index++;
		}
		return m.GetRows().Single().ToNumeral();
	}

	
}


// [DebuggerDisplay("{DebuggerDisplay,nq}")]
public class BitMatrix
{

	private readonly IEnumerable<BitArray> data;
	private readonly int w;
	private readonly int h;

	public BitMatrix(IEnumerable<BitArray> values)
	{
		this.w = values.First().Cast<bool>().Count();
		this.h = values.Count();

		this.data = values.ToArray();
	}

	public BitMatrix(IEnumerable<string> values) : this(values.Select(x => ToBinary(values.First().Length, Convert.ToInt32(x, 2))))
	{

	}

	public static BitArray ToBinary(int w, int numeral)
	{
		var x = new BitArray(new[] { numeral });
		return new BitArray(x.OfType<bool>().Reverse().TakeLast(w).ToArray());
	}

	public BitArray GetRow(int row) => this.data.ElementAt(row);

	public IEnumerable<BitArray> GetRows() => this.data;

	public int Weight => this.w;
	public int Height => this.h;

	public BitArray GetColumn(int col)
	{
		var output = new BitArray(this.h);
		for (int j = 0; j < h; j++)
		{
			output.Set(j, this.data.ElementAt(j)[col]);
		}

		return output;
	}

	public IEnumerable<BitArray> GetColumns() 
	=> Enumerable.Range(0, this.Weight).Select( col => GetColumn(col)).ToArray();

}
