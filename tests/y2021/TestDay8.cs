using advent.of.code.y2021.day8;

namespace advent.of.code.tests.y2021;

[Trait("Year", "2021")]
[Trait("Day", "8")]
public class TestDay8
{
	private readonly IPuzzle _ = new SevenSegmentSearch();

	private IEnumerable<string> CreateSample() {
		string input = @"
be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe
edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc
fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg
fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb
aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea
fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb
dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe
bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef
egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb
gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce
";	
		return input.Split("\n")
				.Where(line => !String.IsNullOrEmpty(line))
				.ToArray();
	}
	
	[Fact]
	public void SampleSilver() 
	=> Assert.Equal(26, _.Silver(CreateSample()));
	
	[Fact]
	public void PuzzleSilver()
	=> Assert.Equal(1, _.Silver(_.ReadPuzzle()));
	
	[Fact]
	public void SampleGold() 
	=> Assert.Equal(61229, _.Gold(CreateSample()));
	
	[Fact]
	public void PuzzleGold()
	=> Assert.Equal(946_346, _.Gold(_.ReadPuzzle()));

	[Fact]
	public void ParseLine()
	{
		var line = CreateSample().FirstOrDefault();

		var row = SevenSegmentSearch.Parse(line);
		Assert.Equal(10,row.Signal.Count());
		Assert.Equal(4,row.Output.Count());
	}

	[Fact]
	public void Determine() {
		var line = "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf";
		var row = SevenSegmentSearch.Parse(line);
		var dict = SevenSegmentSearch.Analyse(row);

		Assert.Equal(SevenSegmentSearch.Ordered("acedgfb"), dict[8]);
		Assert.Equal(SevenSegmentSearch.Ordered("cdfbe"), dict[5]);
		Assert.Equal(SevenSegmentSearch.Ordered("gcdfa"), dict[2]);
		Assert.Equal(SevenSegmentSearch.Ordered("fbcad"), dict[3]);
		Assert.Equal(SevenSegmentSearch.Ordered("dab"), dict[7]);
		Assert.Equal(SevenSegmentSearch.Ordered("cefabd"), dict[9]);
		Assert.Equal(SevenSegmentSearch.Ordered("cdfgeb"), dict[6]);
		Assert.Equal(SevenSegmentSearch.Ordered("eafb"), dict[4]);
		Assert.Equal(SevenSegmentSearch.Ordered("cagedb"), dict[0]);
		Assert.Equal(SevenSegmentSearch.Ordered("ab"), dict[1]);
	}

	[Fact]
	public void DetermineOutput() {
		var line = "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf";
		var row = SevenSegmentSearch.Parse(line);
		var actual = SevenSegmentSearch.GetOutput(row);

		Assert.Equal(5353, actual);
	}

}