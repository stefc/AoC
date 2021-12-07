
// http://adventofcode.com/2015/day/6

namespace advent.of.code.y2015.day6;

internal class FireHazard : IPuzzle
{
	public static Statement Parse(string statement)
	{
		return new Statement(Command.TurnOn, Point.Zero, new Point(999, 999));
	}

	public long Silver(IEnumerable<string> values)
	{
		var grid = values
			.Select(FireHazard.Statement.FromString)
			.Aggregate(
				seed: new FireHazard.LightGrid(1000, 1000),
				func: (accu, current) => accu.Operation(current)
				);
		return grid.LightCount;
	}

	public long Gold(IEnumerable<string> values)
	{
		var grid = values
			.Select(FireHazard.Statement.FromString)
			.Aggregate(
				seed: new FireHazard.BrightnessGrid(1000, 1000),
				func: (accu, current) => accu.Operation(current)
				);
		return grid.TotalBrightness;
	}

	internal enum Command
	{
		TurnOn,
		TurnOff,
		Toggle
	}
	internal class Statement
	{
		public Command Command { get; private set; }
		public Point From { get; private set; }
		public Point Through { get; private set; }

		public Statement(Command command, Point from, Point through)
		{
			this.Command = command;
			this.From = from;
			this.Through = through;
		}

		public static Statement FromString(string code)
		{
			const string pattern = @"(?'command'turn on|turn off|toggle) (?'from'\d{1,3},\d{1,3}) through (?'to'\d{1,3},\d{1,3})";

			RegexOptions options = RegexOptions.IgnoreCase;

			Match match = Regex.Matches(code, pattern, options).First();


			Group commandGroup = match.Groups["command"];
			Group fromGroup = match.Groups["from"];
			Group toGroup = match.Groups["to"];

			Command command = commandGroup.Value.Contains("toggle") ?
				Command.Toggle : commandGroup.Value.Contains("off") ?
				Command.TurnOff : Command.TurnOn;

			Point from = Point.FromString(fromGroup.Value);
			Point to = Point.FromString(toGroup.Value);

			return new Statement(command, from, to);
		}

	}

	public class Grid
	{
		public int Width { get; private set; }
		public int Height { get; private set; }

		internal Grid(int width, int height)
		{
			Width = width;
			Height = height;
		}

		public int Size => Width * Height;

		public IEnumerable<int> GetIndices(Point from, Point to)
		{
			return Enumerable
			.Range(Math.Min(from.Y, to.Y), Math.Abs(to.Y - from.Y) + 1)
			.SelectMany(y => Enumerable
			   .Range(Math.Min(from.X, to.X), Math.Abs(to.X - from.X) + 1)
			   .Select(x => y * Width + x));
		}

	}

	public class LightGrid : Grid
	{

		private readonly BitArray bitmap;

		public LightGrid(int width, int height) :
			this(width, height, new BitArray(width * height, false))
		{
		}

		internal LightGrid(int width, int height, BitArray bitmap) : base(width, height)
		{
			this.bitmap = bitmap;
		}

		public int LightCount
		{
			get
			{
				return Enumerable
					.Range(0, Size)
					.Select(index => bitmap.Get(index))
					.Where(bit => bit)
					.Count();
			}
		}

		private LightGrid GridOperation(Point from, Point to, Func<BitArray, int, BitArray> operation)
		{
			var indices = GetIndices(from, to);

			var newBitmap = indices.Aggregate(
				seed: new BitArray(this.bitmap),
				func: (bits, index) => operation(bits, index));

			return new LightGrid(Width, Height, newBitmap);
		}

		internal LightGrid TurnOff(Point from, Point to)
		{
			return GridOperation(from, to, OperationClear);
		}
		internal LightGrid TurnOn(Point from, Point to)
		{
			return GridOperation(from, to, OperationSet);
		}

		internal LightGrid Toggle(Point from, Point to)
		{
			return GridOperation(from, to, OperationToggle);
		}

		private static BitArray OperationSet(BitArray bits, int index)
		{
			bits.Set(index, true);
			return bits;
		}
		private static BitArray OperationClear(BitArray bits, int index)
		{
			bits.Set(index, false);
			return bits;
		}
		private static BitArray OperationToggle(BitArray bits, int index)
		{
			bits.Set(index, !bits.Get(index));
			return bits;
		}

		internal LightGrid Operation(Statement current)
		{
			switch (current.Command)
			{
				case Command.TurnOn:
					return this.TurnOn(current.From, current.Through);

				case Command.TurnOff:
					return this.TurnOff(current.From, current.Through);

				case Command.Toggle:
					return this.Toggle(current.From, current.Through);
			}
			throw new NotImplementedException();
		}
	}

	public class BrightnessGrid : Grid
	{

		private readonly ImmutableDictionary<int, int> brightnesses;

		public BrightnessGrid(int width, int height) :
				this(width, height, ImmutableDictionary<int, int>.Empty)
		{
		}

		internal BrightnessGrid(int width, int height, ImmutableDictionary<int, int> brightnesses) : base(width, height)
		{
			this.brightnesses = brightnesses;
		}

		public int TotalBrightness
		{
			get
			{
				return brightnesses
					.Select(kvp => kvp.Value)
					.Sum();
			}
		}

		internal BrightnessGrid TurnOn(Point from, Point to)
		{
			return GridOperation(from, to, OperationSet);
		}
		internal BrightnessGrid TurnOff(Point from, Point to)
		{
			return GridOperation(from, to, OperationClear);
		}
		internal BrightnessGrid Toggle(Point from, Point to)
		{
			return GridOperation(from, to, OperationToggle);
		}

		internal BrightnessGrid Operation(Statement current)
		{
			switch (current.Command)
			{
				case Command.TurnOn:
					return this.TurnOn(current.From, current.Through);

				case Command.TurnOff:
					return this.TurnOff(current.From, current.Through);

				case Command.Toggle:
					return this.Toggle(current.From, current.Through);
			}
			throw new NotImplementedException();
		}

		private BrightnessGrid GridOperation(Point from, Point to, Func<ImmutableDictionary<int, int>, int, ImmutableDictionary<int, int>> operation)
		{
			var indices = GetIndices(from, to);

			var newState = indices.Aggregate(
				seed: this.brightnesses,
				func: (prevState, index) => operation(prevState, index));

			return new BrightnessGrid(Width, Height, newState);
		}

		private static ImmutableDictionary<int, int> OperationSet(ImmutableDictionary<int, int> state, int index) =>
			state.TryGetValue(index, out var value)
			?
			state.SetItem(index, value + 1)
			:
			state.Add(index, 1);

		private static ImmutableDictionary<int, int> OperationClear(ImmutableDictionary<int, int> state, int index) =>
			state.TryGetValue(index, out var value)
			?
			value > 1 ? state.SetItem(index, value - 1) : state.Remove(index)
			:
			state;

		private static ImmutableDictionary<int, int> OperationToggle(ImmutableDictionary<int, int> state, int index) =>
			state.TryGetValue(index, out var value)
			?
			state.SetItem(index, value + 2)
			:
			state.Add(index, 2);
	}
}
