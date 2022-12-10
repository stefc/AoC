using System.Linq;
using MoreLinq;

namespace advent.of.code.y2022;

// http://adventofcode.com/2022/day/9

using Pt = Point; 
using Visited = ImmutableHashSet<Point>;

class RopeBridge : IPuzzle
{
	internal record Move( int Len, Pt direction); 

	internal long Count(IEnumerable<string> input) {
		var moves = input.Select(ToMove);
		var head = Pt.Zero;
		var tail = head;
		var visited = Visited.Empty.Add(head);

		var state = (head:head, tail:tail, visited: visited); 

		var movesRepated = moves.SelectMany(x => Enumerable.Repeat(x.direction, x.Len));
		var newState = movesRepated.Aggregate( state, OneMove, acc => acc.visited);
		return newState.Count;
	}

	private (Pt head, Pt tail, Visited visited) OneMove((Pt head, Pt tail, Visited visited) acc, Pt cur)
	{
		var newHead = acc.head + cur;
		var delta = newHead - acc.tail;
		var distance = delta.SquareLength;
		if (distance > 2) {
			var angle = ToAngle(delta);
			var dir = angle switch {
				_ when angle == 0 => Pt.East,
				_ when angle == 180 => Pt.West, 
				_ when angle == 90 => Pt.South,
				_ when angle == 270 => Pt.North,
				_ when angle > 0 && angle < 90 => Pt.SouthEast,
				_ when angle > 90 && angle < 180 => Pt.SouthWest,
				_ when angle > 180 && angle < 270 => Pt.NorthWest,
				_ => Pt.NorthEast,
			};
			var newTail = acc.tail + dir;
			return (newHead,newTail, acc.visited.Add(newTail));
		}
		return (newHead, acc.tail, acc.visited);
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

	public long Silver(IEnumerable<string> input) => Count(input);

	public long Gold(IEnumerable<string> input) => 0;
}
