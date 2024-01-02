using advent.of.code.y2021.day5;

namespace advent.of.code.tests.y2021;

[Trait("Year", "2021")]
[Trait("Day", "5")]
public class TestDay5  : IPuzzleTest
{
	private readonly IPuzzle _ = new HydroVenture();

	private IEnumerable<string> CreateSample()
	{
		string input = @"
0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2
";
		return input.Split("\n")
				.Where(line => !String.IsNullOrEmpty(line))
				.ToArray();
	}

	[Fact( Skip="Not work")]
	public void SampleSilver()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.Silver(input);

		// Assert
		Assert.Equal(5, actual);
	}


	[Fact]
	public void PuzzleSilver()
	{
		//  Arrange
		var input = _.ReadPuzzle();

		// Act
		var actual = _.Silver(input);

		// Assert
		Assert.Equal(4993, actual);
	}

	[Fact( Skip="Not work")]
	public void SampleGold()
	{
		// Arrange
		var input = CreateSample();

		// Act
		var actual = _.Gold(input);

		// Assert
		Assert.Equal(12, actual);
	}

	[Fact]
	public void PuzzleGold()
	{
		//  Arrange
		var input = _.ReadPuzzle();

		// Act
		var actual = _.Gold(input);

		// Assert
		Assert.Equal(21101, actual);
	}

	[Fact]
	public void TestParse() {
		var actual = "510,818 -> 132,818".ToLine();

		Assert.Equal(new Point(510,818), actual.start);
		Assert.Equal(new Point(132,818), actual.end);
	}

	[Fact]
	public void TestDrawLine()
	{
		var field = new VentField();
		var line = "1,1 -> 1,3".ToLine();

		field = field + line;

		Assert.Equal( 3, field.Visited.Count);
		Assert.True( field.Visited.ContainsKey( new Point(1,2)));
		Assert.True( field.Visited.ContainsKey( line.start));
		Assert.True( field.Visited.ContainsKey( line.end));

	}

	[Fact]
	public void TestOverlap()
	{
		var lines = new string[]{
			"2,2 -> 2,1","0,9 -> 5,9","0,9 -> 2,9"};

		var field = lines.Select(l => l.ToLine()).Aggregate(
			new VentField(),
			(accu,current) => accu + current);

		Assert.Equal(3, field.CountOverlaps);

	}
}
