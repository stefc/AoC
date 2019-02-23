// http://adventofcode.com/2017/day/2

using System;
using System.Linq;
using System.Collections.Generic;

namespace advent.of.code.y2017.day2 {


    static class CorruptionChecksum {
        
        public static int[][] getSpreadsheet(this string input) 
        {
            return input.Split("/n")
                .Select( line => line.Split(' ')
                    .Select( cell => Convert.ToInt32(cell)).ToArray()
                ).ToArray();
        }
        
    }
}