using System.Configuration.Internal;
using ALauncher.Model;
using Utf8Json;
namespace ALauncher.Test;

public class Utf8JsonParser<TConfig> where TConfig : IConfig {
    private static string jsonread(string file) {
        using (var filestream = new StreamReader(file) ) 
            return filestream.ReadToEnd();
    }
    public static TConfig Parse(string config) {
        return JsonSerializer.Deserialize<TConfig>(jsonread(config));
    }
    public static async Task<TConfig> ParseAsync(string config) {
        var filestream = new StreamReader(config, false);
        return await JsonSerializer.DeserializeAsync<TConfig>(filestream.BaseStream);
    }
}