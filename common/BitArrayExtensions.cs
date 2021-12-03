using System.Collections;

namespace advent.of.code.common;

public static class BitArrayExtensions
{
	public static int ToNumeral(this BitArray binary)
	{
		if (binary == null)
			throw new ArgumentNullException("binary");
		if (binary.Length > 32)
			throw new ArgumentException("must be at most 32 bits long");

		var result = new int[1];
		new BitArray(binary.Cast<bool>().Reverse().ToArray()).CopyTo(result, 0);
		return result[0];
	}

	public static BitArray Negate(this BitArray bits) => new BitArray(bits).Not();

	public static bool MostCommonBit(this BitArray bits)
	{
		var t = bits.Cast<bool>().Count(b => b);
		var f = bits.Cast<bool>().Count(b => !b);
		return t >= f;
	}

	public static bool LeastCommonBit(this BitArray bits)
	{
		var t = bits.Cast<bool>().Count(b => b);
		var f = bits.Cast<bool>().Count(b => !b);
		return t < f;
	}
}
