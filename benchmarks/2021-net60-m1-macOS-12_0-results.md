``` ini

BenchmarkDotNet=v0.13.1, OS=macOS Monterey 12.0.1 (21A559) [Darwin 21.1.0]
Apple M1, 1 CPU, 8 logical and 8 physical cores
.NET SDK=6.0.100
  [Host]   : .NET 6.0.0 (6.0.21.52210), Arm64 RyuJIT
  .NET 6.0 : .NET 6.0.0 (6.0.21.52210), Arm64 RyuJIT

Job=.NET 6.0  Runtime=.NET 6.0  

```
| Method | Day |          Mean |        Error |       StdDev |
|------- |---- |--------------:|-------------:|-------------:|
| **Silver** |   **1** |     **190.61 μs** |     **0.119 μs** |     **0.106 μs** |
|   Gold |   1 |     385.32 μs |     0.883 μs |     0.826 μs |
| **Silver** |   **2** |     **593.98 μs** |     **0.859 μs** |     **0.762 μs** |
|   Gold |   2 |     586.34 μs |     0.521 μs |     0.407 μs |
| **Silver** |   **3** |   **3,089.94 μs** |     **3.491 μs** |     **3.095 μs** |
|   Gold |   3 |   2,701.98 μs |     4.893 μs |     4.086 μs |
| **Silver** |   **4** |  **55,323.68 μs** |    **76.324 μs** |    **67.659 μs** |
|   Gold |   4 | 168,551.17 μs |   162.517 μs |   144.067 μs |
| **Silver** |   **5** | **108,698.54 μs** |   **623.102 μs** |   **582.850 μs** |
|   Gold |   5 | 275,873.66 μs | 3,212.588 μs | 2,847.876 μs |
| **Silver** |   **6** |      **65.43 μs** |     **0.039 μs** |     **0.032 μs** |
|   Gold |   6 |      71.73 μs |     0.365 μs |     0.342 μs |
| **Silver** |   **7** |      **87.14 μs** |     **0.639 μs** |     **0.534 μs** |
|   Gold |   7 |      85.18 μs |     0.193 μs |     0.161 μs |
| **Silver** |   **8** |     **650.05 μs** |    **12.861 μs** |    **24.469 μs** |
|   Gold |   8 |   2,209.85 μs |    41.198 μs |    42.308 μs |
| **Silver** |   **9** |   **6,525.38 μs** |    **53.550 μs** |    **50.091 μs** |
|   Gold |   9 |   8,675.28 μs |    52.951 μs |    46.940 μs |
| **Silver** |  **10** |     **421.03 μs** |     **6.845 μs** |     **6.403 μs** |
|   Gold |  10 |     588.05 μs |     7.668 μs |     6.403 μs |