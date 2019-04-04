// http://adventofcode.com/2015/day/10

using System;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;

namespace advent.of.code.y2015.day10 {

    static class LookAndSay {
        
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
    
        public static BigInteger Polynomial(int xx) {
            var coef = new int[]{-6, 3, -6, 12, -4, 7, -7, 1, 0, 5, -2, -4, -12, 2, 7, 12, -7, -10, -4, 3, 9, -7, 0, -8, 14, -3, 9, 2, -3, -10, -2, -6, 1, 10, -3, 1, 7, -7, 7, -12, -5, 8, 6, 10, -8, -8, -7, -3, 9, 1, 6, 6, -2, -3, -10, -2, 3, 5, 2, -1, -1, -1, -1, -1, 1, 2, 2, -1, -2, -1, 0, 1};
            
            var x = new BigInteger(xx);
            var result = coef
                .Select( (a, index) => new BigInteger(a) * BigInteger.Pow(x, index))
                .Aggregate(BigInteger.Zero, (accu,current) => accu + current);
            
            return result;
        }

        public static int WhatLength(long number, int iteration) {
            return 0;
        }
    }
}

/*
The Cosmological Theorem
What lets us formally study the look-and-say sequence is a rather 
ominous-sounding result known as the cosmological theorem, which says 
that the eighth term and every term after it in the sequence is made 
up of one or more of 92 “basic” non-interacting subsequences. These 92 
basic subsequences are summarized in lexicographical order in the following 
table. The fourth column in the table says what other subsequence(s) the given subsequence evolves into. For example, the first subsequence, 1112, evolves into the 63rd subsequence: 3112. Similarly, the second subsequence, 1112133, evolves into the 64th subsequence followed by the 62nd subsequence: 31121123.

#	Subsequence	Length	Evolves Into
1	1112	4	(63)
2	1112133	7	(64)(62)
3	111213322112	12	(65)
4	111213322113	12	(66)
5	1113	4	(68)
6	11131	5	(69)
7	111311222112	12	(84)(55)
8	111312	6	(70)
9	11131221	8	(71)
10	1113122112	10	(76)
11	1113122113	10	(77)
12	11131221131112	14	(82)
13	111312211312	12	(78)
14	11131221131211	14	(79)
15	111312211312113211	18	(80)
16	111312211312113221133211322112211213322112	42	(81)(29)(91)
17	111312211312113221133211322112211213322113	42	(81)(29)(90)
18	11131221131211322113322112	26	(81)(30)
19	11131221133112	14	(75)(29)(92)
20	1113122113322113111221131221	28	(75)(32)
21	11131221222112	14	(72)
22	111312212221121123222112	24	(73)
23	111312212221121123222113	24	(74)
24	11132	5	(83)
25	1113222	7	(86)
26	1113222112	10	(87)
27	1113222113	10	(88)
28	11133112	8	(89)(92)
29	12	2	(1)
30	123222112	9	(3)
31	123222113	9	(4)
32	12322211331222113112211	23	(2)(61)(29)(85)
33	13	2	(5)
34	131112	6	(28)
35	13112221133211322112211213322112	32	(24)(33)(61)(29)(91)
36	13112221133211322112211213322113	32	(24)(33)(61)(29)(90)
37	13122112	8	(7)
38	132	3	(8)
39	13211	5	(9)
40	132112	6	(10)
41	1321122112	10	(21)
42	132112211213322112	18	(22)
43	132112211213322113	18	(23)
44	132113	6	(11)
45	1321131112	10	(19)
46	13211312	8	(12)
47	1321132	7	(13)
48	13211321	8	(14)
49	132113212221	12	(15)
50	13211321222113222112	20	(18)
51	1321132122211322212221121123222112	34	(16)
52	1321132122211322212221121123222113	34	(17)
53	13211322211312113211	20	(20)
54	1321133112	10	(6)(61)(29)(92)
55	1322112	7	(26)
56	1322113	7	(27)
57	13221133112	11	(25)(29)(92)
58	1322113312211	13	(25)(29)(67)
59	132211331222113112211	21	(25)(29)(85)
60	13221133122211332	17	(25)(29)(68)(61)(29)(89)
61	22	2	(61)
62	3	1	(33)
63	3112	4	(40)
64	3112112	7	(41)
65	31121123222112	14	(42)
66	31121123222113	14	(43)
67	3112221	7	(38)(39)
68	3113	4	(44)
69	311311	6	(48)
70	31131112	8	(54)
71	3113112211	10	(49)
72	3113112211322112	16	(50)
73	3113112211322112211213322112	28	(51)
74	3113112211322112211213322113	28	(52)
75	311311222	9	(47)(38)
76	311311222112	12	(47)(55)
77	311311222113	12	(47)(56)
78	3113112221131112	16	(47)(57)
79	311311222113111221	18	(47)(58)
80	311311222113111221131221	24	(47)(59)
81	31131122211311122113222	23	(47)(60)
82	3113112221133112	16	(47)(33)(61)(29)(92)
83	311312	6	(45)
84	31132	5	(46)
85	311322113212221	15	(53)
86	311332	6	(38)(29)(89)
87	3113322112	10	(38)(30)
88	3113322113	10	(38)(31)
89	312	3	(34)
90	312211322212221121123222113	27	(36)
91	312211322212221121123222112	27	(35)
92	32112	5	(37)
The important thing about this particular basis of subsequences is that the evolution of any sequence made up of these subsequences is determined entirely by the evolution rule for the subsequences given in the final column of the above table. For example, the eighth term in the look-and-say sequence is 1113213211 = (24)(39). The subsequence (24) evolves into (83) and the subsequence (39) evolves into (9), so the ninth term in the look-and-say sequence is (83)(9), which is 31131211131221.
 */