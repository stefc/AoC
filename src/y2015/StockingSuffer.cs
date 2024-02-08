// http://adventofcode.com/2015/day/4

using System.Security.Cryptography;
using System.Text;

namespace advent.of.code.y2015.day4;

public class StockingSuffer : IPuzzle
{

	public static int FindLowestNumber6(string secret)
	{
		var md5 = MD5.Create();
		var bytes = new byte[16];
		var salt  = new byte[16];
		return Enumerable
			.Range(0, 10000000)
			.FirstOrDefault(number => {
				var len = Encoding.ASCII.GetBytes($"{secret}{number}", salt);
				if (md5.TryComputeHash(salt.AsSpan(0,len), bytes, out var written)) {
					return (bytes[0] == 0) && (bytes[1] == 0) && (bytes[2] == 0); 
				}
				return false;
			});
	}

	public static int FindLowestNumber5(string secret)
	{
		var md5 = MD5.Create();
		var bytes = new byte[16];
		var salt  = new byte[16];
		return Enumerable
			.Range(0, 10000000)
			.FirstOrDefault(number => {
				var len = Encoding.ASCII.GetBytes($"{secret}{number}", salt);
				if (md5.TryComputeHash(salt.AsSpan(0,len), bytes, out var written)) {
					return (bytes[0] == 0) && (bytes[1] == 0) && ((bytes[2] & 0xF0) == 0);
				}
				return false;
			});
	}


	private static bool HasPrefix(string prefix, byte[] hash)
	=> BitConverter.ToString(hash).Replace("-","").StartsWith(prefix);

	public long Gold(IEnumerable<string> values) => 
		FindLowestNumber6(values.Single());
	
	public long Silver(IEnumerable<string> values) => 
		FindLowestNumber5(values.Single());
}
