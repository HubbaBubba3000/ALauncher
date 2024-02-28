using System;
using System.Collections;
namespace ALauncher.Data;

public class FolderConfig : IConfig, IDisposable {
    public Folder[] Folders {get;set;}
    public void Dispose() { }
}

