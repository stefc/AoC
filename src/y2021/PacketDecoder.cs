using MoreLinq;
using MoreLinq.Extensions;

namespace advent.of.code.y2021.day16;

// http://adventofcode.com/2021/day/16

public class PacketDecoder : IPuzzle
{
	public long Silver(IEnumerable<string> values) => 42;

	public long Gold(IEnumerable<string> values) => 42;

	public static BitArray DecodeFromHex(string hex)
	{
		var bytes = hex.Chunk(2).Select(chars => SwapNibbles(Convert.ToByte(new String(chars), 16))).ToArray();
		return new BitArray(bytes);
	}

	public static byte[] DecodeFromHexToBytes(string hex)
	{
		var bytes = hex.Chunk(2).Select(chars => Convert.ToByte(new String(chars), 16)).ToArray();
		return bytes;
	}

	public static IEnumerable<Paket> TakeHeader(Stream stream)
	{
		while (stream.CanRead)
		{

			var bytes = new byte[3];
			if (stream.Read(bytes, 0, 2) != 2)
				yield break;

			// 11010010 11111110 00101000 
			// VVVTTTAA AAABBBBB CCCCC___ 
			var version = Convert.ToByte((bytes[0] & 0b_1110_0000) >> 5);
			var typeId = (TypeId)((bytes[0] & 0b_0001_1100) >> 2);

			if (typeId == TypeId.Literal)
			{
				stream.Read(bytes, 2, 1);
				var b1 = ((bytes[0] & 0b_0000_0011) << 3) | ((bytes[1] & 0b_1110_0000) >> 5);
				var b2 = (bytes[1] & 0b_0001_1111);
				var b3 = (bytes[2] & 0b_1111_1000) >> 3;

				var literal = (b3 & 0b1111) | ((b2 & 0b1111) << 4) | ((b1 & 0b1111) << 8);
				yield return new Literal(version, typeId, literal);
			}
			else
			{
				var isImmediatelyLengthType = Convert.ToBoolean((bytes[0] & 0b00000010) >> 1);
				yield return new Operator(version, typeId, isImmediatelyLengthType);
			}
		}
	}

	private static byte SwapNibbles(byte x) => Convert.ToByte((x & 0x0F) << 4 | (x & 0xF0) >> 4);

	public record class Paket(byte version, TypeId typeID);
	public record class Literal(byte version, TypeId typeID, int value) : Paket(version, typeID);
	public record class Operator(byte version, TypeId typeID, bool isImmediately) : Paket(version, typeID);

	public enum TypeId
	{
		Literal = 0b100, 

		Operator = 0b110
	}
}
