using ALauncher.Data;
using ALauncher.ViewModel;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
namespace ALauncher.View;

public partial class AddictionItems : Window, IDisposable {
    public AddictionItems(Item? item = null) {
        GetItem = item ?? new Item() { AppName = "Name", Path = "Item Path" };
        DataContext = new AddItemVM(GetItem);
        InitializeComponent();
    }
    public Item GetItem {get; private set;}
    public void AddItem(object sender, RoutedEventArgs e) {
        if (!Path.IsPathRooted(((AddItemVM)DataContext).GetItem.Path)) {
            MessageBox.Show("Path is not valid");
            return;
        }
        GetItem = ((AddItemVM)DataContext).GetItem;
        CloseDialog(true);
    }

    private void CloseDialog(bool result) {
        this.DialogResult = result;
        this.Close();
    }
     public void Close(object sender, RoutedEventArgs e) {
        CloseDialog(false);
    }
    public void Dispose() {GC.Collect();}

    public void MoveWindow(object sender, MouseEventArgs e) {
        if (Mouse.LeftButton == MouseButtonState.Pressed)
            Window.GetWindow(this).DragMove();
    }

}