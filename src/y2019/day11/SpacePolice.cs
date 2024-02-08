// http://adventofcode.com/2019/day/11

namespace advent.of.code.y2019.day11
{

	public enum PaintColor {
        Black = 0,
        White = 1
    }

    public enum Rotation {
        Left = 0,

        Right = 1
    }
    
    public class RobotState
    {
        public readonly Point Position;
		public readonly Point Direction;
        public readonly ImmutableDictionary<Point,PaintColor> Painted;

        public RobotState(): this(Point.Zero, Point.North, 
            ImmutableDictionary<Point,PaintColor>.Empty)
        {}

        private RobotState(Point position, Point direction, 
            ImmutableDictionary<Point, PaintColor> painted)
        {
            this.Position = position;
			this.Direction = direction;
            this.Painted = painted;
        }

        public bool IsPainted => this.Painted.ContainsKey(this.Position);

        public PaintColor Color 
        => this.Painted.TryGetValue(this.Position, out var color) ? color : PaintColor.Black;

        public RobotState WithRotation( Rotation rotation) 
        => new RobotState(this.Position, rotation == Rotation.Left ?
                    this.Direction.RotateLeft() : 
                    this.Direction.RotateRight(), this.Painted);

        public RobotState WithPaint(PaintColor color)
        => new RobotState(
            this.Position, this.Direction,  
            this.Painted.SetItem(this.Position, color));
        public RobotState WithMove()
        => new RobotState(
            this.Position + this.Direction,  this.Direction, this.Painted);
        
        public RobotState Handle((int paint,int turn) cmd)
        => this
            .WithPaint((PaintColor)cmd.paint)
            .WithRotation((Rotation)cmd.turn)
            .WithMove();
    }

    public static class ExtensionMethods
    {
        
    }


    public static class SpacePolice
    {
    }
}