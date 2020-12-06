using System.Linq;
using System.Collections.Generic;
using Combinatorics.Collections;
using System.Text.RegularExpressions;

namespace advent.of.code.y2020.day2
{

	// http://adventofcode.com/2020/day/2
	static class PasswordPhilosophy
	{

		const string pattern = @"(?'policy'\d-\d) (?'letter'[a..z]): (?'password'.+)";

		public static int AreValid(IEnumerable<string> lines)
		{
			RegexOptions options = RegexOptions.IgnoreCase;

			Match match = Regex.Matches(lines.First(), pattern, options).First();

			Group firstGroup = match.Groups["policy"];
			Group secondGroup = match.Groups["letter"];
			Group thirdGroup = match.Groups["password"];

			return 0;
		}
	}
}