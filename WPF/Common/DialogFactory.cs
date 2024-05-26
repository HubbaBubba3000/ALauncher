using ALauncher.WPF.Controls.FolderDialog;
namespace ALauncher.WPF.Common;

public class DialogFactory 
{
    private bool IsShowDialog;
    public FolderDialog? CreateFolderDialog() 
    {
        if (IsShowDialog) 
            return null;

        var dialog = new FolderDialog();

        return dialog;
    }
}