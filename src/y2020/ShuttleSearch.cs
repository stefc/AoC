using System.Linq;
using System.Collections.Generic;
using System;
using advent.of.code.common;

namespace advent.of.code.y2020.day13
{
	// http://adventofcode.com/2020/day/13

	public readonly struct Instruction
	{
		public char Code { get; init; }
		public int Length { get; init; }
		private Instruction(char code, int length) => (Code, Length) = (code, length);
		private Instruction(string stmt) => (Code, Length) = FromString(stmt);

		public override string ToString() => $"{Code}{Length}";

		private static (char, int) FromString(string stmt)
		{
			var ch = char.ToUpper(stmt.FirstOrDefault());
			if (int.TryParse(new String(stmt.Skip(1).ToArray()), out var len))
			{
				return (char.ToUpper(ch), len);
			};
			return (ch, 0);
		}

		public static Instruction Create(string stmt) => new Instruction(stmt);


	}


	static class ShuttleSearch
	{
		public static int EarliestBusMultiplyWaitingTime(int time, string ids)
		{

			var busIds = ids
				.Split(',')
				.Where(id => id != "x")
				.Select(id => Convert.ToInt32(id))
				.ToList();

			var nextDepartue = busIds
				.Select(id => (id: id, depart: time - (time % id)))
				.Select(x => (id: x.id, depart: time == x.depart ? time : x.depart + x.id));

			var earliest = nextDepartue.Min(x => x.depart);
			var nextBus = nextDepartue.Single(x => x.depart == earliest);

			return nextBus.id * (earliest - time);
		}

		public static string WolframAlpha(string ids)
		{
			var terms = String.Join(',', ids.Split(',')
				.Select((id, t) => (t: t, id: id == "x" ? 0L : Convert.ToInt64(id)))
				.Where(x => x.id > 0)
				.Select(x => $"(t+{x.t}) mod {x.id} = 0"));
			return terms;
		}
	}
}
