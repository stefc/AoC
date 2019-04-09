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

namespace advent.of.code.y2015.day2 {

    static class WrappingPaper {
        
        public static int SquareFeetOfPaper(string dimension)  
        {
            var dim = ToDimension(dimension);

            var surfaces = ImmutableArray<int>.Empty
                .Add( dim.l*dim.w)
                .Add( dim.w*dim.h)
                .Add( dim.l*dim.h);

            return surfaces
                .Select( side => side * 2)
                .Sum() + surfaces.Min();
        }

        public static int FeetOfRibbon(string dimension) {
            var lengths = GetLengths(dimension);
            return (lengths.Sum() - lengths.Max()) * 2 + 
                lengths.Aggregate(1, (accu,current) => accu * current);
        }

        private static IEnumerable<int> GetLengths(string dimension) {
            return dimension
                    .Split('x')
                    .Select( x => Convert.ToInt32(x));
        }

        public static (int l, int w, int h) ToDimension(string dimension) {
            var lengths = GetLengths(dimension);
            return (
                l:lengths.ElementAt(0), 
                w:lengths.ElementAt(1), 
                h:lengths.ElementAt(2));
        }
    }
}