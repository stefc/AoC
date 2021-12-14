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
| Silver |   1 |       188.10 us |     0.151 us |     0.118 us |
|   Gold |   1 |       387.50 us |     0.323 us |     0.286 us |
| Silver |   2 |       591.72 us |     0.397 us |     0.310 us |
|   Gold |   2 |       586.68 us |     0.601 us |     0.502 us |
| Silver |   3 |     3,113.23 us |     2.434 us |     2.033 us |
|   Gold |   3 |     2,729.38 us |     5.136 us |     4.289 us |
| Silver |   4 |    54,794.64 us |    78.376 us |    61.191 us |
|   Gold |   4 |   168,620.24 us |   238.500 us |   211.424 us |
| Silver |   5 |   108,233.30 us |   640.987 us |   535.253 us |
|   Gold |   5 |   278,629.43 us | 4,640.861 us | 4,341.064 us |
| Silver |   6 |        65.35 us |     0.075 us |     0.063 us |
|   Gold |   6 |        68.93 us |     0.140 us |     0.117 us |
| Silver |   7 |        86.85 us |     0.159 us |     0.141 us |
|   Gold |   7 |        84.89 us |     0.227 us |     0.212 us |
| Silver |   8 |       641.43 us |    11.723 us |    10.966 us |
|   Gold |   8 |     2,215.15 us |    42.539 us |    41.779 us |
| Silver |   9 |     6,496.03 us |    60.787 us |    56.860 us |
|   Gold |   9 |     8,641.95 us |    87.986 us |    77.998 us |
| Silver |  10 |       437.26 us |     7.735 us |    14.144 us |
|   Gold |  10 |       619.24 us |    12.023 us |    26.137 us |
| Silver |  11 |    68,321.85 us | 1,347.790 us | 1,498.065 us |
|   Gold |  11 |   141,987.85 us | 2,732.262 us | 2,805.832 us |
| Silver |  12 |     8,714.23 us |    28.554 us |    25.313 us |
|   Gold |  12 | 1,386,036.64 us | 5,543.955 us | 5,185.819 us |
| Silver |  13 |     1,329.58 us |     4.558 us |     3.558 us |
|   Gold |  13 |     2,181.80 us |    42.066 us |    43.199 us |
| Silver |  14 |       853.78 us |     0.947 us |     0.791 us |
|   Gold |  14 |     3,703.42 us |     6.889 us |     6.444 us |

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