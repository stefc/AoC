namespace advent.of.code.y2022;

// http://adventofcode.com/2022/day/9

using Pt = Point;
using Visited = ImmutableHashSet<Point>;
using Rope = ImmutableList<Point>;

class RopeBridge : IPuzzle
{
	internal record Move(int Len, Pt direction);

	internal long Count(IEnumerable<string> input, int len)
	=> input
			.Select(ToMove)
			.SelectMany(x => Enumerable.Repeat(x.direction, x.Len))
			.Aggregate(
				(rope: Enumerable.Repeat(Pt.Zero, len).ToImmutableList(), visited: Visited.Empty),
				RopeMove,
				acc => acc.visited.Count);

	private (Rope rope, Visited visited) RopeMove((Rope rope, Visited visited) acc, Pt cur)
	{
		var head = acc.rope.Head() + cur;
		var tail = acc.rope.Tail();
		var newRope = Rope.Empty.Add(head);
		while (tail.IsNotEmpty())
		{
			var nextTail = MoveTail(new[] { head, tail.First() });
			newRope = newRope.Add(nextTail);
			head = nextTail;
			tail = tail.Tail();
		}
		return (newRope, acc.visited.Add(newRope.Last()));
	}


	private Pt MoveTail(IEnumerable<Pt> rope)
	{
		var head = rope.Head();
		var tail = rope.Tail().Head();
		var delta = head - tail;
		if (delta.SquareLength > 2)
		{
			var angle = ToAngle(delta);
			var dir = angle switch
			{
				_ when angle == 0 => Pt.East,
				_ when angle == 180 => Pt.West,
				_ when angle == 90 => Pt.South,
				_ when angle == 270 => Pt.North,
				_ when angle > 0 && angle < 90 => Pt.SouthEast,
				_ when angle > 90 && angle < 180 => Pt.SouthWest,
				_ when angle > 180 && angle < 270 => Pt.NorthWest,
				_ => Pt.NorthEast,
			};
			return tail + dir;
		}
		return tail;
	}

	private int ToAngle(Pt xy)
	=> (int)Math.Round(Math.Atan2(xy.Y, xy.X) * 180 / Math.PI + 360) % 360;

	internal Move ToMove(string command) =>
		new Move(int.Parse(command.Substring(2)),
			command.First() switch
			{
				'R' => Pt.East,
				'L' => Pt.West,
				'U' => Pt.South,
				'D' => Pt.North,
				_ => throw new ArgumentOutOfRangeException()
			});

	public long Silver(IEnumerable<string> input) => Count(input, 2);

	public long Gold(IEnumerable<string> input) => Count(input, 10);
}
