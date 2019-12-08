
using System;
using System.Collections.Generic;
using System.Linq;

namespace advent.of.code {

	public static class StringExtension {

		public static IEnumerable<int> ToDigits(this string s)
		=> s.Select( ch => Convert.ToInt32(new String(ch,1)));

		public static IEnumerable<int> ToNumbers(this string s)
		=> s.Split(' ', '\t', ',', '\n')
			.Select( cell => Convert.ToInt32(cell));

		public static IEnumerable<string> ToSegments(this string s)
		=> s.Split(',');
	}
}