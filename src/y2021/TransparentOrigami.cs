using System.Threading.Channels;

namespace advent.of.code.y2021.day13;

// http://adventofcode.com/2021/day/13

public class TransparentOrigami : IPuzzle
{
	public long Silver(IEnumerable<string> values)  
	{
		var instructions = Parse(values);
		return instructions
			.SinkStage(instructions.Folding(instructions.PumpPoints(), instructions.Folds.First()))
			.GetAwaiter()
			.GetResult();
	}
	
	public long Gold(IEnumerable<string> values) {

		var instructions = Parse(values);

		var sink = instructions.Folds.Aggregate(
			instructions.PumpPoints(), 
			(acc, cur) => instructions.Folding(acc, cur), 
			acc => instructions.SinkStage(acc));
		
		return sink.GetAwaiter().GetResult();
	 }

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

		public ChannelReader<Point> PumpPoints() 
		{
			var output = Channel.CreateUnbounded<Point>();

			var dots = Dots;
			Task.Run( async () => 
			{
				foreach(var dot in dots) 
					await output.Writer.WriteAsync(dot);
				output.Writer.Complete();
			});

			return output;
		}

		public ChannelReader<Point> Folding(ChannelReader<Point> input, Fold fold)
		{
			var output = Channel.CreateUnbounded<Point>();

			var dots = ImmutableHashSet<Point>.Empty;
			Task.Run(async () => 
			{
				await foreach (var xy in input.ReadAllAsync()) 
				{
					var newDot = fold.Mirror(xy);
					if (!dots.Contains(newDot)) 
					{
						dots = dots.Add(newDot);
						await output.Writer.WriteAsync(newDot);
					}	
				}
				output.Writer.Complete();
			});

			return output;
		}

		public async Task<int> SinkStage(ChannelReader<Point> input) {

			var counter = 0;
			await foreach (var xy in input.ReadAllAsync()) {
				counter++;
			}
			return counter;
		}
	}



	public record struct Fold (Point At) {

		public static  Fold FromString(string s) {
			var parts = s.Split('=');
			var xy = Convert.ToInt32(parts[1]);
			return (parts[0].Last()=='x') ? new Fold( new Point(xy,0)) : new Fold( new Point(0, xy));
		}

		public Point Mirror(Point dot) 
		{
			if (At.X == 0) 
			{
				var y = At.Y;
				return (dot.Y < y) ? dot : dot with { Y =( y << 1) - dot.Y};
			}
			else 
			{
				var x = At.X;
				return (dot.X < x) ? dot : dot with { X = (x << 1) - dot.X};
			}
		}
	}
}
