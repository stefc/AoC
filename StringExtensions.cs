namespace advent.of.code
{

	public static class StringExtension {

		public static IEnumerable<int> ToDigits(this string s)
		=> s.Select( ch => Convert.ToInt32(new String(ch,1)));

		public static IEnumerable<int> ToDigits(this int number) 
		=> number.ToString().ToDigits();

		public static IEnumerable<int> ToNumbers(this string s)
		=> s.Split(' ', '\t', ',', '\n')
			.Select( cell => Convert.ToInt32(cell));

		public static int[] ToNumbers(this string input, int l)
		=> input.Chunk(l).Select(x => Convert.ToInt32(new String(x).Trim())).ToArray();

		public static IEnumerable<long> ToBigNumbers(this string s)
		=> s.Split(' ', '\t', ',', '\n')
			.Select( cell => Convert.ToInt64(cell));

		public static IEnumerable<string> ToSegments(this string s)
		=> s.Split(',');
	}
}