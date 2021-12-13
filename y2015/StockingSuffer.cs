// http://adventofcode.com/2015/day/4

using System.Security.Cryptography;
using System.Text;

namespace advent.of.code.y2015.day4;

public class StockingSuffer : IPuzzle
{

	public static int FindLowestNumber6(string secret)
	{
		var md5 = MD5.Create();
		return Enumerable
			.Range(0, 10000000)
			.First(number => {
				var bytes = md5.ComputeHash(Encoding.ASCII.GetBytes($"{secret}{number}")).AsSpan(0, 3);
				return (bytes[0] == 0) && (bytes[1] == 0) && (bytes[2] == 0); 
			});
	}

	public static int FindLowestNumber5(string secret)
	{
		var md5 = MD5.Create();
		return Enumerable
			.Range(0, 10000000)
			.First(number => {
				var bytes = md5.ComputeHash(Encoding.ASCII.GetBytes($"{secret}{number}")).AsSpan(0, 3);
				return (bytes[0] == 0) && (bytes[1] == 0) && ((bytes[2] & 0xF0) == 0);
			});
	}


	private static bool HasPrefix(string prefix, byte[] hash)
	=> BitConverter.ToString(hash).Replace("-","").StartsWith(prefix);

	public long Gold(IEnumerable<string> values) => 
		FindLowestNumber6(values.Single());
	
	public long Silver(IEnumerable<string> values) => 
		FindLowestNumber5(values.Single());
}
