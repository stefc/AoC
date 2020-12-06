using System.Linq;
using System.Collections.Generic;
using Combinatorics.Collections;
using System.Text.RegularExpressions;
using System;

namespace advent.of.code.y2020.day2
{

	// http://adventofcode.com/2020/day/2
	static class PasswordPhilosophy
	{

		const string pattern = @"(?'policy'\d+-\d+) (?'letter'[a-z]): (?'password'.+)";

		public static int AreValidCount(IEnumerable<string> lines)
		{
			RegexOptions options = RegexOptions.IgnoreCase;
			return lines.Count(line =>
			{

				Match match = Regex.Matches(line, pattern, options).First();

				var policy = match.Groups["policy"].Value;
				var letter = match.Groups["letter"].Value;
				var password = match.Groups["password"].Value;

				return password.IsValidCount(policy.PolicyFrom(), letter.First());
			});
		}

		public static int AreValidOccurence(IEnumerable<string> lines)
		{
			RegexOptions options = RegexOptions.IgnoreCase;
			return lines.Count(line =>
			{

				Match match = Regex.Matches(line, pattern, options).First();

				var policy = match.Groups["policy"].Value;
				var letter = match.Groups["letter"].Value;
				var password = match.Groups["password"].Value;

				return password.IsValidOccurence(policy.PolicyFrom(), letter.First());
			});
		}

		private static (int start, int end) PolicyFrom(this string value)
		{
			var ranges = value.Split('-').Select(x => Convert.ToInt32(x)).ToArray();
			return (start: ranges.First(), end: ranges.Last());
		}

		private static bool IsValidCount(this string password, (int start, int end) policy, char letter)
		{
			var howMuch = password.Count(ch => ch == letter);
			return policy.start <= howMuch && howMuch <= policy.end;
		}

		private static bool IsValidOccurence(this string password, (int start, int end) policy, char letter)
		=>	password.ElementAt(policy.start - 1) == letter ^
			password.ElementAt(policy.end - 1) == letter;
	}
}
