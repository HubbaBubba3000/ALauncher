
namespace ALauncher.Data;

public class Item : IItem
{

    public string Text { get ; set ; }
    public string Path { get ; set ; }

    public IFormattable Icon {get;set;}
}