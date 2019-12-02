// http://adventofcode.com/2019/day/1

using System;
using System.Linq;

namespace advent.of.code.y2019.day1
{
    public static class RocketEquation {

		public static Func<int,int> GetFuel()
			=> from m in Div(3) select Sub(2)(m);

		public static StatefulComputation<int, int> GetFuelRecurse()
		=> mass => {
			var fuel = GetFuel()(mass);
			return ((fuel>0) ? fuel + GetFuelRecurse()(fuel).Value : 0, fuel);
		};

		public static Func<int,int> GetFuelIncluding()
		=> x => GetFuelRecurse().Run(x);

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