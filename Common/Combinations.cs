using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

/// https://github.com/StephenCleary/Combinatorics

namespace advent.of.code.common
{

	/// <summary>
    /// Indicates whether a permutation, combination or variation generates equivalent result sets.  
    /// </summary>
    public enum GenerateOption
    {
        /// <summary>
        /// Do not generate equivalent result sets.
        /// </summary>
        WithoutRepetition,

        /// <summary>
        /// Generate equivalent result sets.
        /// </summary>
        WithRepetition,
    }

    /// <summary>
    /// Flags controlling set generation behavior.
    /// </summary>
    [Flags]
    public enum GenerateOptions
    {
        /// <summary>
        /// Do not generate equivalent result sets.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Generate equivalent result sets.
        /// </summary>
        WithRepetition = 0x1,

        /// <summary>
        /// Allow each result set to be mutated in-place and returned as the next result set, rather than copying each set.
        /// </summary>
        AllowMutation = 0x2,
    }

    /// <summary>
    /// Combinations defines a sequence of all possible subsets of a particular size from the set of values.
    /// Within the returned set, there is no prescribed order.
    /// This follows the mathematical concept of choose.
    /// For example, put <c>10</c> dominoes in a hat and pick <c>5</c>.
    /// The number of possible combinations is defined as "10 choose 5", which is calculated as <c>(10!) / ((10 - 5)! * 5!)</c>.
    /// </summary>
    /// <remarks>
    /// The MetaCollectionType parameter of the constructor allows for the creation of
    /// two types of sets,  those with and without repetition in the output set when 
    /// presented with repetition in the input set.
    /// 
    /// When given a input collect {A B C} and lower index of 2, the following sets are generated:
    /// MetaCollectionType.WithRepetition =>
    /// {A A}, {A B}, {A C}, {B B}, {B C}, {C C}
    /// MetaCollectionType.WithoutRepetition =>
    /// {A B}, {A C}, {B C}
    /// 
    /// Input sets with multiple equal values will generate redundant combinations in proportion
    /// to the likelihood of outcome.  For example, {A A B B} and a lower index of 3 will generate:
    /// {A A B} {A A B} {A B B} {A B B}
    /// </remarks>
    /// <typeparam name="T">The type of the values within the list.</typeparam>
    public sealed class Combinations<T> : IEnumerable<IReadOnlyList<T>>
    {
        /// <summary>
        /// Create a combination set from the provided list of values.
        /// The upper index is calculated as values.Count, the lower index is specified.
        /// Collection type defaults to MetaCollectionType.WithoutRepetition
        /// </summary>
        /// <param name="values">List of values to select combinations from.</param>
        /// <param name="lowerIndex">The size of each combination set to return.</param>
        public Combinations(IEnumerable<T> values, int lowerIndex)
            : this(values, lowerIndex, GenerateOption.WithoutRepetition)
        {
        }

        /// <summary>
        /// Create a combination set from the provided list of values.
        /// The upper index is calculated as values.Count, the lower index is specified.
        /// </summary>
        /// <param name="values">List of values to select combinations from.</param>
        /// <param name="lowerIndex">The size of each combination set to return.</param>
        /// <param name="type">The type of Combinations set to generate.</param>
        public Combinations(IEnumerable<T> values, int lowerIndex, GenerateOption type)
        {
            _ = values ?? throw new ArgumentNullException(nameof(values));

            // Copy the array and parameters and then create a map of booleans that will 
            // be used by a permutations object to reference the subset.  This map is slightly
            // different based on whether the type is with or without repetition.
            // 
            // When the type is WithoutRepetition, then a map of upper index elements is
            // created with lower index false's.  
            // E.g. 8 choose 3 generates:
            // Map: {1 1 1 1 1 0 0 0}
            // Note: For sorting reasons, false denotes inclusion in output.
            // 
            // When the type is WithRepetition, then a map of upper index - 1 + lower index
            // elements is created with the falses indicating that the 'current' element should
            // be included and the trues meaning to advance the 'current' element by one.
            // E.g. 8 choose 3 generates:
            // Map: {1 1 1 1 1 1 1 1 0 0 0} (7 trues, 3 falses).

            Type = type;
            LowerIndex = lowerIndex;
            _myValues = values.ToList();
            List<bool> myMap;
            if (type == GenerateOption.WithoutRepetition)
            {
                myMap = new List<bool>(_myValues.Count);
                myMap.AddRange(_myValues.Select((t, i) => i < _myValues.Count - LowerIndex));
            }
            else
            {
                myMap = new List<bool>(_myValues.Count + LowerIndex - 1);
                for (var i = 0; i < _myValues.Count - 1; ++i)
                    myMap.Add(true);
                for (var i = 0; i < LowerIndex; ++i)
                    myMap.Add(false);
            }

            _myPermutations = new Permutations<bool>(myMap);
        }

