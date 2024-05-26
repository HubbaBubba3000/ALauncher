using ALauncher.Abstractions.Entities;
using ALauncher.WPF.Controls.FolderPage;

namespace ALauncher.WPF.Common;

public class PageFactory 
{
    public FolderPage CreateFolderPage(IFolder folder) 
    {
        return new();
    }
}