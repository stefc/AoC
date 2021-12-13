namespace advent.of.code.y2021.day13;

// http://adventofcode.com/2021/day/13

public class TransparentOrigami : IPuzzle
{
	public long Silver(IEnumerable<string> values) => Parse(values).OneFolding().Dots.Count;
	
	public long Gold(IEnumerable<string> values) => Parse(values).CompleteFolding().Count();

	internal static Instructions Parse(IEnumerable<string> values)
	{
		var dots = values
			.Where( l => l.Contains(','))
			.Select( l => Point.FromString(l))
			.ToImmutableHashSet();
		
		var folds = values
			.SkipWhile( l => !l.StartsWith("fold along "))
			.Select( l => Fold.FromString(l))
			.ToImmutableList();

		return new Instructions(dots, folds);
	}

	internal static string Visualize(IEnumerable<Point> dots) {
		int w = dots.Max( xy => xy.X);
		int h = dots.Max( xy => xy.Y);

		return String.Join('\n', 
			 Enumerable.Range(0, h+1)
			.Select(y => String.Concat(
				Enumerable.Range(0, w+1).Select( x => dots.Contains(new Point(x,y)) ? '*':' ')))
			.ToArray());
	}

	public record struct Instructions (ImmutableHashSet<Point> Dots, ImmutableList<Fold> Folds) {

		public Instructions OneFolding() {

			var fold = Folds.First();

			if (fold.At.X == 0) 
			{
				var y = fold.At.Y;
				var newDots = Dots.Where( dot => dot.Y < y).ToImmutableHashSet().Union(
						Dots.Where( dot => dot.Y > y).Select( dot => new Point(dot.X, y - ( dot.Y - y))).ToImmutableHashSet());
				
				return this with { Dots = newDots, Folds = Folds.Remove(fold)};
			}
			else 
			{
				var x = fold.At.X;
				var newDots = Dots.Where( dot => dot.X < x).ToImmutableHashSet().Union(
						Dots.Where( dot => dot.X > x).Select( dot => new Point(x - ( dot.X - x),dot.Y)).ToImmutableHashSet());
				
				return this with { Dots = newDots, Folds = Folds.Remove(fold)};
			}
		}

		public IEnumerable<Point> CompleteFolding() 
		{
			var instructions = this;
			while (instructions.Folds.Any()) {
				instructions = instructions.OneFolding();	
			}

			return instructions.Dots;
		}
	}

	public record struct Fold (Point At) {

		public static Fold FromString(string s) {
			var parts = s.Split('=');
			var xy = Convert.ToInt32(parts[1]);
			return (parts[0].Last()=='x') ? new Fold( new Point(xy,0)) : new Fold( new Point(0, xy));
		}
	}



}
