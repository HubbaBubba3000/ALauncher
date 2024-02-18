using System.Text.Json;
using System.IO;
using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace ALauncher.Model {
    public class JsonParser<TConfig> where TConfig : IConfig, IDisposable {

        public static TConfig? Parse(string conf) {
            using (var filestream = new StreamReader(conf) ) 
            return JsonSerializer.Deserialize<TConfig>(filestream.BaseStream);
        }
        public static async ValueTask<TConfig?> ParseAsync(string file) {
            return await JsonSerializer.DeserializeAsync<TConfig>(new StreamReader(file).BaseStream);
        }
        public static void Save(TConfig config ,string path) {
            using (var filestream = new StreamWriter(path, false) ) 
                filestream.Write(JsonSerializer.Serialize<TConfig>(config));
        }
        public static TConfig? TryParse (string conf) {
            try {
                using (var filestream = new StreamReader(conf) ) {
                    var json = JsonSerializer.Deserialize<TConfig>(filestream.BaseStream);
                    return json;
                }
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
                return default;
            }
        }
        public void Dispose() {

        }

    }
}