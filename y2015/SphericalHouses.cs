// http://adventofcode.com/2015/day/3

namespace advent.of.code.y2015.day3;

public class SphericalHouses : IPuzzle
{

	public static int AtLeastOnePresent(string instructions) => GetVisits(instructions).Count;

	public static int TogetherWithRobodog(string instructions)
	{
		var result = instructions
			.Select((instruction, index) => new { instruction, isSanta = index % 2 == 0 })
			.GroupBy(x => x.isSanta, x => x.instruction)
			.Aggregate(
				seed: ImmutableHashSet<Point>.Empty,
				func: (accu, current) => accu.Union(GetVisits(string.Concat(current))),
				resultSelector: accu => accu.Count);
		return result;
	}

	private static ISet<Point> GetVisits(string instructions)
	{
		var initialResult = ImmutableHashSet<Point>.Empty.Add(Point.Zero);
		return instructions
			.Select(InstructionToPoint)
			.Aggregate(
				seed: (location: Point.Zero, result: initialResult),
				func: (accu, current) =>
				{
					var newLocation = accu.location.Add(current);
					var newResult = accu.result.Add(newLocation);
					return (location: newLocation, result: newResult);
				},
				resultSelector: accu => accu.result
			);
	}
	private static Point InstructionToPoint(char instruction) 
    => instruction switch {
        '<' => Point.West,
        '>' => Point.East,
		'v' => Point.South,
        '^' => Point.North,
        _ => throw new ArgumentException()
    };

	public long Gold(IEnumerable<string> values)
	=> SphericalHouses.TogetherWithRobodog(values.Single());

	public long Silver(IEnumerable<string> values) 
    => SphericalHouses.AtLeastOnePresent(values.Single());
}
