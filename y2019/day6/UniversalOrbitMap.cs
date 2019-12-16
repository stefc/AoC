// http://adventofcode.com/2019/day/6

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;

using advent.of.code;
using advent.of.code.common.Bst;

namespace advent.of.code.y2019.day6
{
    using static advent.of.code.common.Bst.Tree;

    using NodeMap = ImmutableDictionary<string, Tree<string>>;
    using ParentSet = ImmutableHashSet<string>;

    public static class UniversalOrbitMap
    {
        public static int CountOrbits(this Tree<string> map, int level = 0)
        => map.Match(
            () => 0,
            (child, _, sibling) =>
                level
                + child.CountOrbits(level + 1) 
                + sibling.CountOrbits(level));

        public static int GetHeight(this Tree<string> map)
        => map.Match(() => -1,
            (child, _, sibling)
            => Math.Max(child.GetHeight(), sibling.GetHeight() - 1) + 1);

        public static Tree<string> CreateMapFromLines(IEnumerable<string> lines) 
        {
            var relations = lines
                .Select(line => line.Trim().Split(")"))
                .GroupBy(x => x.First(), x => x.Last())
                .ToImmutableDictionary(grp => grp.Key, grp => grp.ToImmutableHashSet());

            Func<string, ImmutableHashSet<string>> lookUp =
            id => relations.GetValueOrDefault(id, ImmutableHashSet<string>.Empty);

            var accu = (
                parents: relations
                    .Where(kvp => !kvp.Value.IsEmpty)
                    .Select(kvp => kvp.Key)
                    .ToImmutableHashSet(),
                nodes: ImmutableDictionary<string, Tree<string>>.Empty);

            var map = UniversalOrbitMap.Reduce(accu, lookUp);
            return map;
        }

        public static Tree<string> Reduce(
            (ParentSet parents, NodeMap nodes) accu,
            Func<string, ImmutableHashSet<string>> lookUpChildren)
        {
            if (accu.parents.IsNotEmpty()) {
                return Reduce(
                    ReduceParents(accu, lookUpChildren), lookUpChildren);
            }
        
            var nil = Empty<string>();
            var root = accu.nodes
                .Select( kvp => Node(kvp.Value, kvp.Key, nil))
                .SingleOrDefault() ?? nil;

            return root;
        }

        public static Tree<string> Find(this Tree<string> node, string item)
        {
            return node.Match( 
                () => node, 
                (l,value,r) => {
                    if (value.Equals(item))
                        return node;
                    var left = l.Find(item);
                    return left.IsEmpty ? r.Find(item) : left;
                });
        }

        public static IEnumerable<string> GetPath(
            this Tree<string> node, string item) 
        => node.FindPath(item, ImmutableStack<string>.Empty)
            .Pop()
            .Reverse();

        public static IEnumerable<string> GetPathBetween(
            this Tree<string> node, string src, string dest) 
        {
            var destination = node.GetPath(dest);
            var source = node.GetPath(src);

            var equalCount = (from a in destination.Select((val, index) => new { val, index })
                    join b in source.Select((val, index) => new { val, index })
                    on a.index equals b.index
                where a.val == b.val
                select a.val).Count();

            return source.Skip(equalCount)
                .Reverse()
                .Concat(destination.Skip(equalCount-1));
        }
        
        private static ImmutableStack<string> FindPath(
            this Tree<string> node, 
            string item, ImmutableStack<string> accu) 
        {
            return node.Match(
                ()=> ImmutableStack<string>.Empty,
                (l,value,r) => {
                    accu = accu.Push(value);
                    if (value.Equals(item))
                    {
                        return accu;
                    };

                    var leftPath = l.FindPath(item, accu);
                    if (leftPath.IsNotEmpty() && leftPath.Peek().Equals(item))
                        return leftPath;

                    var rightPath = r.FindPath(item,accu.Pop());
                    if (rightPath.IsNotEmpty() && rightPath.Peek().Equals(item))
                        return rightPath;

                    return accu.Pop();
                });
        }

        public static (ParentSet parents, NodeMap nodes) ReduceParents(
            (ParentSet parents, NodeMap nodes) accu,
            Func<string, ImmutableHashSet<string>> lookUpChildren)
        {
            var nil = Empty<string>();
            var leafParents = accu.parents
                .Where(id => lookUpChildren(id).Intersect(accu.parents).IsEmpty())
                .ToImmutableHashSet();

            var nodes = accu.nodes;

            var newNodes = leafParents.Aggregate(nodes,
                (dict, parent) =>
                {
                    var children = lookUpChildren(parent);
                    if (children.IsNotEmpty())
                    {
                        var firstChild = children.Aggregate(nil,
                            (siblingNode, name) =>
                            {
                                if (dict.TryGetValue(name, out var childNode))
                                {
                                    dict = dict.Remove(name);
                                }
                                else
                                {
                                    childNode = nil;
                                }
                                return Node(childNode, name, siblingNode);
                            });
                        dict = dict.SetItem(parent, firstChild);
                    }
                    return dict;
                });
            return (accu.parents.Except(newNodes.Keys), newNodes);
        }
    }
}