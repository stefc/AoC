/* Fortunately, every present is a box (a perfect right rectangular prism), which makes calculating the required wrapping paper for each gift a little easier: find the surface area of the box, which is 2*l*w + 2*w*h + 2*h*l. The elves also need a little extra paper for each present: the area of the smallest side.

For example:

A present with dimensions 2x3x4 requires 2*6 + 2*12 + 2*8 = 52 square feet of wrapping paper plus 6 square feet of slack, for a total of 58 square feet.
A present with dimensions 1x1x10 requires 2*1 + 2*10 + 2*10 = 42 square feet of wrapping paper plus 1 square foot of slack, for a total of 43 square feet.
All numbers in the elves' list are in feet. How many total square feet of wrapping paper should they order?

*/

// http://adventofcode.com/2015/day/2

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace advent.of.code.y2015.day4 {

    static class StockingSuffer {
        
        public static int FindLowestNumber(string secret, int prefix = 5) {
            var md5 = new MD5CryptoServiceProvider();
            return Enumerable
                .Range(0,10000000)
                .First( number => HasPrefix(
                    new String('0',prefix),
                    md5.ComputeHash(
                        Encoding.ASCII.GetBytes($"{secret}{number}"))));
        }

        private static bool HasPrefix(string prefix, byte[] hash) {
            var result = hash.Aggregate(
                seed: new StringBuilder(),
                func: (accu,current) => accu.Append(current.ToString("X2")),
                resultSelector: accu => accu.ToString());

            return result.StartsWith(prefix);
        } 
    }
}