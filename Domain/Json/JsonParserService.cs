using System.IO;
using System.Threading.Tasks;
using Utf8Json;

namespace ALauncher.Domain.Json
{
    public sealed class JsonParserService : IDisposable
    {
        public T Parse<T>(string conf)
        {
            using (var filestream = new StreamReader(conf))
                return JsonSerializer.Deserialize<T>(filestream.BaseStream);
        }
        public async ValueTask<T> ParseAsync<T>(string file)
        {
            using (var stream = new StreamReader(file))
                return await JsonSerializer.DeserializeAsync<T>(stream.BaseStream);
        }
        public void Dispose() { }
    }
}