        /// <summary>
        /// Gets an enumerator for collecting the list of combinations.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<IReadOnlyList<T>> GetEnumerator() => new Enumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// The enumerator that enumerates each meta-collection of the enclosing Combinations class.
        /// </summary>
        public sealed class Enumerator : IEnumerator<IReadOnlyList<T>>
        {
            /// <summary>
            /// Construct a enumerator with the parent object.
            /// </summary>
            /// <param name="source">The source combinations object.</param>
            public Enumerator(Combinations<T> source)
            {
                _myParent = source;
                _myPermutationsEnumerator = _myParent._myPermutations.GetEnumerator();
            }

            void IEnumerator.Reset() => throw new NotSupportedException();

            /// <summary>
            /// Advances to the next combination of items from the set.
            /// </summary>
            /// <returns>True if successfully moved to next combination, False if no more unique combinations exist.</returns>
            /// <remarks>
            /// The heavy lifting is done by the permutations object, the combination is generated
            /// by creating a new list of those items that have a true in the permutation parallel array.
            /// </remarks>
            public bool MoveNext()
            {
                var ret = _myPermutationsEnumerator.MoveNext();
                _myCurrentList = null;
                return ret;
            }

            /// <summary>
            /// The current combination
            /// </summary>
            public IReadOnlyList<T> Current
            {
                get
                {
                    ComputeCurrent();
                    return _myCurrentList!;
                }
            }

            object IEnumerator.Current => Current;

            /// <inheritdoc />
            public void Dispose() => _myPermutationsEnumerator.Dispose();

            /// <summary>
            /// The only complex function of this entire wrapper, ComputeCurrent() creates
            /// a list of original values from the bool permutation provided.  
            /// The exception for accessing current (InvalidOperationException) is generated
            /// by the call to .Current on the underlying enumeration.
            /// </summary>
            /// <remarks>
            /// To compute the current list of values, the underlying permutation object
            /// which moves with this enumerator, is scanned differently based on the type.
            /// The items have only two values, true and false, which have different meanings:
            /// 
            /// For type WithoutRepetition, the output is a straightforward subset of the input array.  
            /// E.g. 6 choose 3 without repetition
            /// Input array:   {A B C D E F}
            /// Permutations:  {0 1 0 0 1 1}
            /// Generates set: {A   C D    }
            /// Note: size of permutation is equal to upper index.
            /// 
            /// For type WithRepetition, the output is defined by runs of characters and when to 
            /// move to the next element.
            /// E.g. 6 choose 5 with repetition
            /// Input array:   {A B C D E F}
            /// Permutations:  {0 1 0 0 1 1 0 0 1 1}
            /// Generates set: {A   B B     D D    }
            /// Note: size of permutation is equal to upper index - 1 + lower index.
            /// </remarks>
            private void ComputeCurrent()
            {
                if (_myCurrentList != null)
                    return;

                _myCurrentList = new List<T>(_myParent.LowerIndex);
                var index = 0;
                var currentPermutation = _myPermutationsEnumerator.Current;
                foreach (var p in currentPermutation)
                {
                    if (!p)
                    {
                        _myCurrentList.Add(_myParent._myValues[index]);
                        if (_myParent.Type == GenerateOption.WithoutRepetition)
                            ++index;
                    }
                    else
                    {
                        ++index;
                    }
                }
            }

