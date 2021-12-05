namespace advent.of.code.y2021.day4;

// http://adventofcode.com/2021/day/4

internal class GiantSquid : IPuzzle
{
	public int Silver(IEnumerable<string> values)
	{
		var draws = values.First().ToNumbers();

		var boards = values
				.Skip(1)
				.Chunk(5)
				.Select(arr => new BoardState(arr))
   				.ToArray();

		var enumerator = draws.GetEnumerator();
		while (enumerator.MoveNext()) {
			var draw = enumerator.Current; 

			boards = boards.Select( board => board + draw ).ToArray();

			var winner = boards.SingleOrDefault( board => board.IsBingo);
			if (winner != null)
			{
				return winner.Score * draw;
			}
		}
			
		return 0;
	}



	public int Gold(IEnumerable<string> values) {
		var draws = values.First().ToNumbers();

		var boards = values
				.Skip(1)
				.Chunk(5)
				.Select(arr => new BoardState(arr))
   				.ToArray();

		var enumerator = draws.GetEnumerator();
		while (enumerator.MoveNext()) {
			var draw = enumerator.Current; 

			boards = boards.Select( board => board + draw ).ToArray();

			if (boards.Count() == 1) {
				var winner = boards.SingleOrDefault( board => board.IsBingo);
				if (winner != null)
				{
					return winner.Score * draw;
				}
			}

			boards = boards.Where( board => !board.IsBingo).ToArray();
		}
		return 0;
	}
	

	public record Winner (BoardState Board, int LastDraw) {};
}

public record BoardState
{
	public int Width { get; init; }

	public ImmutableDictionary<int,Point> Board { get; init; }
	public ImmutableHashSet<int> Drawn { get; init; }

	public BoardState(int[][] numbers)
	{
		Width = numbers.Length;
		Board = Enumerable.Range(0, Width).SelectMany(x => Enumerable.Range(0, Width).Select(y => new Point(x, y)))
			.Aggregate(ImmutableDictionary<int,Point>.Empty,
				(accu, current) =>
				{
					var number = numbers[current.Y][current.X];
					return accu.Add(number,current);
				});

		Drawn = ImmutableHashSet<int>.Empty;
	}

	public BoardState(IEnumerable<string> values) : this( ToMatrix(values))
	{
	}

	private static int[][] ToMatrix(IEnumerable<string> lines) 
		=> lines.Select( l => l.ToNumbers(3)).ToArray();
	
	public BoardState Draw(int number) 
		=> (Board.ContainsKey(number)) ? this with { Drawn = this.Drawn.Add(number) } : this;

	public static BoardState operator + (BoardState state, int number)
		=> state.Draw(number);
	
	public bool IsBingo => Enumerable.Range(0, Width).Any(
		x => RowComplete(x) || ColumnComplete(x)); 

	public int Score => Board.Keys.Sum() - Drawn.Sum();
	
	private bool RowComplete(int row) {

		var points = Enumerable.Range(0, Width).Aggregate( 
			ImmutableHashSet<Point>.Empty, (accu,col) => accu.Add(new Point(col,row)));

		var correct = Drawn.Aggregate(ImmutableHashSet<Point>.Empty, 
			(accu, current) => accu.Add( Board[current]));

		return points.Except(correct).IsEmpty;  
	}

	private bool ColumnComplete(int col) {

		var points = Enumerable.Range(0, Width).Aggregate( 
			ImmutableHashSet<Point>.Empty, (accu,row) => accu.Add(new Point(col,row)));

		var correct = Drawn.Aggregate(ImmutableHashSet<Point>.Empty, 
			(accu, current) => accu.Add( Board[current]));

		return points.Except(correct).IsEmpty;  
	}
}



