using System.IO;
using System.Threading.Tasks;
using Utf8Json;
using ALauncher.Data;

namespace ALauncher.Core
{
    public sealed class JsonParser<IConfig> : IDisposable
    {
        public static IConfig? Parse(string conf)
        {
            using (var filestream = new StreamReader(conf))
                return JsonSerializer.Deserialize<IConfig>(filestream.BaseStream);
        }
        public static async ValueTask<IConfig?> ParseAsync(string file)
        {
            using (var stream = new StreamReader(file))
                return await JsonSerializer.DeserializeAsync<IConfig>(stream.BaseStream);
        }
        public void Dispose() { }
    }
}