namespace advent.of.code.y2020.day2;

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

			return password.IsValidCount(policy.ExtractRange(), letter.First());
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

			return password.IsValidOccurence(policy.ExtractRange(), letter.First());
		});
	}

	private static bool IsValidCount(this string password, (int start, int end) policy, char letter)
	{
		var howMuch = password.Count(ch => ch == letter);
		return policy.start <= howMuch && howMuch <= policy.end;
	}

	private static bool IsValidOccurence(this string password, (int start, int end) policy, char letter)
	=> password.ElementAt(policy.start - 1) == letter ^
		password.ElementAt(policy.end - 1) == letter;
}
