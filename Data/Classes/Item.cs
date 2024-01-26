using System.Text.Json;
using System.Text.Json.Serialization;
namespace ALauncher.Data;

public class Item : IItem
{
   // [JsonProperty("Name")]
    public string AppName { get ; set ; }
    public string Path { get ; set ; }
    [JsonIgnore]
    public IFormattable Icon {get;set;}
}