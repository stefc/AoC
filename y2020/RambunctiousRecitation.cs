using System.Linq;
using System.Collections.Generic;
using Combinatorics.Collections;
using System.Collections;
using System;
using System.Text.RegularExpressions;
using System.Collections.Immutable;

// http://adventofcode.com/2020/day/15
namespace advent.of.code.y2020.day15
{
	static class RambunctiousRecitation
	{
		public static IEnumerable<int> Spoken(IEnumerable<int> input) {

			var memory = ImmutableDictionary<int,(int act, int prev)>.Empty;
			var turn = 1;

			var lastNumber = 0;
			foreach(var number in input) {
				yield return number;
				memory = memory.SetItem(number, (act:turn,prev:turn));
				lastNumber = number;
				turn++;
			}

			while (true) {
				var lastTurn = memory.GetValueOrDefault(lastNumber, (act:0,prev:0));
				var number = lastTurn.act-lastTurn.prev;
				yield return number;
				lastTurn = memory.GetValueOrDefault(number, (act:turn,prev:0));
				memory = memory.SetItem(number, (act:turn, prev:lastTurn.act));
				lastNumber = number;
				turn++;
			}
		}
    }
}
