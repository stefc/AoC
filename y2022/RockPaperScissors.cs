using MoreLinq;

namespace advent.of.code.y2022.day2;

// http://adventofcode.com/2022/day/2

class RockPaperScissors : IPuzzle
{
	internal int TotalScore(IEnumerable<string> input)
	{
		return input.Select(ToTuple).Select(t =>(int)Compare(t.left, t.right) + (int)t.right).Sum();
	}
	internal int TotalScoreReact(IEnumerable<string> input)
	{

		var rounds = input.Select(ToResult);

		var predict = rounds.Select<(Shape left, State result), (Shape left, Shape right)>(x =>
		{
			if (x.result == State.Draw)
			{
				return (x.left, x.left);
			}
			else if (x.result == State.Lost)
			{
				if (x.left == Shape.Paper) return (x.left, Shape.Rock);
				else if (x.left == Shape.Rock) return (x.left, Shape.Scissors);
				return (x.left, Shape.Paper);
			}
			else
			{
				if (x.left == Shape.Paper) return (x.left, Shape.Scissors);
				else if (x.left == Shape.Rock) return (x.left, Shape.Paper);
				return (x.left, Shape.Rock);
			}
		});

		return predict.Select(t =>(int)Compare(t.left, t.right) + (int)t.right).Sum();
	}

	public long Silver(IEnumerable<string> input) => TotalScore(input);

	public long Gold(IEnumerable<string> input) => TotalScoreReact(input);

	internal enum Shape
	{
		Rock = 1,
		Paper = 2,
		Scissors = 3
	}

	internal enum State
	{
		Lost = 0,
		Draw = 3,

		Win = 6

	}

	private State Compare(Shape left, Shape right)
	{
		if (left == right) return State.Draw;
		else if ((left == Shape.Paper && (right == Shape.Rock)) ||
			(left == Shape.Rock && (right == Shape.Scissors)) ||
			(left == Shape.Scissors && (right == Shape.Paper))) return State.Lost;
		return State.Win;
	}

	private Shape ToShape(char ch)
	{
		switch (ch)
		{
			case 'A' or 'X': return Shape.Rock;
			case 'B' or 'Y': return Shape.Paper;
			case 'C' or 'Z': return Shape.Scissors;
			default: throw new ArgumentException();
		}
	}

	private State ToState(char ch)
	{
		switch (ch)
		{
			case 'X': return State.Lost;
			case 'Y': return State.Draw;
			case 'Z': return State.Win;
			default: throw new ArgumentException();
		}
	}

	private (Shape left, Shape right) ToTuple(string round)
	=> (ToShape(round.First()), ToShape(round.Last()));

	private (Shape left, State result) ToResult(string round)
	=> ((ToShape(round.First()), ToState(round.Last())));
}
