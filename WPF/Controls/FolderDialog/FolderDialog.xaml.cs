using ALauncher.Entities.Containers;
using System;
using System.Windows;
using ALauncher.WPF.Common;

namespace ALauncher.WPF.Controls.FolderDialog;
public partial class FolderDialog : BaseWindow, IDisposable
{
    public FolderDialog(Folder? folder = null)
    {
        DataContext = new FolderDialogVM(folder);
        InitializeComponent();
    }
    public Folder? GetFolder { get; private set; }
    public void AddFolder(object sender, RoutedEventArgs e)
    {
        GetFolder = ((FolderDialogVM)DataContext).GetFolder;
        CloseDialog(true);
    }

    private void CloseDialog(bool result)
    {
        this.DialogResult = result;
        this.Close();
    }
    public void Close(object sender, RoutedEventArgs e)
    {
        CloseDialog(false);
    }

    public void Dispose()
    {
        GetFolder = null;
    }

}