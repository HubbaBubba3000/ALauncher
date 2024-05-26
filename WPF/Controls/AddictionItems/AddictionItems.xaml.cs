using ALauncher.Entities.Containers;
using ALauncher.WPF.Common;
using System;
using System.IO;
using System.Windows;
namespace ALauncher.WPF.Controls.ItemDialog;

public partial class ItemDialog : BaseWindow, IDisposable
{
    public ItemDialog(Item? item = null)
    {
        GetItem = item ?? new();
        DataContext = new ItemDialogVM(GetItem);
        InitializeComponent();
    }
    public Item GetItem { get; private set; }
    public void AddItem(object sender, RoutedEventArgs e)
    {
        if (!Path.IsPathRooted(((ItemDialogVM)DataContext).GetItem.Path))
        {
            MessageBox.Show("Path is not valid");
            return;
        }
        if (GetItem.Name.Length > 14)
        {
            MessageBox.Show("Name is too length, Name must contain less 14 characters");
            return;
        }
        GetItem = ((ItemDialogVM)DataContext).GetItem;
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
    public void Dispose() { GC.Collect(); }

}