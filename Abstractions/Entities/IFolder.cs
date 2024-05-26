

using System.Collections.ObjectModel;

namespace ALauncher.Abstractions.Entities;

public interface IFolder {
    public string Name {get;set;}

    public ObservableCollection<IItem> Items {get;set;}

}