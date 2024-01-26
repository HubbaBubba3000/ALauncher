using System.Text.Json;

namespace ALauncher.Data;

public class Folder : IFolder
{
    public string Name { get ; set; }
    //[JsonProperty("Items")]
    public List<Item> Items { get; set;}

}