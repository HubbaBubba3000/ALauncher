using System.IO;
using System.Threading.Tasks;
using System.Windows;
using ALauncher.Abstractions.Domain;
using ALauncher.Domain.Json;


namespace ALauncher.Domain.ConfigRepositories;

public class ConfigSaveRepository : Repository
{
    public void Save<T>(T config, string configName) where T : IAgregator 
    {
        JsonSaver.Save(config, $"{WorkFolder}/{configName}.json");
    }
    public Task SaveAsync<T>(T config, string configName) where T : IAgregator 
    {
        return JsonSaver.SaveAsync(config, $"{WorkFolder}/{configName}.json");
    }
    JsonSaverService JsonSaver;
    public ConfigSaveRepository(JsonSaverService jsonSaver) 
    {
        JsonSaver = jsonSaver;
    }
}