
namespace ALauncher.Data;

public class Item : IItem
{

    public string Name { get ; set ; }
    public string Path { get ; set ; }

    public Item(string path, string name) {
        Path= path;
        Name=name;

    }
}