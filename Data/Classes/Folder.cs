

namespace ALauncher.Data;

public class Folder : IFolder
{
    public string Name { get ; set; }
    public List<IItem> Items { get; set;}

    public Folder(string name) {
        Name = name;
        Items = new List<IItem>(2)
        {
            new Item(@"D:\coropa\coropata.exe", "Coropata"),
            new Item(@"D:\Cuphead\Cuphead.exe", "Cuphead")

        };
    }
    // public void Add(Item item) {
    //     Items.Add(item);
    // }
}