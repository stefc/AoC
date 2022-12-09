``` ini

BenchmarkDotNet=v0.13.1, OS=macOS Monterey 12.0.1 (21A559) [Darwin 21.1.0]
Apple M1, 1 CPU, 8 logical and 8 physical cores
.NET SDK=6.0.100
  [Host]   : .NET 6.0.0 (6.0.21.52210), Arm64 RyuJIT
  .NET 6.0 : .NET 6.0.0 (6.0.21.52210), Arm64 RyuJIT

Job=.NET 6.0  Runtime=.NET 6.0  

```
| Method | Day |           Mean |       Error |    StdDev |
|------- |---- |---------------:|------------:|----------:|
| **Silver** |   **1** |       **110.2 μs** |     **0.65 μs** |   **0.58 μs** |
|   Gold |   1 |       139.9 μs |     0.33 μs |   0.29 μs |
| **Silver** |   **2** |       **338.1 μs** |     **4.45 μs** |   **3.94 μs** |
|   Gold |   2 |       312.7 μs |     2.51 μs |   2.22 μs |
| **Silver** |   **3** |     **1,588.4 μs** |     **2.68 μs** |   **2.24 μs** |
|   Gold |   3 |     2,238.1 μs |     3.55 μs |   2.77 μs |
| **Silver** |   **4** |    **52,069.0 μs** |    **69.26 μs** |  **64.78 μs** |
|   Gold |   4 | 1,710,779.5 μs | 1,061.13 μs | 940.66 μs |
