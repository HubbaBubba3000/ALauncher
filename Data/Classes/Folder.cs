using System.Collections.ObjectModel;

namespace ALauncher.Data;

public sealed class Folder : IFolder
{
    public string Name { get ; set; }
    public ObservableCollection<Item> Items { get; set;}

}