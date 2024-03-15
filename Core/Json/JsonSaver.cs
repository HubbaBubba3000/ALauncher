using System.IO;
using ALauncher.Data;
using System.Threading.Tasks;
using Utf8Json;

namespace ALauncher.Core; 

public sealed class JsonSaver<IConfig> : IDisposable
{
    public static void Save(IConfig config, string path)
    {
        using (var filestream = new StreamWriter(path, false))
            JsonSerializer.Serialize<IConfig>(filestream.BaseStream, config);
    }
    public static Task SaveAsync(IConfig config, string path)
    {
        using (var filestream = new StreamWriter(path, false))
            return JsonSerializer.SerializeAsync<IConfig>(filestream.BaseStream, config);
    }

    public void Dispose() { }
}