using advent.of.code.y2021.day13;

namespace advent.of.code.tests.y2021;

[Trait("Year", "2021")]
[Trait("Day", "13")]
public class TestDay13 : IPuzzleTest
{
	private readonly IPuzzle _ = new TransparentOrigami();

	private IEnumerable<string> CreateSample() {
		string input = @"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5";	
		return input.Split("\n")
				.ToArray();
	}
	
	[Fact]
	public void SampleSilver() => Assert.Equal(17, _.Silver(CreateSample()));
	
	[Fact]
	public void PuzzleSilver() => Assert.Equal(647, _.Silver(_.ReadPuzzle()));
	
	[Fact]
	public void SampleGold() => Assert.Equal(16, _.Gold(CreateSample()));
		
	[Fact]
	public void PuzzleGold() => Assert.Equal(93, _.Gold(_.ReadPuzzle()));
	
}