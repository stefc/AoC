using advent.of.code.tests.y2015;
using advent.of.code.tests.y2021;
using BenchmarkDotNet.Running;

namespace advent.of.code;

class Program
{
	static void Main(string[] args)
	{
		BenchmarkRunner.Run<TestScenario2015>();
		//BenchmarkRunner.Run<TestScenario2021>();
	}
}
