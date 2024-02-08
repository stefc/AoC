using System;
using System.IO;
using System.Linq;
using System.Collections.Immutable;

using Xunit;

using advent.of.code.common.Bst;
using advent.of.code.y2019.day6;

namespace advent.of.code.tests.y2019
{
    using static advent.of.code.common.Bst.Tree;

    [Trait("Category", "y2019")]
    public class TestDay6
    {

        [Fact]
        public void TestReduceInto()
        {
            string letters = "abracadabra";

            var letterCount = letters.Aggregate(
                ImmutableDictionary<char, int>.Empty,
                (counts, letter) => counts.TryGetValue(letter, out var count) ?
                    counts.SetItem(letter, count + 1)
                    :
                    counts.Add(letter, 1)
                );
            Assert.Equal(5, letterCount.Count);
            Assert.Equal(letters.Length, letterCount.Sum(kvp => kvp.Value));
            // letterCount == ["a": 5, "b": 2, "r": 2, "c": 1, "d": 1]
        }

        [Fact]
        public void TreeConstruct()
        {
            var lines = new[] {
                "COM)B",
                "B)C",
                "C)D",
                "D)E",
                "E)F",
                "B)G",
                "G)H",
                "D)I",
                "E)J",
                "J)K",
                "K)L",
            };

            var map = UniversalOrbitMap.CreateMapFromLines(lines);
            Assert.Equal(42, map.CountOrbits());
        }


        [Fact]
        public void TreeOperation()
        {
            var tree = CreateSample();
            var orbits = tree.CountOrbits(0);
            Assert.Equal(42, orbits);
        }

        [Fact]
        public void FindPath()
        {
            var root = CreateNextSample();

            var path = root.GetPathBetween("SAN","YOU");

            Assert.Equal(4, path.Count()-1);
        }

        [Fact]
		public void PuzzleOne() {
             var lines = File.ReadAllLines("tests/y2019/Day6.Input.txt");
			 var map = UniversalOrbitMap.CreateMapFromLines(lines);
             Assert.Equal(268504, map.CountOrbits());
        }

        [Fact]
        public void PuzzleTwo()
        {
            var lines = File.ReadAllLines("tests/y2019/Day6.Input.txt");
			var map = UniversalOrbitMap.CreateMapFromLines(lines);
            var path = map.GetPathBetween("SAN","YOU");
            Assert.Equal(409, path.Count()-1);
        }

        private Tree<String> CreateSample()
        {
            // Rechts => Siblings
            // Links => Children
            var nil = Empty<string>();
            var l = Node(nil, "L", nil);
            var k = Node(l, "K", nil);
            var j = Node(k, "J", nil);
            var f = Node(nil, "F", j);
            var e = Node(f, "E", nil);
            var i = Node(nil, "I", e);
            var d = Node(i, "D", nil);
            var h = Node(nil, "H", nil);
            var g = Node(h, "G", nil);
            var c = Node(d, "C", g);
            var b = Node(c, "B", nil);
            var com = Node(b, "COM", nil);
            return com;
        }

        private Tree<String> CreateNextSample()
        {
            var lines = new[] {
                "COM)B",
                "B)C",
                "C)D",
                "D)E",
                "E)F",
                "B)G",
                "G)H",
                "D)I",
                "E)J",
                "J)K",
                "K)L",
                "K)YOU",
                "I)SAN"
            };
            return UniversalOrbitMap.CreateMapFromLines(lines);
        }

    }
}
