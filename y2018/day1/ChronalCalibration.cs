// http://adventofcode.com/2018/day/1

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace advent.of.code.y2018.day1 {

    static class ChronalCalibration {
        public static int ChangeFrequency(string display) {

            return display
                .ToNumbers()
                .Sum();
        }

        internal static int DetectTwice(string display)
        {
            return display
                .ToNumbers()
                .Endless()
                .FindTwice();
        }


        private static int FindTwice(ImmutableHashSet<int> set, int accu, int head, 
            IEnumerable<int> tail) 
            => set.Contains(accu) ?  accu :
                FindTwice(set.Add(accu), accu + head, tail.Head(), tail.Tail());

        internal static int FindTwice(this IEnumerable<int> sequence)
        {
            return FindTwice(
                ImmutableHashSet<int>.Empty, 0,
                sequence.Head(), sequence.Tail());
        }
    }
}