            /// <summary>
            /// Parent object this is an enumerator for.
            /// </summary>
            private readonly Combinations<T> _myParent;

            /// <summary>
            /// The current list of values, this is lazy evaluated by the Current property.
            /// </summary>
            private List<T>? _myCurrentList;

            /// <summary>
            /// An enumerator of the parents list of lexicographic orderings.
            /// </summary>
            private readonly IEnumerator<IReadOnlyList<bool>> _myPermutationsEnumerator;
        }

        /// <summary>
        /// The number of unique combinations that are defined in this meta-collection.
        /// This value is mathematically defined as Choose(M, N) where M is the set size
        /// and N is the subset size.  This is M! / (N! * (M-N)!).
        /// </summary>
        public BigInteger Count => _myPermutations.Count;

        /// <summary>
        /// The type of Combinations set that is generated.
        /// </summary>
        public GenerateOption Type { get; }

        /// <summary>
        /// The upper index of the meta-collection, equal to the number of items in the initial set.
        /// </summary>
        public int UpperIndex => _myValues.Count;

        /// <summary>
        /// The lower index of the meta-collection, equal to the number of items returned each iteration.
        /// </summary>
        public int LowerIndex { get; }

        /// <summary>
        /// Copy of values object is initialized with, required for enumerator reset.
        /// </summary>
        private readonly List<T> _myValues;

        /// <summary>
        /// Permutations object that handles permutations on booleans for combination inclusion.
        /// </summary>
        private readonly Permutations<bool> _myPermutations;
    }

	/// <summary>
    /// Permutations defines a sequence of all possible orderings of a set of values.
    /// </summary>
    /// <remarks>
    /// When given a input collect {A A B}, the following sets are generated:
    /// MetaCollectionType.WithRepetition =>
    /// {A A B}, {A B A}, {A A B}, {A B A}, {B A A}, {B A A}
    /// MetaCollectionType.WithoutRepetition =>
    /// {A A B}, {A B A}, {B A A}
    /// 
    /// When generating non-repetition sets, ordering is based on the lexicographic 
    /// ordering of the lists based on the provided Comparer.  
    /// If no comparer is provided, then T must be IComparable on T.
    /// 
    /// When generating repetition sets, no comparisons are performed and therefore
    /// no comparer is required and T does not need to be IComparable.
    /// </remarks>
    /// <typeparam name="T">The type of the values within the list.</typeparam>
    public sealed class Permutations<T> : IEnumerable<IReadOnlyList<T>>
    {
        /// <summary>
        /// Create a permutation set from the provided list of values.  
        /// The values (T) must implement IComparable.  
        /// If T does not implement IComparable use a constructor with an explicit IComparer.
        /// The repetition type defaults to MetaCollectionType.WithholdRepetitionSets
        /// </summary>
        /// <param name="values">List of values to permute.</param>
        public Permutations(IEnumerable<T> values)
            : this(values, GenerateOptions.None, null)
        {
        }

        /// <summary>
        /// Create a permutation set from the provided list of values.  
        /// If type is MetaCollectionType.WithholdRepetitionSets, then values (T) must implement IComparable.  
        /// If T does not implement IComparable use a constructor with an explicit IComparer.
        /// </summary>
        /// <param name="values">List of values to permute.</param>
        /// <param name="type">The type of permutation set to calculate.</param>
        public Permutations(IEnumerable<T> values, GenerateOption type)
            : this(values, type, null)
        {
        }

