using System;
using System.IO;
using System.Linq;

using Xunit;

using advent.of.code.y2019.day11;
using advent.of.code.y2019.day2;
using advent.of.code.common;

namespace advent.of.code.tests.y2019
{
    [Trait("Year", "2019")]
    [Trait("Topic", "intcode")]
    public class TestDay11
    {
        [Fact]
        public void TestRotate() 
        {
            Point pt = Point.North;

            var a1 = pt.RotateRight();
            var a2 = a1.RotateRight();
            var a3 = a2.RotateRight();
            var a4 = a3.RotateRight();

            Assert.Equal(Point.East, a1);
            Assert.Equal(Point.South, a2);
            Assert.Equal(Point.West, a3);
            Assert.Equal(Point.North, a4);

            var b1 = pt.RotateLeft();
            var b2 = b1.RotateLeft();
            var b3 = b2.RotateLeft();
            var b4 = b3.RotateLeft();

            Assert.Equal(Point.West, b1);
            Assert.Equal(Point.South, b2);
            Assert.Equal(Point.East, b3);
            Assert.Equal(Point.North, b4); 
        }

        [Fact]
        public void TestRobotState() {
            var robot = new RobotState();

            Assert.Equal(Point.North, robot.Direction);
            Assert.Equal(Point.Zero, robot.Position);
            Assert.Equal(PaintColor.Black, robot.Color);

            robot = robot
                .WithRotation(Rotation.Left)
                .WithPaint(PaintColor.White)
                .WithMove();
            Assert.Equal(Point.West, robot.Direction);
            Assert.Equal(new Point(-1,0), robot.Position);
            Assert.Equal(PaintColor.Black, robot.Color);
            Assert.True(robot.Painted.ContainsKey(Point.Zero));
        }

        [Fact]
        public void TestSequenceOfCommands() {
            var robot = new RobotState();

            var commands = new (int,int) []{
                (1,0),
                (0,0),
                (1,0),(1,0),
                (0,1),(1,0),(1,0)
                };

            robot = commands
                .Aggregate( robot, (robot, cmd) => robot.Handle(cmd));

            Assert.Equal(6, robot.Painted.Count);
            Assert.Equal(Point.West, robot.Direction);
            Assert.Equal(new Point(0,-1), robot.Position);

            Assert.Equal(4, 
                robot.Painted.Values.Where( x => x == PaintColor.White).Count());
        }
        
        [Fact]
        public void PuzzleOne()
        {
            string input = File.ReadAllText("tests/y2019/Day11.Input.txt");
            var prg = ProgramAlarm.CreateProgram(input.ToBigNumbers());
			var computer = ProgramAlarm
				.CreateStateMaschine();

            var robot = new RobotState();
            var program = prg;

			do {
                var result = computer(program.WithInput((int)robot.Color));

                program = computer(program.WithInput((int)robot.Color))
                    .State.Match( () => program, 
                        x => {
                            var newState = x.WithOutput(x.Output
                                .Pop(out var b)
                                .Pop(out var a));
                            if (newState.OpCode != OpCode.Exit) {
                                robot = robot.Handle((Convert.ToInt32(a),Convert.ToInt32(b)));
                            }
                            return newState;
                        });

            } while (program.OpCode != OpCode.Exit);
            Assert.Equal(2441, robot.Painted.Values.Count());
        }

        [Fact]
        public void PuzzleTwo()
        {
            string input = File.ReadAllText("tests/y2019/Day11.Input.txt");
            var prg = ProgramAlarm.CreateProgram(input.ToBigNumbers());
			var computer = ProgramAlarm
				.CreateStateMaschine();

            var robot = new RobotState().WithPaint(PaintColor.White);
            var program = prg;

			do {
                var result = computer(program.WithInput((int)robot.Color));

                program = computer(program.WithInput((int)robot.Color))
                    .State.Match( () => program, 
                        x => {
                            var newState = x.WithOutput(x.Output
                                .Pop(out var b)
                                .Pop(out var a));
                            if (newState.OpCode != OpCode.Exit) {
                                robot = robot.Handle((Convert.ToInt32(a),Convert.ToInt32(b)));
                            }
                            return newState;
                        });

            } while (program.OpCode != OpCode.Exit);

            var points = robot.Painted.Keys;


            
            var max = points.Aggregate(Point.Zero, (accu, current) => 
                new Point( Math.Max(accu.X, current.X),  Math.Max(accu.Y, current.Y)));

            var min = points.Aggregate(Point.Zero, (accu, current) => 
                new Point( Math.Min(accu.X, current.X),  Math.Min(accu.Y, current.Y)));


            var image = Enumerable.Range(0, max.Y+1)
                .Select( y => string.Concat(Enumerable.Range(0, max.X+1)
                .Select( x => 
                    robot.Painted.TryGetValue(
                        new Point(x,y), out var color) ? 
                            color == PaintColor.White ? '#' : ' ': ' ')))
                .ToArray();

            foreach(var line in image) System.Console.WriteLine(line);
            //Assert.Equal("PZRFPRKC", robot.Painted.Values.Count());
        }
    }
}