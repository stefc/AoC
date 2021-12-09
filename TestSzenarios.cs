using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

using advent.of.code.tests.y2021;

namespace advent.of.code;

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
[MedianColumn]
[HtmlExporter]
public class TestScenarios
{
    private TestDay9 subject;

    [GlobalSetup]
    public void Setup()
    {
        this.subject = new TestDay9();
    }


    

    [Benchmark]
    public void Silver() => this.subject.PuzzleSilver();
    
    
    [Benchmark]
    public void Gold() => this.subject.PuzzleGold();
}
