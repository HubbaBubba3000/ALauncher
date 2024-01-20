using ALauncher.Data;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
namespace ALauncher.View;

public partial class AddictionWindow : Window {

    public Folder GetFolder;
    public AddictionWindow() {
        InitializeComponent();
    }
    public void AddFolder(object sender, RoutedEventArgs e) {
        GetFolder = new Folder() { Name = NameBox.Text };
        this.Close();
    }
     public void Close(object sender, RoutedEventArgs e) {
        GetFolder = null;
        this.Close();
    }

}