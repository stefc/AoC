# AoC

Hier eine Übersicht über alle Puzzle und was für Algorithmen verwendet wurden sind.

## 2021 

* SonarSweep (#1) -> Zip & Window Linq Funktionen
* Dive (#2) -> Beschleunigung 
* BinaryDiagnostic (#3) -> BitArray 2d mit Mcb & Lcb Funktionen
* GiantSquid (#4) -> Bingo Ermittlung in Zeilen und Spalten (Linq Chunk)
* HydroVenture (#5) -> Bresenham Algorithmus Zeichnen Linie
* LaternFish (#6) -> Circular Shift Register mit Feedback
* Treachery Of Whales (#7) -> Avg + Bionomal Formel (n*(n+1)/2)
* Seven Segment Search (#8) -> Backtracking 
* Smoke Basin (#9) -> Rekursiver Fill Algorithmus ala Minesweeper 

### Zeiten

BenchmarkDotNet=v0.13.0, OS=macOS 12.0.1 (21A559) [Darwin 21.1.0]
Apple M1, 1 CPU, 8 logical and 8 physical cores, .NET SDK=6.0.100, Arm64 RyuJIT
#### Day 1
| Method |     Mean |   Error |  StdDev |   Median |    Gen 0 |   Gen 1 | Gen 2 | Allocated |
|------- |---------:|--------:|--------:|---------:|---------:|--------:|------:|----------:|
| Silver | 188.2 us | 0.22 us | 0.19 us | 188.2 us |  51.5137 | 16.6016 |     - |    106 KB |
|   Gold | 388.7 us | 0.33 us | 0.29 us | 388.8 us | 189.4531 |       - |     - |    387 KB |

#### Day 2 
| Method |     Mean |   Error |  StdDev |   Median |    Gen 0 | Gen 1 | Gen 2 | Allocated |
|------- |---------:|--------:|--------:|---------:|---------:|------:|------:|----------:|
| Silver | 593.7 us | 2.52 us | 2.36 us | 592.6 us | 323.2422 |     - |     - |    662 KB |
|   Gold | 589.8 us | 0.91 us | 0.76 us | 589.7 us | 327.1484 |     - |     - |    669 KB |

#### Day 3 
| Method |     Mean |     Error |    StdDev |   Median |     Gen 0 |    Gen 1 | Gen 2 | Allocated |
|------- |---------:|----------:|----------:|---------:|----------:|---------:|------:|----------:|
| Silver | 3.142 ms | 0.0245 ms | 0.0218 ms | 3.136 ms | 1796.8750 | 398.4375 |     - |      4 MB |
|   Gold | 2.763 ms | 0.0199 ms | 0.0186 ms | 2.766 ms | 1656.2500 | 175.7813 |     - |      3 MB |

#### Day 4
| Method |      Mean |    Error |   StdDev |    Median |      Gen 0 |     Gen 1 | Gen 2 | Allocated |
|------- |----------:|---------:|---------:|----------:|-----------:|----------:|------:|----------:|
| Silver |  69.57 ms | 0.320 ms | 0.299 ms |  69.42 ms | 33000.0000 |  250.0000 |     - |     67 MB |
|   Gold | 212.06 ms | 0.832 ms | 0.738 ms | 212.05 ms | 92000.0000 | 1000.0000 |     - |    188 MB |

#### Day 5 
| Method |     Mean |   Error |  StdDev |   Median |      Gen 0 |      Gen 1 |     Gen 2 | Allocated |
|------- |---------:|--------:|--------:|---------:|-----------:|-----------:|----------:|----------:|
| Silver | 158.3 ms | 2.71 ms | 2.26 ms | 157.6 ms | 31750.0000 |  4500.0000 | 1250.0000 |    100 MB |
|   Gold | 402.7 ms | 1.62 ms | 1.26 ms | 403.2 ms | 67000.0000 | 11000.0000 | 3000.0000 |    209 MB |

#### Day 6 
| Method |     Mean |    Error |   StdDev |   Median |   Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------- |---------:|---------:|---------:|---------:|--------:|-------:|------:|----------:|
| Silver | 65.44 us | 0.033 us | 0.029 us | 65.45 us | 14.8926 | 7.4463 |     - |     31 KB |
|   Gold | 68.83 us | 0.109 us | 0.091 us | 68.85 us | 22.9492 | 7.5684 |     - |     47 KB |

#### Day 7 
| Method |        Mean |    Error |   StdDev |      Median |   Gen 0 |   Gen 1 | Gen 2 | Allocated |
|------- |------------:|---------:|---------:|------------:|--------:|--------:|------:|----------:|
| Silver | 4,553.82 us | 2.336 us | 1.951 us | 4,553.55 us | 85.9375 |       - |     - |    181 KB |
|   Gold |    84.71 us | 0.165 us | 0.138 us |    84.68 us | 33.8135 | 11.2305 |     - |     69 KB |
#### Day 8 
| Method |       Mean |   Error |  StdDev |     Median |     Gen 0 | Gen 1 | Gen 2 | Allocated |
|------- |-----------:|--------:|--------:|-----------:|----------:|------:|------:|----------:|
| Silver |   892.0 us | 1.63 us | 1.45 us |   891.6 us |  727.5391 |     - |     - |      1 MB |
|   Gold | 4,982.0 us | 7.61 us | 6.74 us | 4,982.4 us | 2000.0000 |     - |     - |      4 MB |

#### Day 9
| Method |      Mean |     Error |    StdDev |    Median |     Gen 0 |    Gen 1 |    Gen 2 | Allocated |
|------- |----------:|----------:|----------:|----------:|----------:|---------:|---------:|----------:|
| Silver |  9.981 ms | 0.0651 ms | 0.0577 ms |  9.973 ms | 4062.5000 | 265.6250 | 109.3750 |     10 MB |
|   Gold | 13.594 ms | 0.1352 ms | 0.1265 ms | 13.575 ms | 6281.2500 | 250.0000 | 109.3750 |     14 MB |
## 2015 

* NotQuiteLisp (#1) ->
* WrappingPaper (#2) -> 
* SphericalHouses (#3) ->
* StockingSuffer (#4) -> MD5 Hashing / Paralleles First()  Brute Force 
* 