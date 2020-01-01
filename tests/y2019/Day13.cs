using System;
using System.IO;
using System.Linq;
using System.Collections.Immutable;

using Xunit;

using advent.of.code.y2019.day13;
using advent.of.code.y2019.day2;
using advent.of.code.common;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

using static System.Environment;
using SixLabors.Shapes;
using System.Collections.Generic;

namespace advent.of.code.tests.y2019
{
    [Trait("Year", "2019")]
    [Trait("Topic", "intcode")]
    public class TestDay13
    {

        [Fact]
        public void ConvertSequence()
        {
            var seq = new long[] { 1, 2, 3, 6, 5, 4 }.AsEnumerable()
                .Select(Convert.ToInt32);

            var actual = seq.ToTiles();

            Assert.Equal(2, actual.Count());

            Assert.Equal(new common.Point(1, 2), actual.First().Position);
            Assert.Equal(TileId.Paddle, actual.First().TileId);
            Assert.Equal(new common.Point(6, 5), actual.Last().Position);
            Assert.Equal(TileId.Ball, actual.Last().TileId);

            var count = actual.Count(x => x.TileId == TileId.Block);
            Assert.Equal(0, count);
        }

        //[Fact]
        public void Playing()
        {
            string input = File.ReadAllText("tests/y2019/Day13.Input.txt");
            var prg = ProgramAlarm.CreateProgram(input.ToBigNumbers()).Write(0, 2);
            var computer = ProgramAlarm
                .CreateStateMaschine();

            prg = Enumerable.Range(0,36)
                .Aggregate(prg, (accu,idx)=> accu.Write(639+(36*23)+idx,1));

            prg = computer(prg).State.GetOrElse(prg);


            var total = Enumerable.Range(1719, 1).Select( idx => prg.Program[idx]).Sum();
            // Assert.Equal(11441, total);


            var gameState = prg.Output
                .Reverse()
                .Select(Convert.ToInt32)
                .ToTilesOrScore();

            var max = gameState.Aggregate(common.Point.Zero,
            (accu, current) => current.Match(
                l => new common.Point(
                    Math.Max(accu.X, l.Position.X),
                    Math.Max(accu.Y, l.Position.Y)),
                r => accu)
            );

            int w = (max.X+1) * 10;
            int h = (max.Y+2) * 10;

           


            var queue = new long[]
                {0,0,0,0,0,0,0,0,0,
                -1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                +1,+1,+1,
                }
                .Aggregate(
                    ImmutableQueue<long>.Empty, 
                    (accu,current) => accu.Enqueue(current));
            // prg = prg.WithInput(queueAsImmutableStack());
            
            var imgFactory = new ImageFactory(w,h);

            var frame = 0;

            var rnd = new Random();

            var scores = ImmutableList<int>.Empty;
            
            while (frame < 5000) {

                scores = imgFactory.AddFrame(gameState, frame, scores);

                if (prg.OpCode == OpCode.Exit) break;
                
                queue = queue.Dequeue(out var control);
                prg = computer(prg
                        .WithInput(control)
                        .WithOutput(ImmutableStack<long>.Empty))
                    .State.GetOrElse(prg);

                gameState = prg.Output
                    .Reverse()
                    .Select(Convert.ToInt32)
                    .ToTilesOrScore();
                frame += 1;

                if (queue.IsEmpty) {
                    queue = queue.Enqueue(rnd.Next(3)-1);
                }

            }
            
            imgFactory.SaveGif("Test.gif");
            Assert.Equal(11441, scores.Last());
        }

        [Fact]
        public void PuzzleOne()
        {
            string input = File.ReadAllText("tests/y2019/Day13.Input.txt");
            var prg = ProgramAlarm.CreateProgram(input.ToBigNumbers());
            var computer = ProgramAlarm
                .CreateStateMaschine();

            var seq = computer(prg).State.Match(
                   () => Enumerable.Empty<int>(),
                   x => x.Output.Reverse().Select(Convert.ToInt32));

            var actual = seq.ToTiles();


            var count = actual.Count(x => x.TileId == TileId.Block);
            Assert.Equal(213, count);
        }

        [Fact]
        public void PuzzleTwo()
        {

        }
    }

    public class ImageFactory {

        private readonly int weight;
        private readonly int height;

        private readonly Image<Rgba32> gif;

        public ImageFactory(int w, int h)
        {
            this.weight = w;
            this.height = h;

            this.gif = new Image<Rgba32>(weight, height);
            gif.Mutate(context => context.Fill(Rgba32.Red));

        }
        public ImmutableList<int> AddFrame(IEnumerable<Either<TileState, int>> gameState, int frame, ImmutableList<int> scores) {

            var img = new Image<Rgba32>(weight, height);

            var brushWall = new SolidBrush(Rgba32.Gray);
            var brushBrick = new SolidBrush(Rgba32.IndianRed);
            var brushBall = new SolidBrush(Rgba32.Black);
            var brushPaddle = new SolidBrush(Rgba32.PaleGoldenrod);

            var font = SixLabors.Fonts.SystemFonts.CreateFont("Arial", 12);

            int count = gameState.Count();

            foreach (var x in gameState)
            {

                x.Match(tile =>
                {
                    float x = tile.Position.X * 10f;
                    float y = tile.Position.Y * 10f;
                    if (tile.TileId != TileId.Empty)
                    {
                        IBrush brush = null;
                        if (tile.TileId == TileId.Wall)
                        {
                            brush = brushWall;
                        }
                        else if (tile.TileId == TileId.Block)
                        {
                            brush = brushBrick;
                        }
                        else if (tile.TileId == TileId.Ball)
                        {
                            brush = brushBall;
                        }
                        else
                        {
                            brush = brushPaddle;
                        }

                        if (tile.TileId == TileId.Ball)
                        {
                            img.Mutate(i => i.Fill( brush, 
                                new EllipsePolygon(
                                    new PointF(x+5f,y+5f), new SizeF(8f,8f))));
                        }
                        else
                        {
                            img.Mutate(i => i.Fill( brush, 
                               new RectangularPolygon( x,y, 9f, 9f)));
                        }
                    } 
                    else 
                    {
                        img.Mutate(i => i.Fill( Rgba32.White,  
                            new RectangularPolygon( x,y, 10f, 10f)));
                    }
                }, score =>
                {
                    img.Mutate(i => i
                        .Fill(Rgba32.White,  
                            new RectangularPolygon( 12f, this.height - 10f, 150f, 10f))
                        .DrawText($"Score: {score}", font, Rgba32.Black, 
                            new PointF(12f, this.height - 10f)));
                    scores = scores.Add(score);
                });
            }

            
          
            gif.Frames.AddFrame(img.Frames[0]);
            img.Dispose();

            return scores;
        }

        public void SaveGif(string name) {
            string appData = System.IO.Path.Combine(
                Environment.GetFolderPath(
                    SpecialFolder.Desktop, SpecialFolderOption.DoNotVerify),
                    "aoc");

            Directory.CreateDirectory(appData);
            gif.Frames.RemoveFrame(0);
            using (var fs = File.Create(System.IO.Path.Join(appData, name)))
            {
                // var encoder = new GifEncoder
                // {
                //     // Use the palette quantizer without dithering to ensure results
                //     // are consistent
                //     Quantizer = new WebSafePaletteQuantizer(false),

                // };

                gif.SaveAsGif(fs);
            }
        }
    }
}