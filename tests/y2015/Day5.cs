// http://adventofcode.com/2015/day/3

using advent.of.code.y2015.day5;

namespace advent.of.code.tests.y2015;

/*
ow, a nice string is one with all of the following properties:

It contains a pair of any two letters that appears at least twice in the string without overlapping, like xyxy (xy) or aabcdefgaa (aa), but not like aaa (aa, but it overlaps).
It contains at least one letter which repeats with exactly one letter between them, like xyx, abcdefeghi (efe), or even aaa.
For example:

qjhvhtzxzqqjkmpb is nice because is has a pair that appears twice (qj) and a letter that repeats with exactly one letter between them (zxz).
xxyxx is nice because it has a pair that appears twice and a letter that repeats with one between, even though the letters used by each rule overlap.
uurcxstgmygtbstg is naughty because it has a pair (tg) but no repeat with a single letter between them.
ieodomkazucvgmuy is naughty because it has a repeating letter with one between (odo), but no pair that appears twice.

Realizing the error of his ways, Santa has switched to a better model of determining whether a string is naughty or nice. None of the old rules apply, as they are all clearly ridiculous.

Now, a nice string is one with all of the following properties:

It contains a pair of any two letters that appears at least twice in the string without overlapping, like xyxy (xy) or aabcdefgaa (aa), but not like aaa (aa, but it overlaps).
It contains at least one letter which repeats with exactly one letter between them, like xyx, abcdefeghi (efe), or even aaa.

def is_really_nice(s):
	first = False
	for i in range(len(s) - 3):
		sub = s[i: i + 2]
		if sub in s[i + 2:]:
			first = True
			print("{} is really nice and repeats with {}".format(s, sub))
			break
	if not first:
		return False
	second = False
	for i in range(len(s) - 2):
		if s[i] == s[i + 2]:
			second = True
			break
	return second


*/

[Trait("Year", "2015")]
[Trait("Day", "5")]
public class TestDay5
{
	[Theory]
	[InlineData("xyxy")]
	[InlineData("aabcdefgaa")]
	public void TestPair(string value) =>
		Assert.True(StringClassifier.HasPair(value));

	[Fact]
	public void TestNoPair() =>
		Assert.False(StringClassifier.HasPair("aaa"));

	[Theory]
	[InlineData("xyx")]
	[InlineData("aaa")]
	[InlineData("abcdefeghi")]
	public void TestSurrounding(string value) =>
		Assert.True(StringClassifier.HasSurounding(value));

	[Theory]
	[InlineData("qjhvhtzxzqqjkmpb", true)]
	[InlineData("xxyxx", true)]
	[InlineData("uurcxstgmygtbstg", false)]
	[InlineData("ieodomkazucvgmuy", false)]
	public void PartTwo(string value, bool isNice)
		=>
			Assert.Equal(isNice,
				StringClassifier.IsNicer(value));

	[Fact]
	public void Puzzle()
	{
		var input = File
			.ReadLines("tests/y2015/Day5.Input.txt");

		Assert.Equal(113, input
				.Where(StringClassifier.HasPair)
				.Count());
		Assert.Equal(412, input
				.Where(StringClassifier.HasSurounding)
				.Count());

		Assert.Equal(55, input
				.Where(StringClassifier.IsNicer)
				.Count());
		Assert.Equal(255, input
				.Where(StringClassifier.IsNice)
				.Count());
	}
}
