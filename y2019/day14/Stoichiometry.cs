// http://adventofcode.com/2019/day/14


using System.Linq;
using System.Collections.Generic;


using advent.of.code;
using System;
using System.Collections.Immutable;

namespace advent.of.code.y2019.day14
{
    public readonly struct Reagent
    {
        public readonly string Chemical;
        public readonly int Quantity;

        public Reagent(string chemical, int quantity) {
            this.Chemical = chemical;
            this.Quantity = quantity;
        }

        public static Reagent Parse(string s) 
        {
            string[] parts = s.Trim().Split(' ');
            return new Reagent(parts.Last(), Convert.ToInt32(parts.First()));
        }
    }

    public readonly struct Reaction
    {
        public readonly string Chemical;
        public readonly int Quantity;

        public readonly ImmutableList<Reagent> Reagents;

        public Reaction(string chemical, int quantity, 
            IEnumerable<Reagent> reagents) {
            this.Chemical = chemical;
            this.Quantity = quantity;
            this.Reagents = ImmutableList<Reagent>.Empty.AddRange(reagents); 
        }

        public static Reaction Parse(string s) {

            string[] parts = s.Trim().Split(" => ");

            string[] right = parts.Last().Trim().Split(' ');
            string[] reagents = parts.First().Trim().Split(',');

            return new Reaction(right.Last().Trim(), Convert.ToInt32(right.First().Trim()), 
                reagents.Select( x => Reagent.Parse(x)));
        }
    }



    public static class Stoichiometry
    {
        public static IEnumerable<Reaction> GetReactions(
               this IEnumerable<string> lines)
           => lines.Select(line => Reaction.Parse(line));


        public static ImmutableDictionary<string,Reaction> Substitute(
            this ImmutableDictionary<string,Reaction> reactions)
        {
            return reactions.Aggregate( reactions, 
                (accu, current) => accu.SetItem(current.Key, Subst(current.Value, reactions)));
        }

        private static Reaction Subst(Reaction old, ImmutableDictionary<string,Reaction> reactions)
        {
            var reagents = old.Reagents
                .SelectMany( reagent => {
                    if (reactions.TryGetValue(reagent.Chemical, out var refReaction)) {
                        if (refReaction.Quantity <= reagent.Quantity)
                        {
                            int factor = Convert.ToInt32(
                            Math.Ceiling( 
                                Convert.ToDecimal(reagent.Quantity) / 
                                Convert.ToDecimal(refReaction.Quantity)));
                            return refReaction.Reagents.Select(
                                r=> new Reagent(r.Chemical, r.Quantity*factor));
                        }

                    }
                    return new []{reagent};
                })
                .GroupBy( x => x.Chemical)
                .Select( grp => new Reagent( grp.Key, grp.Sum( x => x.Quantity)));
            return new Reaction(old.Chemical, old.Quantity, reagents);
        }


    }
}