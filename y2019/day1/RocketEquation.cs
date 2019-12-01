// http://adventofcode.com/2019/day/1

using System;
using System.Linq;

namespace advent.of.code.y2019.day1
{
    public static class RocketEquation {

		public static Func<int,int> GetFuel() => Div(3).Select( Sub(2) );

		public static Func<int,int> GetFuelIncluding() {
			var g = GetFuel();
			Func<int,int> recurse = null;
			recurse = (int x) => {
				var y = g(x);
				return (y>0) ? y + recurse(y) : 0;
			};
			return recurse;
		}

		public static Func<int,int> Div(int y) => x => x / y;
		public static Func<int,int> Sub(int y) => x => x - y;

		public static int CalcTotal(string moduleMasses, bool correct) {

            return moduleMasses
                .ToNumbers()
				.Select( correct ? GetFuelIncluding() : GetFuel() )
                .Sum();
        }


	}
}