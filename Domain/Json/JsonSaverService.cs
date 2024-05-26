using System.IO;
using System.Threading.Tasks;
using ALauncher.Abstractions.Domain;
using Utf8Json;

namespace ALauncher.Domain.Json; 

public sealed class JsonSaverService
{
    public void Save<T>(T config, string path) where T : IAgregator
    {
        using (var filestream = new StreamWriter(path, false))
            JsonSerializer.Serialize(filestream.BaseStream, config);
    }
    public Task SaveAsync<T>(T config, string path)  where T : IAgregator
    {
        using (var filestream = new StreamWriter(path, false))
            return JsonSerializer.SerializeAsync(filestream.BaseStream, config);
    }

}