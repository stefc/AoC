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

### Zeiten

* BenchmarkDotNet=v0.13.0, OS=macOS 12.0.1 (21A559) [Darwin 21.1.0]

* Apple M1, 1 CPU, 8 logical and 8 physical cores

* .NET SDK=6.0.100, Arm64 RyuJIT

| Method | Day |          Mean |        Error |       StdDev |
|------- |---- |--------------:|-------------:|-------------:|
| Silver |   1 |     190.61 us |     0.119 us |     0.106 us |
|   Gold |   1 |     385.32 us |     0.883 us |     0.826 us |
| Silver |   2 |     593.98 us |     0.859 us |     0.762 us |
|   Gold |   2 |     586.34 us |     0.521 us |     0.407 us |
| Silver |   3 |   3,089.94 us |     3.491 us |     3.095 us |
|   Gold |   3 |   2,701.98 us |     4.893 us |     4.086 us |
| Silver |   4 |  55,323.68 us |    76.324 us |    67.659 us |
|   Gold |   4 | 168,551.17 us |   162.517 us |   144.067 us |
| Silver |   5 | 108,698.54 us |   623.102 us |   582.850 us |
|   Gold |   5 | 275,873.66 us | 3,212.588 us | 2,847.876 us |
| Silver |   6 |      65.43 us |     0.039 us |     0.032 us |
|   Gold |   6 |      71.73 us |     0.365 us |     0.342 us |
| Silver |   7 |      87.14 us |     0.639 us |     0.534 us |
|   Gold |   7 |      85.18 us |     0.193 us |     0.161 us |
| Silver |   8 |     650.05 us |    12.861 us |    24.469 us |
|   Gold |   8 |   2,209.85 us |    41.198 us |    42.308 us |
| Silver |   9 |   6,525.38 us |    53.550 us |    50.091 us |
|   Gold |   9 |   8,675.28 us |    52.951 us |    46.940 us |
| Silver |  10 |     421.03 us |     6.845 us |     6.403 us |
|   Gold |  10 |     588.05 us |     7.668 us |     6.403 us |
## 2015 

* NotQuiteLisp (#1) ->
* WrappingPaper (#2) -> 
* SphericalHouses (#3) ->
* StockingSuffer (#4) -> MD5 Hashing / Paralleles First()  Brute Force 
* 