
using System;
using System.Collections;
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

        public static IEnumerable<T> Infinite<T>(this ICollection<T> sequence) {
            return new InfiniteGenerator<T>(sequence);
        }

        private class InfiniteGenerator<T> : IEnumerable<T>
        {
            private readonly ICollection<T> collection;

            public InfiniteGenerator(ICollection<T> collection)
            {
                this.collection=collection;
            }
            public IEnumerator<T> GetEnumerator()
            {
                return new InfiniteEnumerator<T>(this.collection);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class InfiniteEnumerator<T> : IEnumerator<T>
        {
            private int state = -1;
            private readonly ICollection<T> collection;

            public InfiniteEnumerator(ICollection<T> collection)
            {
                this.collection = collection;
            }

            public T Current => this.collection.ElementAtOrDefault(state);

            object IEnumerator.Current => this.Current;
            
            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                int count = this.collection.Count;
                this.state = (this.state + 1) % count; 
                return count != 0;
            }

            public void Reset()
            {
                this.state = -1;
            }
        }
    }
}