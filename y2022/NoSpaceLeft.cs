
namespace advent.of.code.y2022;

// http://adventofcode.com/2022/day/7

using Store = ImmutableDictionary<string, long>;
using Path = ImmutableStack<string>;

class NoSpaceLeft : IPuzzle
{
	internal long Calc(IEnumerable<string> input, bool toDelete)
	{
		var result = Traverse(input);
		return toDelete ?
			result.Values.Where(size => size >= 30000000 - (70000000 - result.Values.Max())).Min() : 
			result.Values.Where( size => size <= 100_000).Sum();
	}
	
	private Store Traverse(IEnumerable<string> input) 
	=> input.Aggregate(
		(res: Store.Empty, path: Path.Empty),
		(acc, cur) =>
		{
			if (cur.StartsWith("$ cd "))
			{
				var dir = cur.Substring(5);
				return (acc.res, dir == ".." ? acc.path.Pop() : acc.path.Push(dir));
			}
			else
			{
				var parts = cur.Split(' ');
				if (long.TryParse(parts[0], out var fileSize))
				{
					return (AddFile(acc.res, acc.path, fileSize), acc.path);
				}
			}
			return acc;
		},
		acc => acc.res);

	private Store AddFile(Store accu, Path cwd, long fileSize) 
	=> cwd.Aggregate(
		(res: accu, dir: cwd),
		(acc, _) => (AddSize(acc.res, ToFullPath(acc.dir), fileSize), acc.dir.Pop()),
		acc => acc.res);

	private string ToFullPath(Path path)
	=> path.Reverse().Aggregate(
		string.Empty, 
		(acc, cur) => string.Concat(acc, "-", cur));

	private Store AddSize(Store state, string path, long fileSize) 
	=>	state.TryGetValue(path, out var value) ?
		state.SetItem(path, value + fileSize) :
		state.Add(path, fileSize);

	public long Silver(IEnumerable<string> input) => Calc(input, false);

	public long Gold(IEnumerable<string> input) => Calc(input, true);
}