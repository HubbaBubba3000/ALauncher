using ALauncher.Model;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Attributes;

namespace ALauncher.Test;
[Config(typeof(AntiVirusFriendlyConfig))]
[MemoryDiagnoser]
public class JsonParserBench {
    [Benchmark]
    public SettingsConfig Parse() {
        return JsonParser<SettingsConfig>.Parse("../App/configs/Settings.json");
    }
    [Benchmark]
    public async Task<SettingsConfig> ParseAsync() {
        return await JsonParser<SettingsConfig>.ParseAsync("../App/configs/Settings.json");

    }
    [Benchmark]
    public SettingsConfig ParseUTF8() {
        return Utf8JsonParser<SettingsConfig>.Parse("../App/configs/Settings.json");
    }
    [Benchmark]
    public async Task<SettingsConfig> ParseUTF8Async() {
        return await Utf8JsonParser<SettingsConfig>.ParseAsync("../App/configs/Settings.json");

    }

}