        /// <summary>
        /// Create a permutation set from the provided list of values.  
        /// The values will be compared using the supplied IComparer.
        /// The repetition type defaults to MetaCollectionType.WithholdRepetitionSets
        /// </summary>
        /// <param name="values">List of values to permute.</param>
        /// <param name="comparer">Comparer used for defining the lexicographic order.</param>
        public Permutations(IEnumerable<T> values, IComparer<T>? comparer)
            : this(values, GenerateOptions.None, comparer)
        {
        }

        /// <summary>
        /// Create a permutation set from the provided list of values.  
        /// If type is MetaCollectionType.WithholdRepetitionSets, then the values will be compared using the supplied IComparer.
        /// </summary>
        /// <param name="values">List of values to permute.</param>
        /// <param name="type">The type of permutation set to calculate.</param>
        /// <param name="comparer">Comparer used for defining the lexicographic order.</param>
        public Permutations(IEnumerable<T> values, GenerateOption type, IComparer<T>? comparer)
            : this(values, type == GenerateOption.WithRepetition ? GenerateOptions.WithRepetition : GenerateOptions.None, comparer)
        {

        }

        /// <summary>
        /// Create a permutation set from the provided list of values.  
        /// If type is MetaCollectionType.WithholdRepetitionSets, then the values will be compared using the supplied IComparer.
        /// </summary>
        /// <param name="values">List of values to permute.</param>
        /// <param name="flags">The type of permutation set to calculate.</param>
        /// <param name="comparer">Comparer used for defining the lexicographic order.</param>
        public Permutations(IEnumerable<T> values, GenerateOptions flags, IComparer<T>? comparer)
        {
            _ = values ?? throw new ArgumentNullException(nameof(values));

            // Copy information provided and then create a parallel int array of lexicographic
            // orders that will be used for the actual permutation algorithm.  
            // The input array is first sorted as required for WithoutRepetition and always just for consistency.
            // This array is constructed one of two way depending on the type of the collection.
            //
            // When type is MetaCollectionType.WithRepetition, then all N! permutations are returned
            // and the lexicographic orders are simply generated as 1, 2, ... N.  
            // E.g.
            // Input array:          {A A B C D E E}
            // Lexicographic Orders: {1 2 3 4 5 6 7}
            // 
            // When type is MetaCollectionType.WithoutRepetition, then fewer are generated, with each
            // identical element in the input array not repeated.  The lexicographic sort algorithm
            // handles this natively as long as the repetition is repeated.
            // E.g.
            // Input array:          {A A B C D E E}
            // Lexicographic Orders: {1 1 2 3 4 5 5}

            Flags = flags;
            _myValues = values.ToList();
            _myLexicographicOrders = new int[_myValues.Count];

            if (Type == GenerateOption.WithRepetition)
            {
                for (var i = 0; i < _myLexicographicOrders.Length; ++i)
                {
                    _myLexicographicOrders[i] = i;
                }
            }
            else
            {
                comparer ??= Comparer<T>.Default;

                _myValues.Sort(comparer);
                var j = 1;
                if (_myLexicographicOrders.Length > 0)
                {
                    _myLexicographicOrders[0] = j;
                }

                for (var i = 1; i < _myLexicographicOrders.Length; ++i)
                {
                    if (comparer.Compare(_myValues[i - 1], _myValues[i]) != 0)
                    {
                        ++j;
                    }

                    _myLexicographicOrders[i] = j;
                }
            }

            _count = new Lazy<BigInteger>(GetCount);
        }

