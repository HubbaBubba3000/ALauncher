using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using ALauncher.Abstractions.Entities;

namespace ALauncher.Entities.Containers;

public sealed class Folder : IFolder, ISearchable
{
    public string Name { get ; set; } = "New Folder";
    public ObservableCollection<IItem> Items { get; set;} = new();
    
    [IgnoreDataMember]
    public string type {get;} = "Folder";

    [IgnoreDataMember]
    public IItem this[int i] {
        get {
            return Items[i];
        }
        set {
            Items[i] = value;
        }
    }

    public Folder() { }

}