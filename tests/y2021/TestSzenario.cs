using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;


namespace advent.of.code.tests.y2021;

[SimpleJob(RuntimeMoniker.Net70)]
[MarkdownExporterAttribute.GitHub]

public class TestScenario
{
    private ImmutableDictionary<int,IPuzzleTest> subjects;

    private IEnumerable<Type> GetAllTypesThatImplementInterface<IPuzzleTest>()
    => System.Reflection.Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(type => typeof(IPuzzleTest).IsAssignableFrom(type) 
            && !type.IsInterface 
            && Attribute.IsDefined(type, typeof(TraitAttribute)));


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
            .Add(12, new TestDay12())
            .Add(13, new TestDay13())
            .Add(14, new TestDay14());
    }

    [Params(1,2,3,4,5,6,7,8,9,10,11,12,13,14)]
    //[Params(14)]
    public int Day;
    
    [Benchmark]
    public void Silver() => this.subjects[Day].PuzzleSilver();
    
    [Benchmark]
    public void Gold() => this.subjects[Day].PuzzleGold();
}
