// http://adventofcode.com/2015/day/10

/*
The Cosmological Theorem
What lets us formally study the look-and-say sequence is a rather 
ominous-sounding result known as the cosmological theorem, which says 
that the eighth term and every term after it in the sequence is made 
up of one or more of 92 “basic” non-interacting subsequences. These 92 
basic subsequences are summarized in lexicographical order in the following 
table. The fourth column in the table says what other subsequence(s) the given subsequence evolves into. For example, the first subsequence, 1112, evolves into the 63rd subsequence: 3112. Similarly, the second subsequence, 1112133, evolves into the 64th subsequence followed by the 62nd subsequence: 31121123.
 */
 
using System;
using System.Linq;
using System.Numerics;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace advent.of.code.y2015.day10 {

    static class LookAndSay {

         private static ImmutableArray<(string seq, int length, int[] nextIdxs)> GetConwayTable() {

            return ImmutableArray<(string, int, int[])>
                .Empty
                .Add(("1112",4,new []{63}))
                .Add(("1112133",7,new []{64,62}))
                .Add(("111213322112",12,new []{65}))
                .Add(("111213322113",12,new []{66}))
                .Add(("1113",4,new []{68}))
                .Add(("11131",5,new []{69}))
                .Add(("111311222112",12,new []{84,55}))
                .Add(("111312",6,new []{70}))
                .Add(("11131221",8,new []{71}))
                .Add(("1113122112",10,new []{76}))
                .Add(("1113122113",10,new []{77}))
                .Add(("11131221131112",14,new []{82}))
                .Add(("111312211312",12,new []{78}))
                .Add(("11131221131211",14,new []{79}))
                .Add(("111312211312113211",18,new []{80}))
                .Add(("111312211312113221133211322112211213322112",42,new []{81,29,91}))
                .Add(("111312211312113221133211322112211213322113",42,new []{81,29,90}))
                .Add(("11131221131211322113322112",26,new []{81,30}))
                .Add(("11131221133112",14,new []{75,29,92}))
                .Add(("1113122113322113111221131221",28,new []{75,32}))
                .Add(("11131221222112",14,new []{72}))
                .Add(("111312212221121123222112",24,new []{73}))
                .Add(("111312212221121123222113",24,new []{74}))
                .Add(("11132",5,new []{83}))
                .Add(("1113222",7,new []{86}))
                .Add(("1113222112",10,new []{87}))
                .Add(("1113222113",10,new []{88}))
                .Add(("11133112",8,new []{89,92}))
                .Add(("12",2,new []{1}))
                .Add(("123222112",9,new []{3}))
                .Add(("123222113",9,new []{4}))
                .Add(("12322211331222113112211",23,new []{2,61,29,85}))
                .Add(("13",2,new []{5}))
                .Add(("131112",6,new []{28}))
                .Add(("13112221133211322112211213322112",32,new []{24,33,61,29,91}))
                .Add(("13112221133211322112211213322113",32,new []{24,33,61,29,90}))
                .Add(("13122112",8,new []{7}))
                .Add(("132",3,new []{8}))
                .Add(("13211",5,new []{9}))
                .Add(("132112",6,new []{10}))
                .Add(("1321122112",10,new []{21}))
                .Add(("132112211213322112",18,new []{22}))
                .Add(("132112211213322113",18,new []{23}))
                .Add(("132113",6,new []{11}))
                .Add(("1321131112",10,new []{19}))
                .Add(("13211312",8,new []{12}))
                .Add(("1321132",7,new []{13}))
                .Add(("13211321",8,new []{14}))
                .Add(("132113212221",12,new []{15}))
                .Add(("13211321222113222112",20,new []{18}))
                .Add(("1321132122211322212221121123222112",34,new []{16}))
                .Add(("1321132122211322212221121123222113",34,new []{17}))
                .Add(("13211322211312113211",20,new []{20}))
                .Add(("1321133112",10,new []{6,61,29,92}))
                .Add(("1322112",7,new []{26}))
                .Add(("1322113",7,new []{27}))
                .Add(("13221133112",11,new []{25,29,92}))
                .Add(("1322113312211",13,new []{25,29,67}))
                .Add(("132211331222113112211",21,new []{25,29,85}))
                .Add(("13221133122211332",17,new []{25,29,68,61,29,89}))
                .Add(("22",2,new []{61}))
                .Add(("3",1,new []{33}))
                .Add(("3112",4,new []{40}))
                .Add(("3112112",7,new []{41}))
                .Add(("31121123222112",14,new []{42}))
                .Add(("31121123222113",14,new []{43}))
                .Add(("3112221",7,new []{38,39}))
                .Add(("3113",4,new []{44}))
                .Add(("311311",6,new []{48}))
                .Add(("31131112",8,new []{54}))
                .Add(("3113112211",10,new []{49}))
                .Add(("3113112211322112",16,new []{50}))
                .Add(("3113112211322112211213322112",28,new []{51}))
                .Add(("3113112211322112211213322113",28,new []{52}))
                .Add(("311311222",9,new []{47,38}))
                .Add(("311311222112",12,new []{47,55}))
                .Add(("311311222113",12,new []{47,56}))
                .Add(("3113112221131112",16,new []{47,57}))
                .Add(("311311222113111221",18,new []{47,58}))
                .Add(("311311222113111221131221",24,new []{47,59}))
                .Add(("31131122211311122113222",23,new []{47,60}))
                .Add(("3113112221133112",16,new []{47,33,61,29,92}))
                .Add(("311312",6,new []{45}))
                .Add(("31132",5,new []{46}))
                .Add(("311322113212221",15,new []{53}))
                .Add(("311332",6,new []{38,29,89}))
                .Add(("3113322112",10,new []{38,30}))
                .Add(("3113322113",10,new []{38,31}))
                .Add(("312",3,new []{34}))
                .Add(("312211322212221121123222113",27,new []{36}))
                .Add(("312211322212221121123222112",27,new []{35}))
                .Add(("32112",5,new []{37}));
        }

        public static string Iterate(long number, int iteration) {
            return Iterate(new BigInteger(number), iteration).ToString();
        }
        public static BigInteger Iterate(BigInteger number, int iteration) {
            return Enumerable
                .Range(0, iteration)
                .Aggregate(number, (accu,_) => NextElement(accu));
        }

        private static BigInteger Shift(this BigInteger value, int count, char digit) 
            => value * 100 + count*10 +(digit-'0');

        private static BigInteger NextElement(this BigInteger value) 
        { 
            var s = value.ToString();
            
            return s.Skip(1).Aggregate( 
                seed: (x: new BigInteger(0), count: 1, previous: s.First()),
                func: (accu, current) => {
                    return (current != accu.previous) ?
                        (x:accu.x.Shift(accu.count,accu.previous), count: 1, previous:current)
                        :
                        (x: accu.x, count: accu.count+1, previous: current);
                },
                resultSelector: 
                    accu => accu.x.Shift(accu.count, accu.previous)
            );
        }
   
        public static int WhatLength(long number, int iteration) {

            var conway = GetConwayTable();
            return Enumerable.Range(0, iteration)
                .Aggregate<int,IEnumerable<int>, int>( 
                    seed: FindFirstIndices(conway, number.ToString()), 
                    func: (accu, _ ) => 
                        accu
                            .SelectMany( index => conway.ElementAt(index-1).nextIdxs),
                    resultSelector: (accu) =>
                        accu
                            .Select( index => conway.ElementAt(index-1).length)
                            .Sum()
                );
        }

        private static int FindIndex(ImmutableArray<(string seq, int, int[])> conway, string number) 
            => conway.FindIndex( x => x.seq == number);

        private static IEnumerable<int> FindFirstIndices(ImmutableArray<(string, int length, int[] nextIdxs)> conway, string no) {

            var idx = FindIndex(conway, no);
            if (idx != -1)
            {
                return new []{idx+1};
            }

            var left = no.Substring(0,no.Length/2);
            var right = no.Substring(no.Length/2);

            var leftIdx = FindIndex(conway, left);
            var rightIdx = FindIndex(conway, right);
            
            var nextIndices = new []{leftIdx+1,rightIdx+1};
            return nextIndices;
        }
    }
}