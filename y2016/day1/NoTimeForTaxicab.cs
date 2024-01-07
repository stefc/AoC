// http://adventofcode.com/2015/day/1

namespace advent.of.code.y2016;

static class NoTimeForTaxicap
{

    public static int HowFarBlocksAway(string instructions)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<Direction> ToPath(this string wire)
    => wire.ToSegments()
        .Aggregate(ImmutableList<Direction>.Empty,
        (accu, current) => accu.Add(new Direction(current)));

    
    public readonly struct Direction
    {
        public readonly char Code;
        public readonly int Length;
        private Direction(char code, int length) => (Code, Length) = (code, length);
        public Direction(string stmt) => (Code, Length) = FromString(stmt);

        public override string ToString() => $"{Code}{Length}";

        private static (char, int) FromString(string stmt)
        {
            var ch = char.ToUpper(stmt.FirstOrDefault());
            if (int.TryParse(new String(stmt.Skip(1).ToArray()), out var len))
            {
                return (char.ToUpper(ch), len);
            };
            return (ch, 0);
        }
   }
}