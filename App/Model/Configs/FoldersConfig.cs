using System;
using System.Collections;
using ALauncher.Data;
namespace ALauncher.Model;

public class FolderConfig : IConfig, IDisposable {
    public Folder[] Folders {get;set;}
    public void Dispose() { }
}

