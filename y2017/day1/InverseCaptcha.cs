// http://adventofcode.com/2017/day/1

using System;
using System.Linq;
using System.Collections.Generic;

namespace advent.of.code.y2017.day1 {

    static class InverseCaptcha {
        
        public static int sumOfZippedSequences(IEnumerable<int> first, IEnumerable<int> second) {
            return first
                .Zip(second, (left,right) => new {left, right })
                .Select( tuple => tuple.left == tuple.right ? tuple.left : 0)
                .Aggregate( (accu,current) => accu + current);
        }

        private static int findSumOfDigits(string s, int shiftOffset) {
            var sequence = s.ToDigits();
            var shiftedSequence = sequence
                    .Skip(shiftOffset)
                    .Concat(sequence.Take(shiftOffset));
            return sumOfZippedSequences(
                sequence, shiftedSequence);
        }

        public static int findSumOfDigitsMatchNextDigit(string s)
        {
            return findSumOfDigits(s,1);
        }

        public static int findSumOfDigitsMatchHalfDigit(string s) 
        {
            return findSumOfDigits(s,s.Length / 2);
        }
    }
}