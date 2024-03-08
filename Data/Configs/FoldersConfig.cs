using System;
using System.Collections;
namespace ALauncher.Data;

public sealed class FolderConfig : IConfig, IDisposable {
    public Folder[] Folders {get;set;}
    public void Dispose() { }
}

