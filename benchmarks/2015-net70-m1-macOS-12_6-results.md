``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.6 (21G115) [Darwin 21.6.0]
Apple M1, 1 CPU, 8 logical and 8 physical cores
.NET SDK=7.0.100-rc.2.22477.23
  [Host]   : .NET 7.0.0 (7.0.22.47203), Arm64 RyuJIT AdvSIMD
  .NET 7.0 : .NET 7.0.0 (7.0.22.47203), Arm64 RyuJIT AdvSIMD

Job=.NET 7.0  Runtime=.NET 7.0  

```
| Method | Day |           Mean |       Error |      StdDev |
|------- |---- |---------------:|------------:|------------:|
| **Silver** |   **1** |       **100.8 μs** |     **0.28 μs** |     **0.25 μs** |
|   Gold |   1 |       105.8 μs |     0.27 μs |     0.22 μs |
| **Silver** |   **2** |       **224.3 μs** |     **2.12 μs** |     **1.98 μs** |
|   Gold |   2 |       219.4 μs |     1.60 μs |     1.50 μs |
| **Silver** |   **3** |     **1,479.9 μs** |     **0.35 μs** |     **0.31 μs** |
|   Gold |   3 |     2,094.3 μs |     1.07 μs |     0.89 μs |
| **Silver** |   **4** |    **57,415.3 μs** |    **17.63 μs** |    **14.72 μs** |
|   Gold |   4 | 1,909,636.5 μs | 1,917.22 μs | 1,699.57 μs |
