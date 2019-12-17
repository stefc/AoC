// http://adventofcode.com/2019/day/7

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;

using advent.of.code;
using advent.of.code.common.Bst;

namespace advent.of.code.y2019.day7
{


    using PermutationAccu = ValueTuple<ImmutableArray<int>,ImmutableArray<bool>>;

    // https://en.wikipedia.org/wiki/Steinhaus–Johnson–Trotter_algorithm

    public static class AmplificationCircuit
    {
        private const bool LEFT_TO_RIGHT = true;
        private const bool RIGHT_TO_LEFT = false;

        public static int SearchArr(IEnumerable<int> a, int mobile)
        => a.FindIndex(x => x.Equals(mobile)) + 1;

        public static int Factorial(int number)
        => (number == 1) ? 1 : number * Factorial(number - 1);

        public static int GetMobile(IReadOnlyList<int> a,
            IReadOnlyList<bool> dir)
        {
            int n = a.Count;
            int mobilePrev = 0;
            int mobile = 0;

            foreach (var i in Enumerable.Range(0, n))
            {
                // direction 0 represents 
                // RIGHT TO LEFT. 
                if ((i != 0) && (dir[a[i]] == RIGHT_TO_LEFT))
                {
                    if ((a[i] > a[i - 1]) && (a[i] > mobilePrev))
                    {
                        mobile = a[i];
                        mobilePrev = mobile;
                    }
                }

                // direction 1 represents 
                // LEFT TO RIGHT. 
                if ((i != n - 1) && (dir[a[i]] == LEFT_TO_RIGHT))
                {
                    if ((a[i] > a[i + 1]) && (a[i] > mobilePrev))
                    {
                        mobile = a[i];
                        mobilePrev = mobile;
                    }
                }
            }
            return ((mobile == 0) && (mobilePrev == 0)) ? 0 : mobile;
        }

        public static PermutationAccu 
            GetPermutation(ImmutableArray<int> a, ImmutableArray<bool> dir)
        {
            var n = a.Length;

            int mobile = GetMobile(a, dir);
            int pos = SearchArr(a, mobile);

            // swapping the elements 
            // according to the 
            // direction i.e. dir[]. 
            if (dir[a[pos - 1]] == RIGHT_TO_LEFT)
            {
                a = a.Swap(pos - 1, pos - 2);
            }
            else if (dir[a[pos - 1]] == LEFT_TO_RIGHT)
            {
                a = a.Swap(pos, pos-1);
            }

            // changing the directions 
            // for elements greater  
            // than largest mobile integer. 
            foreach (var i in Enumerable.Range(0, n))
            {
                if (a[i] > mobile)
                {
                    if (dir[a[i]] == LEFT_TO_RIGHT)
                        dir = dir.SetItem(a[i], RIGHT_TO_LEFT);

                    else if (dir[a[i]] == RIGHT_TO_LEFT)
                        dir = dir.SetItem(a[i], LEFT_TO_RIGHT);
                }
            }

            return (a,dir);
        }
        
        public static ImmutableArray<T> Swap<T>(this ImmutableArray<T> a, 
            int index1, int index2)
        => a.SetItem(index1, a[index2]).SetItem(index2, a[index1]);

        public static int ToInt(this IEnumerable<int> array)
		=> array.Aggregate( 0, (accu, current) => accu * 10 + current);

        public static IEnumerable<int> Permutate(int n)
        {
            var accu = (
                a:Enumerable.Range(0, n).ToImmutableArray(),
                dir:Enumerable.Repeat(false, n).ToImmutableArray());
            
            yield return accu.a.ToInt();
            foreach(var _ in Enumerable.Range(0, Factorial(n)-1)) {
                accu = AmplificationCircuit.GetPermutation(accu.a, accu.dir);
                yield return accu.a.ToInt();
            }
        }
    }
}