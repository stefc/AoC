// http://adventofcode.com/2019/day/11


using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;


using advent.of.code;
using advent.of.code.common;

namespace advent.of.code.y2019.day13
{

    public enum TileId {
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

        public TileState(int x, int y, int id): this(new Point(x,y), (TileId)id)
        {}

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
            .Select( (x,index) => new {x, index})
            .GroupBy( g => g.index / 3, i => i.x)
            .Select( g => new TileState(
                g.ElementAt(0),
                g.ElementAt(1),
                g.ElementAt(2))
            ); 
    }
}