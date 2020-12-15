using System.Linq;
using System.Collections.Generic;
using Combinatorics.Collections;
using System.Collections;
using System;
using System.Text.RegularExpressions;
using System.Collections.Immutable;

namespace advent.of.code.y2020.day14
{
	 using static F;




	 using Program = IEnumerable<Either<Mask, Store>>;

	 using Memory = ImmutableDictionary<ulong,ulong>;

	// http://adventofcode.com/2020/day/14

	public readonly struct Store
	{
		public ulong Adr { get; init; }

		public ulong Value { get; init; }

		private Store(ulong adr, ulong value) => (Adr, Value) = (adr, value);
		private Store(string stmt) => (Adr, Value) = FromString(stmt);

		const string pattern = @"mem\[(?'adr'\d+)\] = (?'value'\d+)";

		private static (ulong adr, ulong value) FromString(string stmt)
		{
			RegexOptions options = RegexOptions.IgnoreCase;
			Match match = Regex.Matches(stmt, pattern, options).First();

			var adr = match.Groups["adr"].Value;
			var value = match.Groups["value"].Value;

			return (adr: Convert.ToUInt64(adr), value: Convert.ToUInt64(value));
		}

		public static Store Create(string stmt) => new Store(stmt);
	}

	public readonly struct Mask
	{
		public BitArray Or { get; init; }
		public BitArray And { get; init; }
		public BitArray Floating { get; init; }
		private Mask(BitArray or, BitArray and, BitArray floating) => (Or, And, Floating) = (or, and, floating);
		private Mask(string stmt) => (Or, And, Floating) = FromString(stmt);

		private static (BitArray or, BitArray and, BitArray floating) FromString(string stmt)
		=> (or: DockingData.OrMaskFromString(stmt),
			and: DockingData.AndMaskFromString(stmt),
			floating: DockingData.FloatingMaskFromString(stmt));
		public static Mask Create(string stmt) => new Mask(stmt);

		public static Mask Empty => new Mask(new BitArray(36), new BitArray(36), new BitArray(36));
	}

	static class DockingData
	{
		public static BitArray OrMaskFromString(string value) =>
			new BitArray(value.Select( ch => ch == '1').Reverse().ToArray());

		public static BitArray AndMaskFromString(string value) =>
			new BitArray(value.Select( ch => ch == '0').Reverse().ToArray());

		public static BitArray FloatingMaskFromString(string value) =>
			new BitArray(value.Select( ch => ch == 'X').Reverse().ToArray());

		public static IEnumerable<bool> AsEnumerable(this BitArray bits) {
			foreach (var bit in bits) {
				yield return (bool)bit;
			}
		}

		public static IEnumerable<ulong> Adresses(this BitArray floatMask)
		{
			var floats = floatMask.AsEnumerable()
				.Zip(36.PowersOf2(), (bit,ary) => bit ? ary: 0)
				.Where( x => x > 0)
				.ToArray();


			var adrs = floats
				.Aggregate( ImmutableList<ulong>.Empty,
					(acc,cur) => acc.IsEmpty
					?
					acc.Add(0).Add(cur)
					:
					acc.Aggregate( acc, (a,c) => a.Add(c | cur)))
				.ToArray();

			return adrs;
		}

		public static ulong ToUInt64(this BitArray ba)
		{
			var len = Math.Min(36, ba.Count);
			ulong n = 0;
			for (int i = 0; i < len; i++) {
				if (ba.Get(i))
					n |= 1UL << i;
			}
			return n;
		}

		public static Program ToProgram(this string input)
		=> input.Trim().Split('\n').ToArray().ToProgram();

		public static Program ToProgram(this IEnumerable<string> input)
		=> input.Select( line => {
			return ToEither( line.StartsWith("mask = "),
                    () => Mask.Create(line.Substring("mask = ".Length)),
                    () => Store.Create(line) );
			});
		private static Either<Mask, Store> ToEither(bool isMask,
        	    Func<Mask> getLeft, Func<Store> getRight)
        	=> isMask ? (Either<Mask,Store>) Left(getLeft()) : Right(getRight());

		public static ulong Evaluate(Program program)
		=> program
			.Aggregate( ProgramState.Empty,
				(acc,cur) => cur.Match( mask => acc.WithMask(mask), store => acc.WithStore(store, acc.DecodeV1)),
				acc => acc.Memory.Values.Aggregate(0ul, (a,c) => a+c ));

		public static ulong EvaluateV2(Program program)
		=> program
			.Aggregate( ProgramState.Empty,
				(acc,cur) => cur.Match( mask => acc.WithMask(mask), store => acc.WithStore(store, acc.DecodeV2)),
				acc => acc.Memory.Values.Aggregate(0ul, (a,c) => a+c ));


	}

	public readonly struct ProgramState
    {
        public readonly Mask Mask { get; init; }

		public readonly Memory Memory { get; init; }

        private ProgramState(Mask mask, Memory memory)
        {
            this.Mask = mask;
            this.Memory = memory;
        }

		public static ProgramState Empty => new ProgramState(Mask.Empty, Memory.Empty);

		public ProgramState WithMask(Mask mask)
            => new ProgramState(mask, this.Memory);

		public ProgramState WithStore(Store store, Func<Memory,Store,Memory> decode) {
			var newMemory = decode(this.Memory, store);

			return new ProgramState(this.Mask, newMemory);
		}

		public Memory DecodeV1(Memory memory, Store store) {
			var orMask = this.Mask.Or.ToUInt64();
			var andMask = this.Mask.And.ToUInt64() ^ 0x0FFFFFFFFF;

			var value = (store.Value | orMask) & andMask;

			return value == 0 ? memory.Remove(store.Adr) : memory.SetItem(store.Adr, value);

		}

		public Memory DecodeV2(Memory memory, Store store) {
			var overwriteMask = this.Mask.Or.ToUInt64();
			var floatMask = this.Mask.Floating;

			var andMask = floatMask.ToUInt64() ^ 0x0FFFFFFFFF;
			var adr = (store.Adr | overwriteMask) & andMask;

			var adrs = floatMask.Adresses().Select( adrMask =>
				adr | adrMask ).ToArray();

			var value = store.Value;

			if (value == 0)
			{
				return memory.RemoveRange( adrs );
			}
			else
			{
				return memory.SetItems( adrs.Select( x => KeyValuePair.Create(x, value)));
			}
		}
    }
}
