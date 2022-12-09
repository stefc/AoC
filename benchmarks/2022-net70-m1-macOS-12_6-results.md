``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.6 (21G115) [Darwin 21.6.0]
Apple M1, 1 CPU, 8 logical and 8 physical cores
.NET SDK=7.0.100-rc.2.22477.23
  [Host]   : .NET 7.0.0 (7.0.22.47203), Arm64 RyuJIT AdvSIMD
  .NET 7.0 : .NET 7.0.0 (7.0.22.47203), Arm64 RyuJIT AdvSIMD

Job=.NET 7.0  Runtime=.NET 7.0  

```
| Method | Day |       Mean |    Error |  StdDev |
|------- |---- |-----------:|---------:|--------:|
| **Silver** |   **1** |   **117.3 μs** |  **0.24 μs** | **0.22 μs** |
|   Gold |   1 |   119.5 μs |  0.08 μs | 0.07 μs |
| **Silver** |   **2** |   **252.9 μs** |  **0.35 μs** | **0.31 μs** |
|   Gold |   2 |   258.3 μs |  0.29 μs | 0.26 μs |
| **Silver** |   **3** | **1,916.4 μs** |  **1.81 μs** | **1.51 μs** |
|   Gold |   3 |   949.5 μs | 10.30 μs | 9.13 μs |
| **Silver** |   **4** |   **356.3 μs** |  **0.36 μs** | **0.30 μs** |
|   Gold |   4 |   352.2 μs |  5.30 μs | 4.14 μs |
| **Silver** |   **5** |   **356.2 μs** |  **1.28 μs** | **1.20 μs** |
|   Gold |   5 |   486.4 μs |  0.36 μs | 0.30 μs |
| **Silver** |   **6** |   **365.7 μs** |  **0.30 μs** | **0.26 μs** |
|   Gold |   6 |   353.2 μs |  0.55 μs | 0.49 μs |
