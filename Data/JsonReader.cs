using System.Text.Json;
using System.IO;
using System.Collections.Generic;

namespace ALauncher.Data {
    
    struct JsonReader {
        public static List<Folder>? Readfolders(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open)) 
            {
                return JsonSerializer.Deserialize<List<Folder>>(fs) ;
                
            }
        }
    }
}