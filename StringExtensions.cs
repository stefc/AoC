
using System;
using System.Collections.Generic;
using System.Linq;

namespace advent.of.code {

	public static class StringExtension {

		public static IEnumerable<int> ToDigits(this string s) {
			return
				s.Select( ch => Convert.ToInt32(new String(ch,1)));
		}

		public static IEnumerable<int> ToNumbers(this string s) {
			return s
				.Split(' ', '\t', ',', '\n')
				.Select( cell => Convert.ToInt32(cell));
		}
	}
}