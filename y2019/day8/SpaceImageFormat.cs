// http://adventofcode.com/2019/day/8

namespace advent.of.code.y2019.day8;

public static class SpaceImageFormat
{
	public static int GetDigitsCount(Point dimensions)
	=> dimensions.X * dimensions.Y;

	public static int GetLayerCount(string content, Point dimensions)
	=> content.Length / GetDigitsCount(dimensions);


	public static IEnumerable<string> GetLayers(this string content, Point dimensions)
	{
		var layers = GetLayerCount(content, dimensions);
		var sizeOfLayer = GetDigitsCount(dimensions);
		return Enumerable.Range(0, layers)
			.Select(layer =>
			   content[(layer * sizeOfLayer)..((layer + 1) * sizeOfLayer)]);
	}

	public static int CalcCheckSum(this string content, Point dimensions)
	=> content.GetLayers(dimensions)
		.MinBy(layer => layer.Where(ch => ch == '0').Count())
		.ToDigits()
		.Where(digit => digit == 1 || digit == 2)
		.GroupBy(x => x)
		.Select(grp => grp.Count())
		.Aggregate(1, (accu, current) => accu * current);

	public static string Decode(this string content, Point dimensions)
	{
		var layers = content.GetLayers(dimensions).Reverse();

		return layers.Aggregate((accu, current)
	   => Add(accu, current));
	}

	public static char Add(char a, char b)
	=> (b == '2') ? a : b;

	public static string Add(string a, string b)
	=> string.Concat(a.Zip(b, (aa, bb) => Add(aa, bb)));
}
