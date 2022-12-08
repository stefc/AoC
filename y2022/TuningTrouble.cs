using MoreLinq;

namespace advent.of.code.y2022;

// http://adventofcode.com/2022/day/6

class TuningTrouble : IPuzzle
{

	internal long Find(IEnumerable<string> input, int differs)
	{
		var data = input.First().Window(differs).Select( x => x.Distinct().Count() == differs);

		var x = data.Select ( (f,i) => new {f,i}).First( a => a.f).i; 
		return x+differs;
	}
	public long Silver(IEnumerable<string> input) => Find(input, 4);

	public long Gold(IEnumerable<string> input) => Find(input, 14);

}
