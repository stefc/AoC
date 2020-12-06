// http://adventofcode.com/2015/day/4

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Security.Cryptography;
using System.Text;

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