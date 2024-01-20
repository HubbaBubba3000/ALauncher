using System.Text.Json;
using System.IO;
using System;

namespace ALauncher.Model {
    public class JsonParser<TConfig> where TConfig : IConfig, IDisposable {

        private static string jsonread(string file) {
            using (var filestream = new StreamReader(file) ) 
                return filestream.ReadToEnd();
        }
        public static TConfig? Parse(string conf) {
            return JsonSerializer.Deserialize<TConfig>(jsonread(conf));
        }
        public static TConfig? TryParse (string conf) {
            try {
                var json = JsonSerializer.Deserialize<TConfig>(jsonread(conf));
                return json;
            }
            catch (Exception e) {
                throw;
                
            }
        }
        public void Dispose() {

        }

    }
}