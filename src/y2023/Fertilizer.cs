using MathNet.Numerics.LinearAlgebra;
using MoreLinq;

namespace advent.of.code.y2023;

// http://adventofcode.com/2023/day/5

public partial class Fertilizer : IPuzzle
{

	public static IEnumerable<(string, Matrix<long>)> ParseAlmanac(IEnumerable<string> values)
	{
		var first = true;
		string name = string.Empty;
		var data = ImmutableArray<long[]>.Empty;
		foreach (var (line, _) in values.Select((line, index) => (line, index)))
		{
			if (first)
			{
				first = false;
				var segments = line.Split(':');
				var seeds = segments.Last().Trim().ToBigNumbers().ToArray();
				yield return ("seeds", Matrix<long>.Build.Dense(1, seeds.Length, seeds));
			}

			if (line.EndsWith(":"))
			{
				if (data != null && name != string.Empty)
					yield return (name, Matrix<long>.Build.DenseOfArray(ConvertJaggedArray(data.ToArray())));
				name = line.TrimEnd(':').Trim();
				data = ImmutableArray<long[]>.Empty;
			}
		}
		if (data != null && name != string.Empty)
			yield return (name, Matrix<long>.Build.DenseOfArray(ConvertJaggedArray(data.ToArray())));
	}

	private static T[,] ConvertJaggedArray<T>(T[][] jaggedArray)
	{
		int rows = jaggedArray.Length;
		int cols = jaggedArray[0].Length;

		T[,] multiArray = new T[rows, cols];
		for (int i = 0; i < rows; i++)
			for (int j = 0; j < cols; j++)
				multiArray[i, j] = jaggedArray[i][j];
		return multiArray;
	}
	
	public long Silver(IEnumerable<string> input)
	{
		return 0;
	}

	public long Gold(IEnumerable<string> input)
	{
		return 0;
	}

}
