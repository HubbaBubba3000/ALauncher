

using System.Collections.ObjectModel;

namespace ALauncher.Data;

public interface IFolder {
    public string Name {get;set;}

    public ObservableCollection<Item> Items {get;set;}

}