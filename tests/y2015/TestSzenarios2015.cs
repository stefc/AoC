using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;


namespace advent.of.code.tests.y2015;

[SimpleJob(RuntimeMoniker.Net60)]
[MarkdownExporterAttribute.GitHub]

public class TestScenario2015
{
    private ImmutableDictionary<int,IPuzzleTest> subjects;


    [GlobalSetup]
    public void Setup()
    {
        this.subjects = ImmutableDictionary<int,IPuzzleTest>.Empty 
            .Add(1, new TestDay1())
            .Add(2, new TestDay2())
            .Add(3, new TestDay3())
            .Add(4, new TestDay4());
    }

    //[Params(1,2,3,4,5,6,7,8,9,10,11)]
    [Params(1,2,3,4)]
    public int Day;
    
    [Benchmark]
    public void Silver() => this.subjects[Day].PuzzleSilver();
    
    [Benchmark]
    public void Gold() => this.subjects[Day].PuzzleGold();
}
