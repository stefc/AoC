using advent.of.code.y2021.day16;

namespace advent.of.code.tests.y2021;

[Trait("Year", "2021")]
[Trait("Day", "16")]
public class TestDay16 : IPuzzleTest
{
	private readonly IPuzzle _ = new PacketDecoder();

	private IEnumerable<string> CreateSample() {
		string input = @"D2FE28";
		return input.Split("\n")
				.ToArray();
	}
	
	[Theory]
	[InlineData("8A004A801A8002F478", 16)]
	[InlineData("620080001611562C8802118E34", 12)]
	[InlineData("C0015000016115A2E0802F182340", 23)]
	[InlineData("A0016C880162017C3686B18A3D4780", 31)]
	public void SampleSilver(string input, int expected) => Assert.Equal(expected, _.Silvered(input));
	
	[Fact]
	public void PuzzleSilver() => Assert.Equal(2703, _.Silver(_.ReadPuzzle()));
	
	[Fact(Skip="Noch unfertig")]
	public void SampleGold() => Assert.Equal(2188189693529, _.Gold(CreateSample()));
		
	[Fact(Skip="Noch unfertig")]
	public void PuzzleGold() => Assert.Equal(2984946368465, _.Gold(_.ReadPuzzle()));

	[Fact]
	public void ParseLiteral()	
	{
		var bytes = PacketDecoder.DecodeFromHexToBytes("D2FE28");
		Assert.Equal(new byte[]{0xD2,0xFE,0x28}, bytes);
		Assert.Equal(new byte[]{0b11010010,0b11111110,0b00101000}, bytes);

		var memStream = new MemoryStream(bytes);
		var paket = PacketDecoder.TakeHeader( memStream).Single();


		Assert.Equal(6, paket.version);
		Assert.Equal(PacketDecoder.TypeId.Literal, paket.typeID);

		var literal = paket as PacketDecoder.Literal; 
		Assert.Equal(2021, literal.value);
	}

	[Fact]
	public void ParseOperator()	
	{
		var bytes = PacketDecoder.DecodeFromHexToBytes("38006F45291200");
		Assert.Equal(new byte[]{0x38,0x00,0x6F,0x45,0x29,0x12,0x00}, bytes);
		Assert.Equal(new byte[]{0b0011_1000, 0b00000000, 0b01101111, 0b01000101, 0b00101001, 0b00010010, 0b00000000}, bytes);

		var memStream = new MemoryStream(bytes);
		var paket = PacketDecoder.TakeHeader( memStream).Single();

		Assert.Equal(1, paket.version);
		Assert.Equal(PacketDecoder.TypeId.Operator, paket.typeID);

		var op = paket as PacketDecoder.Operator; 
		// Assert.Equal(2021, literal.value);
	}

}
