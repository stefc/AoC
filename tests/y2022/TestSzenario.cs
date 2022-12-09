using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace advent.of.code.tests.y2022;

[SimpleJob(RuntimeMoniker.Net70)]
[MarkdownExporterAttribute.GitHub]

public class TestScenario
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
            .Add(5, new TestDay4())
            .Add(6, new TestDay4())
            ;
    }

    [Params(1,2,3,4,5,6)]
    public int Day;
    
    [Benchmark]
    public void Silver() => this.subjects[Day].PuzzleSilver();
    
    [Benchmark]
    public void Gold() => this.subjects[Day].PuzzleGold();
}
