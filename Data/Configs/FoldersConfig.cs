using System;
namespace ALauncher.Data;

public struct FolderConfig : IConfig, IDisposable {
    public Folder[] Folders {get;set;}
    public void Dispose() { }
}

