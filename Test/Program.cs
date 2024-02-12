using ALauncher.Model;
using BenchmarkDotNet.Running;

namespace ALauncher.Test;

public class Program {
    public static void Main() {
        BenchmarkRunner.Run<JsonParserBench>();
    }
}