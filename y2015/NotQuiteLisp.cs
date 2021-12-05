// http://adventofcode.com/2015/day/1

namespace advent.of.code.y2015.day1;

public class NotQuiteLisp : IPuzzle
{
	public int WhatFloor(string instructions) =>
		instructions
			.Select(MovementSign)
			.Sum();

	public int HowManyMovesToBasement(string instructions) =>
		instructions
			.Select(MovementSign)
			.Aggregate(
				seed: (count: 0, floor: 0),
				func: (accu, current)
					=> accu.floor < 0 ? accu : (count: accu.count + 1, floor: accu.floor + current),
				resultSelector: accu => accu.count);

	private static int MovementSign(char movement) => movement == '(' ? +1 : -1;

	public int Silver(IEnumerable<string> values) => WhatFloor(values.Single());

	public int Gold(IEnumerable<string> values) => HowManyMovesToBasement(values.Single());
}
