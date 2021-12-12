using advent.of.code.y2021.day12;

namespace advent.of.code.tests.y2021;

[Trait("Year", "2021")]
[Trait("Day", "12")]
public class TestDay12 : IPuzzleTest
{
	private readonly IPuzzle _ = new PassagePathing();

	private IEnumerable<string> CreateSample() {
		string input = @"
start-A
start-b
A-c
A-b
b-d
A-end
b-end
";	
		return input.Split("\n")
				.Where(line => !String.IsNullOrEmpty(line))
				.ToArray();
	}
	
	private IEnumerable<string> CreateSampleLarger() {
		string input = @"
dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc
";	
		return input.Split("\n")
				.Where(line => !String.IsNullOrEmpty(line))
				.ToArray();
	}

	private IEnumerable<string> CreateSampleHuge() {
		string input = @"
fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW
";	
		return input.Split("\n")
				.Where(line => !String.IsNullOrEmpty(line))
				.ToArray();
	}
	
	[Fact]
	public void SampleSilver() {
		Assert.Equal(10, _.Silver(CreateSample()));
		Assert.Equal(19, _.Silver(CreateSampleLarger()));
		Assert.Equal(226, _.Silver(CreateSampleHuge()));
	}
	
	[Fact]
	public void PuzzleSilver() => Assert.Equal(4411, _.Silver(_.ReadPuzzle()));
	
	[Fact]
	public void SampleGold() 
	{
		Assert.Equal(36, _.Gold(CreateSample()));
		Assert.Equal(103, _.Gold(CreateSampleLarger()));
		Assert.Equal(3509, _.Gold(CreateSampleHuge()));
	}
	
	[Fact]
	public void PuzzleGold() => Assert.Equal(136767, _.Gold(_.ReadPuzzle()));
}