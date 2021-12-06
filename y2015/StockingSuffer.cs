// http://adventofcode.com/2015/day/4

using System.Security.Cryptography;
using System.Text;

namespace advent.of.code.y2015.day4;

public class StockingSuffer : IPuzzle
{

	public static int FindLowestNumber(string secret, int prefix = 5)
	{
		var startPrefix = new String('0', prefix);
		var md5 = MD5.Create();
		return Enumerable
			.Range(0, 10000000)
			.First(number => HasPrefix(
			   startPrefix,
			   md5.ComputeHash(
				   Encoding.ASCII.GetBytes($"{secret}{number}"))));
	}

	private static bool HasPrefix(string prefix, byte[] hash)
	=> BitConverter.ToString(hash).Replace("-","").StartsWith(prefix);

	public long Gold(IEnumerable<string> values) => 
		FindLowestNumber(values.Single(), 6);
	
	public long Silver(IEnumerable<string> values) => 
		FindLowestNumber(values.Single());
}
