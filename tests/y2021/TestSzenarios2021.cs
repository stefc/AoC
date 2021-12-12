using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;


namespace advent.of.code.tests.y2021;

[SimpleJob(RuntimeMoniker.Net60)]
[MarkdownExporterAttribute.GitHub]

public class TestScenario2021
{
    private ImmutableDictionary<int,IPuzzleTest> subjects;


    [GlobalSetup]
    public void Setup()
    {
        this.subjects = ImmutableDictionary<int,IPuzzleTest>.Empty 
            .Add(1, new TestDay1())
            .Add(2, new TestDay2())
            .Add(3, new TestDay3())
            .Add(4, new TestDay4())
            .Add(5, new TestDay5())
            .Add(6, new TestDay6())
            .Add(7, new TestDay7())
            .Add(8, new TestDay8())
            .Add(9, new TestDay9())
            .Add(10, new TestDay10())
            .Add(11, new TestDay11())
            .Add(12, new TestDay12());
    }

    //[Params(1,2,3,4,5,6,7,8,9,10,11,12)]
    [Params(12)]
    public int Day;
    
    [Benchmark]
    public void Silver() => this.subjects[Day].PuzzleSilver();
    
    [Benchmark]
    public void Gold() => this.subjects[Day].PuzzleGold();
}
