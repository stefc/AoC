using advent.of.code.y2021.day15;

namespace advent.of.code.tests.y2021;

[Trait("Year", "2021")]
[Trait("Day", "14")]
public class TestDay15 : IPuzzleTest
{
	private readonly IPuzzle _ = new Chiton();

	private IEnumerable<string> CreateSampleSmall() {
		string input = @"116
138
581";
		return input.Split("\n")
				.ToArray();
	}


	private IEnumerable<string> CreateSample() {
		string input = @"1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581";
		return input.Split("\n")
				.ToArray();
	}
	
	[Fact]
	public void SampleSilver() => Assert.Equal(40, _.Silver(CreateSample()));
	
	[Fact]
	public void PuzzleSilver() => Assert.Equal(2703, _.Silver(_.ReadPuzzle()));
	
	[Fact(Skip="Noch unfertig")]
	public void SampleGold() => Assert.Equal(2188189693529, _.Gold(CreateSample()));
		
	[Fact(Skip="Noch unfertig")]
	public void PuzzleGold() => Assert.Equal(2984946368465, _.Gold(_.ReadPuzzle()));

	[Fact]
	public void SpreadValues()	
	{
		
		var act = Chiton.Spread("3694931569");
		Assert.Equal(act, "4715142671");

		act = Chiton.Spread("3694931569",2);
		Assert.Equal(act, "5826253782");
		
		act = Chiton.SpreadHorizontal("1163751742");
		Assert.Equal(act, "11637517422274862853338597396444961841755517295286");
		
	}

}
