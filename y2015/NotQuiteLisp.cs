// http://adventofcode.com/2015/day/1

using System;
using System.Linq;
using System.Collections.Generic;

namespace advent.of.code.y2015.day1 {

    static class NotQuiteLisp {
        
        public static int WhatFloor(string instructions) =>
            instructions
                .Select(MovementSign)
                .Sum();

        public static int HowManyMovesToBasement(string instructions) =>
            instructions
                .Select(MovementSign)
                .Aggregate(
                    seed:(count:0, floor:0),
                    func: (accu,current) 
                        => accu.floor < 0 ? accu : (count:accu.count+1, floor: accu.floor+current),
                    resultSelector: accu => accu.count);
                    
        private static int MovementSign(char movement) => movement == '(' ? +1 : -1;
    }
}