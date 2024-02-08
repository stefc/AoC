using BenchmarkDotNet.Running;

namespace advent.of.code;

class Program
{
	static void Main(string[] args)
	{
		Console.WriteLine("The number of processors on this computer is {0}.",Environment.ProcessorCount);
		// BenchmarkRunner.Run<advent.of.code.tests.y2015.TestScenario>();
		// BenchmarkRunner.Run<advent.of.code.tests.y2021.TestScenario>();
		BenchmarkRunner.Run<advent.of.code.tests.y2022.TestScenario>();
	}
}
