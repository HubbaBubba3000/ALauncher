using ALauncher.Data;
using ALauncher.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;
namespace ALauncher.View;

public partial class AddictionFolder : Window, IDisposable {
    public AddictionFolder(Folder? folder = null) {
        DataContext = new AddFolderVM(folder);
        InitializeComponent();
    }
    public Folder? GetFolder {get; private set;}
    public void AddFolder(object sender, RoutedEventArgs e) {
        GetFolder = ((AddFolderVM)DataContext).GetFolder;
        CloseDialog(true);
    }

    private void CloseDialog(bool result) {
        this.DialogResult = result;
        this.Close();
    }
     public void Close(object sender, RoutedEventArgs e) {
        CloseDialog(false);
    }

    public void Dispose()  { 
        GetFolder = null;
    }

    public void MoveWindow(object sender, MouseEventArgs e) {
        if (Mouse.LeftButton == MouseButtonState.Pressed)
            Window.GetWindow(this).DragMove();
    }

}