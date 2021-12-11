namespace advent.of.code.y2021.day11;

// http://adventofcode.com/2021/day/11

public class DumboOctopus : IPuzzle
{

	public long Silver(IEnumerable<string> values)
	=> Parse(values).CountFlashes(100);
	
	public long Gold(IEnumerable<string> values) {
		var field = Parse(values);
		var step = 0;
		while (field.Map.Values.Any( v => v != 0)) {
			field = field.OneStep();
			step++;
		}
		return step;
	}

	internal static Field Parse(IEnumerable<string> values) => Field.OfMatrix(ToMatrix(values));
	
	private static int[][] ToMatrix(IEnumerable<string> lines) 
	=> lines.AsParallel().Select( l => l.ToDigits().ToArray()).ToArray();
	
	internal record struct Field (ImmutableDictionary<SmallPoint,int> Map)
	{
		private static SmallPoint[] Adjacents = new SmallPoint[]{
			SmallPoint.North,SmallPoint.South,SmallPoint.East,SmallPoint.West,
			SmallPoint.NorthWest,SmallPoint.NorthEast,SmallPoint.SouthEast,SmallPoint.SouthWest
			};

		internal static Field OfMatrix(int[][] numbers)
		{
			var height = numbers.Length;
			var width = numbers[0].Length;
			var map = SmallPoint.Cloud(width, height)
				.Aggregate(ImmutableDictionary<SmallPoint,int>.Empty,
					(acc, cur) => acc.Add(cur, numbers[cur.Y][cur.X]));
			return new Field( map );
		}

		public static ImmutableDictionary<SmallPoint,int> IncreaseLevel(ImmutableDictionary<SmallPoint,int> map)
		=> ImmutableDictionary<SmallPoint,int>.Empty
			.AddRange(map.AsParallel().Select( kvp => KeyValuePair.Create<SmallPoint,int>(kvp.Key, kvp.Value < 9 ? kvp.Value+1:0)));

		public static ImmutableDictionary<SmallPoint,int> IncreaseLevel(ImmutableDictionary<SmallPoint,int> map, ImmutableList<SmallPoint> points)
		=> points.AsParallel().Aggregate( map, (acc,cur) => {
			if (acc.TryGetValue(cur, out var v) && (v != 0))
			{
				return ( v < 9) ? acc.SetItem(cur, v+1) : acc.SetItem(cur, 0);
			}
			return acc;
		});

		public static ImmutableHashSet<SmallPoint> NewFlashes(ImmutableDictionary<SmallPoint,int> map, ImmutableHashSet<SmallPoint> flashes)
		=> map.AsParallel().Where(kvp => kvp.Value == 0 && !flashes.Contains(kvp.Key)).Select( kvp => kvp.Key).ToImmutableHashSet();

		public static ImmutableList<SmallPoint> Hood(ImmutableDictionary<SmallPoint,int> map, ImmutableHashSet<SmallPoint> flashes)
		=> flashes.AsParallel().SelectMany( pt => Adjacents.Select( at => pt + at).Where( pt => map.ContainsKey(pt) && !flashes.Contains(pt) && map[pt] != 0)).ToImmutableList(); 

		public Field OneStep() {
			var map = IncreaseLevel(this.Map);
			var flashes = NewFlashes(map, ImmutableHashSet<SmallPoint>.Empty);
			var totalFlashes = flashes;
			while (!flashes.IsEmpty()) {

				// new Field { Map = map}.Dump();

				var hood = Hood(map, flashes);
				if (hood.IsEmpty()) {
					flashes = flashes.Clear();
				} else {
					map = IncreaseLevel(map, hood);
					flashes = NewFlashes(map, totalFlashes);
					totalFlashes = totalFlashes.Union(flashes);
				}
			}
			return this with { Map = map };
		}

		public int CountFlashes(int steps) {
			return Enumerable.Range(0, steps).Aggregate( (field:this, count:0),
				(accu, _) => {
					var field = accu.field.OneStep();
					var total = accu.count + field.Map.Values.Count( v => v == 0);
					return (field, total);
				},
				accu => accu.count);
		}

		public void Dump() 
		{
			int width = Map.Keys.Max( pt => pt.X);
			int height = Map.Keys.Max( pt => pt.Y);

			System.Console.WriteLine(new String('-', width));

			var map = Map;
			var lines = Enumerable.Range(0, height+1)
				.Select(y => String.Concat(
					Enumerable.Range(0, width+1).Select( x => map[new SmallPoint(x,y)].ToString().First())))
				.ToArray();

			foreach(var line in lines)	
				System.Console.WriteLine(line);
			
		}
	}

}