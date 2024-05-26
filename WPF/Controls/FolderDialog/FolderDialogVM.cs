using ALauncher.WPF.Common;
using ALauncher.Entities.Containers;

namespace ALauncher.WPF.Controls.FolderDialog;

public sealed class FolderDialogVM : BaseVM
{
    public Folder GetFolder { get; private set; }
    public string FolderName
    {
        get => GetFolder.Name;
        set
        {
            GetFolder.Name = value;
            OnPropertyChanged("FolderName");
        }
    }
    public FolderDialogVM(Folder? folder)
    {
        this.GetFolder = folder ?? new();
    }
}