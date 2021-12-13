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
* Syntax Scoring (#10) -> Öffnende & Schliessende Klammern per Stack ermitteln
* Dumbo Octopus (#11) -> Kettenreaktion an Benachbarte Octopusse (Schlecht Paralelisierbar)
* Passage Pathing (#12) -> Alle möglichen Pfade in einem Graph ermitteln (Null Parallel !)
* Transparent Origami (#13) -> Koordinaten an einer Linie spiegeln und Ergebnis visualisieren

### Zeiten

* BenchmarkDotNet=v0.13.0, OS=macOS 12.0.1 (21A559) [Darwin 21.1.0]

* Apple M1, 1 CPU, 8 logical and 8 physical cores

* .NET SDK=6.0.100, Arm64 RyuJIT

| Method | Day |            Mean |        Error |       StdDev |
|------- |---- |----------------:|-------------:|-------------:|
| Silver |   1 |       193.85 us |     0.405 us |     0.316 us |
|   Gold |   1 |       386.01 us |     1.083 us |     0.904 us |
| Silver |   2 |       588.02 us |     1.245 us |     0.972 us |
|   Gold |   2 |       588.64 us |     1.143 us |     1.014 us |
| Silver |   3 |     3,097.48 us |     4.752 us |     3.968 us |
|   Gold |   3 |     2,713.45 us |     5.743 us |     4.796 us |
| Silver |   4 |    55,175.68 us |   129.790 us |   115.056 us |
|   Gold |   4 |   169,281.89 us |   354.289 us |   331.402 us |
| Silver |   5 |   108,772.49 us |   824.378 us |   771.123 us |
|   Gold |   5 |   278,522.47 us | 2,798.525 us | 2,617.742 us |
| Silver |   6 |        65.67 us |     0.095 us |     0.074 us |
|   Gold |   6 |        68.83 us |     0.116 us |     0.097 us |
| Silver |   7 |        86.91 us |     0.320 us |     0.284 us |
|   Gold |   7 |        85.81 us |     0.207 us |     0.183 us |
| Silver |   8 |       638.63 us |    12.040 us |    11.262 us |
|   Gold |   8 |     2,211.95 us |    33.848 us |    31.661 us |
| Silver |   9 |     6,555.74 us |    39.171 us |    34.724 us |
|   Gold |   9 |     8,633.09 us |   101.021 us |    94.495 us |
| Silver |  10 |       418.59 us |     8.154 us |     7.228 us |
|   Gold |  10 |       604.91 us |    11.623 us |    12.436 us |
| Silver |  11 |    67,503.48 us | 1,292.534 us | 1,769.235 us |
|   Gold |  11 |   133,021.39 us | 2,002.511 us | 1,873.150 us |
| Silver |  12 |    11,809.10 us |    10.320 us |     8.610 us |
|   Gold |  12 | 1,867,125.70 us | 5,196.320 us | 4,056.950 us |
| Silver |  13 |     1,372.00 us |    0.0049 ms |    0.0041 ms |
|   Gold |  13 |     2,159.00 ms |    0.0303 ms |    0.0268 ms |


## 2015 

* NotQuiteLisp (#1) -> 
* WrappingPaper (#2) -> 
* SphericalHouses (#3) ->
* StockingSuffer (#4) -> MD5 Hashing / Paralleles First()  Brute Force 

### Zeiten

* BenchmarkDotNet=v0.13.0, OS=macOS 12.0.1 (21A559) [Darwin 21.1.0]

* Apple M1, 1 CPU, 8 logical and 8 physical cores

* .NET SDK=6.0.100, Arm64 RyuJIT

| Method | Day |           Mean |       Error |    StdDev |
|------- |---- |---------------:|------------:|----------:|
| Silver |   1 |       110.2 us |     0.65 us |   0.58 us |
|   Gold |   1 |       139.9 us |     0.33 us |   0.29 us |
| Silver |   2 |       338.1 us |     4.45 us |   3.94 us |
|   Gold |   2 |       312.7 us |     2.51 us |   2.22 us |
| Silver |   3 |     1,588.4 us |     2.68 us |   2.24 us |
|   Gold |   3 |     2,238.1 us |     3.55 us |   2.77 us |
| Silver |   4 |    52,069.0 us |    69.26 us |  64.78 us |
|   Gold |   4 | 1,710,779.5 us | 1,061.13 us | 940.66 us |