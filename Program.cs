using BenchmarkDotNet.Running;

namespace advent.of.code;

class Program
{
    static void Main(string[] args)
    {
        BenchmarkRunner.Run<TestScenarios>();
    }
}



/*


[SimpleJob(RuntimeMoniker.Net60)]
public class TestDynmaicPGOScenarios
{
    private IEnumerator<Int32> _source = Enumerable.Range(0, Int32.MaxValue).GetEnumerator();

    [Benchmark]
    public void MoveNext() => _source.MoveNext();
}


|   Method |      Mean |     Error |    StdDev |
|--------- |----------:|----------:|----------:|
| MoveNext | 0.1860 ns | 0.0009 ns | 0.0007 ns |

|   Method |      Mean |     Error |    StdDev |
|--------- |----------:|----------:|----------:|
| MoveNext | 0.7914 ns | 0.0004 ns | 0.0004 ns |

*/