        /// <summary>
        /// Gets an enumerator for collecting the list of permutations.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<IReadOnlyList<T>> GetEnumerator()
        {
            var lexicographicalOrders = _myLexicographicOrders.ToArray();
            var values = new List<T>(_myValues);
            yield return AllowMutation ? _myValues : new List<T>(_myValues);
            if (values.Count < 2)
                yield break;

            while (true)
            {
                var i = lexicographicalOrders.Length - 1;

                while (lexicographicalOrders[i - 1] >= lexicographicalOrders[i])
                {
                    --i;
                    if (i == 0)
                        yield break;
                }

                var j = lexicographicalOrders.Length;

                while (lexicographicalOrders[j - 1] <= lexicographicalOrders[i - 1])
                    --j;

                Swap(i - 1, j - 1);

                ++i;

                j = lexicographicalOrders.Length;
                while (i < j)
                {
                    Swap(i - 1, j - 1);
                    ++i;
                    --j;
                }

                yield return AllowMutation ? _myValues : new List<T>(_myValues);
            }

            void Swap(int i, int j)
            {
                var valueTemp = values[i];
                values[i] = values[j];
                values[j] = valueTemp;
                var orderTemp = lexicographicalOrders[i];
                lexicographicalOrders[i] = lexicographicalOrders[j];
                lexicographicalOrders[j] = orderTemp;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// The count of all permutations that will be returned.
        /// If <see cref="Type"/> is <see cref="GenerateOption.WithoutRepetition"/>, then this does not count equivalent result sets.  
        /// I.e., count of permutations of "AAB" will be 3 instead of 6.  
        /// If <see cref="Type"/> is <see cref="GenerateOption.WithRepetition"/>, then this is all combinations and is therefore N!, where N is the number of values in the input set.
        /// </summary>
        public BigInteger Count => _count.Value;

        /// <summary>
        /// The flags used to generate the result sets.
        /// </summary>
        public GenerateOptions Flags { get; }

        /// <summary>
        /// The type of permutations set that is generated.
        /// </summary>
        public GenerateOption Type => (Flags & GenerateOptions.WithRepetition) != 0 ? GenerateOption.WithRepetition : GenerateOption.WithoutRepetition;

        /// <summary>
        /// The upper index of the meta-collection, equal to the number of items in the input set.
        /// </summary>
        public int UpperIndex => _myValues.Count;

        /// <summary>
        /// The lower index of the meta-collection, equal to the number of items returned each iteration.
        /// This is always equal to <see cref="UpperIndex"/>.
        /// </summary>
        public int LowerIndex => _myValues.Count;

        private bool AllowMutation => (Flags & GenerateOptions.AllowMutation) != 0;

        /// <summary>
        /// Calculates the total number of permutations that will be returned.  
        /// As this can grow very large, extra effort is taken to avoid overflowing the accumulator.  
        /// While the algorithm looks complex, it really is just collecting numerator and denominator terms
        /// and cancelling out all of the denominator terms before taking the product of the numerator terms.  
        /// </summary>
        /// <returns>The number of permutations.</returns>
        private BigInteger GetCount()
        {
            var runCount = 1;
            var divisors = Enumerable.Empty<int>();
            var numerators = Enumerable.Empty<int>();

            for (var i = 1; i < _myLexicographicOrders.Length; ++i)
            {
                numerators = numerators.Concat(SmallPrimeUtility.Factor(i + 1));

                if (_myLexicographicOrders[i] == _myLexicographicOrders[i - 1])
                {
                    ++runCount;
                }
                else
                {
                    for (var f = 2; f <= runCount; ++f)
                        divisors = divisors.Concat(SmallPrimeUtility.Factor(f));

                    runCount = 1;
                }
            }

            for (var f = 2; f <= runCount; ++f)
                divisors = divisors.Concat(SmallPrimeUtility.Factor(f));

            return SmallPrimeUtility.EvaluatePrimeFactors(
                SmallPrimeUtility.DividePrimeFactors(numerators, divisors)
            );
        }

        /// <summary>
        /// A list of T that represents the order of elements as originally provided.
        /// </summary>
        private readonly List<T> _myValues;

        /// <summary>
        /// Parallel array of integers that represent the location of items in the myValues array.
        /// This is generated at Initialization and is used as a performance speed up rather that
        /// comparing T each time, much faster to let the CLR optimize around integers.
        /// </summary>
        private readonly int[] _myLexicographicOrders;

        /// <summary>
        /// Lazy-calculated <see cref="Count"/>.
        /// </summary>
        private readonly Lazy<BigInteger> _count;
    }
}