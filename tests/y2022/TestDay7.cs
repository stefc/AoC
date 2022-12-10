using advent.of.code.y2022;

namespace advent.of.code.tests.y2022;

[Trait("Year", "2022")]
[Trait("Day", "7")]
public class TestDay7 : IPuzzleTest
{
	private readonly IPuzzle _ = new NoSpaceLeft();

	private readonly string[] input;

	public TestDay7() => this.input = _.ReadPuzzle().ToArray();

	private IEnumerable<string> CreateSample()
	=> @"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k".Split("\n").ToArray();

	[Fact] public void SampleSilver() => Assert.Equal(95437, _.Silver(CreateSample()));
	
	[Fact] public void SampleGold() => Assert.Equal(24933642, _.Gold(CreateSample()));

	public void PuzzleSilver() => _.Silver(this.input);
	public void PuzzleGold() => _.Gold(this.input);

	[Fact] public void TestSilver() => Assert.Equal(1232307, _.Silver(this.input));

	[Fact] public void TestGold() => Assert.Equal(7268994, _.Gold(this.input));

}