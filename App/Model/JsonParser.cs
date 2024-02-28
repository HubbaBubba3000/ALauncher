using System.Text.Json;
using System.IO;
using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using Utf8Json;
using ALauncher.Data;

namespace ALauncher.Model {
    public class JsonParser<TConfig> where TConfig : IConfig, IDisposable {
#region system.text.json
        public static TConfig? Parse(string conf) {
            using (var filestream = new StreamReader(conf) ) 
            return System.Text.Json.JsonSerializer.Deserialize<TConfig>(filestream.BaseStream);
        }
        public static async ValueTask<TConfig?> ParseAsync(string file) {
            return await System.Text.Json.JsonSerializer.DeserializeAsync<TConfig>(new StreamReader(file).BaseStream);
        }
        public static void Save(TConfig config ,string path) {
            using (var filestream = new StreamWriter(path, false) ) 
                filestream.Write(System.Text.Json.JsonSerializer.Serialize<TConfig>(config));
        }
        public static Task SaveAsync(TConfig config ,string path) {
            using (var filestream = new StreamWriter(path, false) ) 
               return System.Text.Json.JsonSerializer.SerializeAsync<TConfig>(filestream.BaseStream,config);
        }
#endregion

#region utf8json

    public static TConfig? Utf8Parse(string conf) {
            using (var filestream = new StreamReader(conf) ) 
            return Utf8Json.JsonSerializer.Deserialize<TConfig>(filestream.BaseStream);
        }
        public static async ValueTask<TConfig?> Utf8ParseAsync(string file) {
            using (var stream = new StreamReader(file))
            return await Utf8Json.JsonSerializer.DeserializeAsync<TConfig>(stream.BaseStream);
        }
        public static void Utf8Save(TConfig config ,string path) {
            using (var filestream = new StreamWriter(path, false) ) 
                filestream.Write(Utf8Json.JsonSerializer.Serialize<TConfig>(config));
        }
        public static Task Utf8SaveAsync(TConfig config ,string path) {
            using (var filestream = new StreamWriter(path, false) ) 
               return Utf8Json.JsonSerializer.SerializeAsync<TConfig>(filestream.BaseStream,config);
        }
#endregion
public void Dispose() {

}

    }
}