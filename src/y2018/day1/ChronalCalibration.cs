// http://adventofcode.com/2018/day/1

using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace advent.of.code.y2018.day1
{

    static class ChronalCalibration {
        public static int ChangeFrequency(string display) {

            return display
                .ToNumbers()
                .Sum();
        }

        internal static int DetectTwice(string display)
        {
            var numbers = display
                .ToNumbers()
                .ToImmutableArray();

            return numbers
                .Infinite()
                .FindTwice();
        }

        internal static int FindTwice(this IEnumerable<int> enumerable)
        {
            var set = ImmutableHashSet<int>.Empty;
            var accu = 0;
            var enumerator = enumerable.GetEnumerator();
            while (!set.Contains(accu) && enumerator.MoveNext()) {
                set = set.Add(accu);
                var head = enumerator.Current;
                accu += head;
            }
            return accu;
        }
    }
}