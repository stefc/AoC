// http://adventofcode.com/2015/day/10

using System;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;

namespace advent.of.code.y2015.day10 {

    static class LookAndSay {
        
        public static string Iterate(long number, int iteration) {
            var x = new BigInteger(number);
            var s = x.ToString();

            char previous = s.First();
            int count = 1;

            x = new BigInteger();
            IEnumerator<char> enumerator = s.Skip(1).GetEnumerator();
            
            while (enumerator.MoveNext()) {
                char current = enumerator.Current;
                if (current != previous)
                {
                    x = x * 100 + ((count*10)+ (previous - '0'));

                }
            };
            System.Console.WriteLine(x.ToString());
 
            return x.ToString();
        }
        public static int WhatLength(long number, int iteration) {
            return 0;
        }
    }
}