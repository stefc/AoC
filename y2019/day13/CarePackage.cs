// http://adventofcode.com/2019/day/13


using System.Linq;
using System.Collections.Generic;


using advent.of.code;
using advent.of.code.common;
using System;

namespace advent.of.code.y2019.day13
{

    using static F;

    public enum TileId
    {
        Empty = 0,
        Wall = 1,
        Block = 2,
        Paddle = 3,
        Ball = 4,
    }


    public class TileState
    {
        public readonly Point Position;
        public readonly TileId TileId;

        public TileState(int x, int y, int id) : this(new Point(x, y), (TileId)id)
        { }

        private TileState(Point position, TileId tileId)
        {
            this.Position = position;
            this.TileId = tileId;
        }
    }




    public static class CarePackage
    {
        public static IEnumerable<TileState> ToTiles(this IEnumerable<int> output)
        => output
            .Select((x, index) => new { x, index })
            .GroupBy(g => g.index / 3, i => i.x)
            .Select(g => new TileState(
               g.ElementAt(0),
               g.ElementAt(1),
               g.ElementAt(2))
            );

        public static IEnumerable<Either<TileState, int>> ToTilesOrScore(
            this IEnumerable<int> output)
        {
            return output
            .Select((x, index) => new { x, index })
            .GroupBy(g => g.index / 3, i => i.x)
            .Select(g =>
            {
                var x = g.ElementAt(0);
                var y = g.ElementAt(1);
                return ToEither(x == -1 && y == 0,
                    () => new TileState(x, y, g.ElementAt(2)),
                    () => g.ElementAt(2));
            });
        }

        private static Either<TileState, int> ToEither(bool isScore, 
            Func<TileState> getLeft, Func<int> getRight)
        => isScore ? (Either<TileState,int>)
            Right(getRight()) : Left(getLeft());
    }
}