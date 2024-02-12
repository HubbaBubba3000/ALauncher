```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.3803/22H2/2022Update)
AMD A4-9125 RADEON R3, 4 COMPUTE CORES 2C+2G, 1 CPU, 2 logical and 2 physical cores
.NET SDK 7.0.304
  [Host] : .NET 7.0.7 (7.0.723.27404), X64 RyuJIT AVX2

Job=MediumRun  Toolchain=InProcessNoEmitToolchain  IterationCount=15  
LaunchCount=2  WarmupCount=10  

```
| Method         | Mean      | Error    | StdDev   | Gen0    | Gen1   | Allocated |
|--------------- |----------:|---------:|---------:|--------:|-------:|----------:|
| Parse          | 106.04 μs | 2.303 μs | 3.376 μs | 16.1133 |      - |   8.24 KB |
| ParseAsync     |  94.22 μs | 0.362 μs | 0.542 μs |  9.6436 | 2.3193 |   4.77 KB |
| ParseUTF8      | 103.23 μs | 2.273 μs | 3.402 μs | 16.3574 |      - |   8.37 KB |
| ParseUTF8Async |  88.74 μs | 0.492 μs | 0.721 μs |  8.7891 | 2.0752 |   4.44 KB |
