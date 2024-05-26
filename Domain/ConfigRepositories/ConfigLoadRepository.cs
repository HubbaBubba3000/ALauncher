using System.IO;
using System.Threading.Tasks;
using System.Windows;
using ALauncher.Abstractions.Domain;
using ALauncher.Domain.Json;


namespace ALauncher.Domain.ConfigRepositories;

public class ConfigLoadRepository : Repository
{
    public T Load<T>(string configName) where T : IAgregator 
    {
        return JsonParser.Parse<T>($"{WorkFolder}/{configName}.json");
    }
    public ValueTask<T> LoadAsync<T>(string configName) where T : IAgregator 
    {
        return JsonParser.ParseAsync<T>($"{WorkFolder}/{configName}.json");
    }
    readonly JsonParserService JsonParser;
    public ConfigLoadRepository(JsonParserService jsonParser) 
    {
        JsonParser = jsonParser;
    }
}