
using System.IO;
using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using Utf8Json;
using Utf8Json.Resolvers;
using ALauncher.Data;

namespace ALauncher.Core {
    public sealed class JsonParser<TConfig> where TConfig : IConfig, IDisposable {
    public static TConfig? Parse(string conf) {
            using (var filestream = new StreamReader(conf) ) 
            return Utf8Json.JsonSerializer.Deserialize<TConfig>(filestream.BaseStream);
        }
        public static async ValueTask<TConfig?> ParseAsync(string file) {
            using (var stream = new StreamReader(file))
            return await Utf8Json.JsonSerializer.DeserializeAsync<TConfig>(stream.BaseStream);
        }
        public static void Save(TConfig config ,string path) {
            using (var filestream = new StreamWriter(path, false) ) {
                JsonSerializer.Serialize<TConfig>(filestream.BaseStream, config);
            }
        }
        public static Task SaveAsync(TConfig config ,string path) {
            using (var filestream = new StreamWriter(path, false) ) 
               return Utf8Json.JsonSerializer.SerializeAsync<TConfig>(filestream.BaseStream,config);
        }
        public void Dispose() {}
    }
}