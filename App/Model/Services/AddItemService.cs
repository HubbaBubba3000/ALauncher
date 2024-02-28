using ALauncher.View;
using ALauncher.Data;

namespace ALauncher.Model;

public class AddItemService : IService
{
    public Item? Result {get; private set;}
    public bool Show()
    {
        var window = new AddictionItems();
        if (window.ShowDialog() == true) {
            Result = window.GetItem;
            return true;
        } else return false;
            
    }
    public bool Show(Item item)
    {
        var window = new AddictionItems(item);
        if (window.ShowDialog() == true) {
            Result = window.GetItem;
            return true;
        } else return false;
    }

}