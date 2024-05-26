using System.Collections.ObjectModel;
using ALauncher.Abstractions.Domain;
using ALauncher.Abstractions.Entities;

namespace ALauncher.Domain.Agregators;

public class StorageAgregator : IAgregator 
{
    public StorageAgregator() { }
    public ObservableCollection<IFolder> Folders {get;set;} = new();
    
}