using System;
using System.Collections.ObjectModel;
namespace ALauncher.Data;

public sealed class FolderConfig : IConfig, IDisposable {
    public FolderConfig() { }

    public ObservableCollection<Folder> Folders {get;set;} = new();
    public void Dispose() { }
}

