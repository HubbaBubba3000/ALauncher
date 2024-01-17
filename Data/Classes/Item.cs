
namespace ALauncher.Data;

public class Item : IItem
{

    public string AppName { get ; set ; }
    public string Path { get ; set ; }

    public IFormattable Icon {get;set;}
}