using System;
using ALauncher.Data;
using ALauncher.View;
namespace ALauncher.Model;
public sealed class AddFolderService : IService
{
    public Folder? Result {get; private set;}
    public bool Show()
    {
        AddictionFolder window = new(null);
        if (window.ShowDialog() == true) {
            Result = window.GetFolder;
            return true;
        } else return false;
    }
    
    public bool Show(Folder EditFolder)
    {
        AddictionFolder window = new(EditFolder);
        if (window.ShowDialog() == true) {
            Result = window.GetFolder;
            return true;
        } else return false;
    }
}