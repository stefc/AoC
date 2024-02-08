// http://adventofcode.com/2019/day/4

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;

using advent.of.code;
using advent.of.code.common;

namespace advent.of.code.y2019.day4
{
    using static F;


    public static class ExtensionMethods
    {

    }

    public static class SecureContainer
    {
        public static bool HasTwoAdjacentDigits(int value)
        => value
            .ToDigits()
            .SelectWithPrevious((prev, cur) => prev == cur)
            .Any(x => x);

        public static bool NeverDecrease(int value)
        => value
            .ToDigits()
            .SelectWithPrevious((prev, cur) => prev <= cur)
            .All(x => x);

        public static IEnumerable<int> DigitCounting(int value)
        => value.ToDigits().Zip(new []{false}.Concat(value.ToDigits()
            .SelectWithPrevious(
                (prev, cur) => prev == cur)), (a,b) => b)
            .Aggregate( ImmutableStack<int>.Empty, 
                (accu,current) => 
                    current ? accu.Pop().Push(accu.Peek()+1): accu.Push(1))
            .ToImmutableHashSet();

        public static bool NotPartOfGroup(int value)
        => DigitCounting(value).Contains(2);

        public static bool IsOdd(this int value) => value % 2 != 0;
    }
}