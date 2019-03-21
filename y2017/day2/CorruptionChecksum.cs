// http://adventofcode.com/2017/day/2

using System;
using System.Linq;
using System.Collections.Generic;


using Division = System.ValueTuple<int,int>;

namespace advent.of.code.y2017.day2 {

    static class CorruptionChecksum {
        
        public static int[][] getSpreadsheet(this string input) 
            => input.Split("\r\n")
                .Where( line => !String.IsNullOrEmpty(line))
                .Select( line => line.ToNumbers().ToArray())
                .ToArray();

        public static Division getDivision(this string line) {
            IEnumerable<int> numbers = line.ToNumbers().OrderByDescending(x => x);
            return getDivisionNumbers(numbers);
        }

        private static Division getDivisionNumbers(IEnumerable<int> numbers) {
            
            var numerator = 0;
            var  denominator = 0;

            while (denominator == 0 && numbers.IsNotEmpty()) {
                numerator = numbers.Head();
                numbers = numbers.Tail();
                denominator = numbers.SingleOrDefault( x => (numerator % x) == 0);
            }

            return (numerator, denominator);
        }

        public static int getMinMaxAggregate(string input) {
            var matrix = getSpreadsheet(input);

            var result = matrix
                .Select( row => row.Max() - row.Min())
                .Sum();
            return result;
        }

// https://www.tabsoverspaces.com/233633-head-and-tail-like-methods-on-list-in-csharp-and-fsharp-and-python-and-haskell

// https://gist.github.com/d11wtq/5234515

        public static bool Deconstruct<T>(this IEnumerable<T> seq, out (T head, IEnumerable<T> tail) list)
        {
            list = (head: seq.FirstOrDefault(), tail: seq.Skip(1));
            return (seq.Count() != 0);
        }

        public static Nullable<(T head, IEnumerable<T> tail)> Split<T>(this IEnumerable<T> seq)
        {
            if (seq.Count()==0)
                return null;
            return (head: seq.FirstOrDefault(), tail: seq.Skip(1));
        }
        public static int getDivisionAggregate(string input) {
            var matrix = getSpreadsheet(input);

            var result = matrix
                .Select( row => getDivisionNumbers(row.OrderByDescending( x => x)))
                ;

            return result            
                .Select( division => division.Item1 / division.Item2 )
                .Sum();
        }
    }
}