
namespace advent.of.code.y2022;

// http://adventofcode.com/2022/day/10

using Store = ImmutableDictionary<int, int>;

class CathodeRayTube : IPuzzle
{
	internal long Calc(IEnumerable<string> input)
	{
		var program = input.Select(ToCommand).ToArray();

		var registerX = new []{20,60,100,140,180,220}.Aggregate(Store.Empty, 
			(acc,cur) => acc.Add(cur, 0));
		
		var res = program.Aggregate( (reg:registerX, x: 1, cycle: 0), 
			(acc,cur) => {

				return cur switch {
					NoopCmd cmd => (reg:Save(acc.reg, acc.cycle+1, acc.x), 
						x:acc.x, acc.cycle+1),
					AddxCmd cmd => (reg:Save(Save(acc.reg, acc.cycle+1, acc.x),acc.cycle+2,acc.x), 
						x:acc.x+cmd.Arg, acc.cycle+2),
					_ => acc
				};
			},
			acc => acc.reg.Values.Sum());

		return res;
	}

	internal long CalcCrt(IEnumerable<string> input)
	{
		var program = input.Select(ToCommand).ToArray();

		var width = 40; 
		var height = 6;

		var crt = new BitArray(width * height, false);

		

		return 0; 
	}

	
	private Store Save(Store store, int cycle, int x) => store.Keys.Contains(cycle) ? store.SetItem(cycle, x*cycle) : store;
	

	public long Silver(IEnumerable<string> input) => Calc(input);

	public long Gold(IEnumerable<string> input) => CalcCrt(input);

	internal Cmd ToCommand(string line)
	{
		var parts = line.Split(' ');
		int arg1 = 0;
		return (parts[0], parts.Length > 1 ? int.TryParse(parts[1], out arg1) : false) switch
		{
			("addx", true) => new AddxCmd(arg1),
			_ => new NoopCmd()
		};
	}

	internal record Cmd(int Cycle) { }

	internal record NoopCmd() : Cmd(1)
	{
	}

	internal record AddxCmd(int Arg) : Cmd(2)
	{
	}
}