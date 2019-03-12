
using System;
using System.Collections.Generic;
using System.Linq;

namespace advent.of.code {

    public static class EnumerableExtension {

        public static T Head<T>(this IEnumerable<T> sequence) {
            return sequence.FirstOrDefault();
        } 

        public static IEnumerable<T> Tail<T>(this IEnumerable<T> sequence) {
            return sequence.Skip(1);
        }

        public static bool IsEmpty<T>(this IEnumerable<T> sequence) =>
            sequence.Count() == 0;
        public static bool IsNotEmpty<T>(this IEnumerable<T> sequence) =>
            sequence.Count() != 0;
    }
}