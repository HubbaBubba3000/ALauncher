using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace ALauncher.Data;

public sealed class Folder : IFolder, ISearchable
{
    public string Name { get ; set; } = "New Folder";
    public ObservableCollection<Item> Items { get; set;} = new();
    
    [IgnoreDataMember]
    public string type {get;} = "Folder";

    [IgnoreDataMember]
    public Item this[int i] {
        get {
            return Items[i];
        }
        set {
            Items[i] = value;
        }
    }

    public Folder() { }

}