using advent.of.code.y2021.day10;

namespace advent.of.code.tests.y2021;

[Trait("Year", "2021")]
[Trait("Day", "10")]
public class TestDay10 : IPuzzleTest
{
	private readonly IPuzzle _ = new SyntaxScoring();

	private IEnumerable<string> CreateSample() {
		string input = @"
[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]
";	
		return input.Split("\n")
				.Where(line => !String.IsNullOrEmpty(line))
				.ToArray();
	}
	
	[Fact]
	public void SampleSilver() 
	=> Assert.Equal(26397, _.Silver(CreateSample()));
	
	[Fact]
	public void PuzzleSilver()
	=> Assert.Equal(389589, _.Silver(_.ReadPuzzle()));
	
	[Fact]
	public void SampleGold() 
	=> Assert.Equal(288957, _.Gold(CreateSample()));
	
	
	[Fact]
	public void PuzzleGold()
	=> Assert.Equal(1190420163, _.Gold(_.ReadPuzzle()));
	
	[Theory]
	[InlineData("{([(<{}[<>[]}>{[]{[(<()>", 1197)]
	[InlineData("[[<[([]))<([[{}[[()]]]", 3)]
	[InlineData("[{[{({}]{}}([{[{{{}}([]", 57)]
	[InlineData("[<(<(<(<{}))><([]([]()", 3)]
	[InlineData("<{([([[(<>()){}]>(<<{{", 25137)]
	public void TestPoints(string chunk, int expected) 
	{
		Assert.Equal(expected, SyntaxScoring.CalcScore(chunk));	
	}

	[Theory]
	[InlineData("[({(<(())[]>[[{[]{<()<>>", "}}]])})]")]
	[InlineData("[(()[<>])]({[<{<<[]>>(", ")}>]})")]
	[InlineData("(((({<>}<{<{<>}{[]{[]{}", "}}>}>))))")]
	[InlineData("{<[[]]>}<{[{[{[]{()[[[]", "]]}}]}]}>")]
	[InlineData("<{([{{}}[<[[[<>{}]]]>[]]", "])}>")]
	public void TestAutocomplete(string chunk, string expected) 
	{
		Assert.Equal(expected, SyntaxScoring.Autocomplete(chunk));	
	}
}