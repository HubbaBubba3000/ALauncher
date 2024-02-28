using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using ALauncher.Data;
using ALauncher.Model;
using Microsoft.Win32;

namespace ALauncher.ViewModel;

public class AddItemVM : BaseVM {
    public Item GetItem {get; private set;}
    public AddItemVM(Item item) {
        GetItem = item;
    }
    public string AppName {
        get => GetItem.AppName ?? "";
        set {
            GetItem.AppName = value;
            OnPropertyChanged("AppName");
        }
    }
    public string ItemPath {
        get => GetItem.Path ?? "";
        set {

            GetItem.Path = value;
            OnPropertyChanged("ItemPath");
        }
    }
    
    public ICommand Browse {
        get => new RelayCommand((obj) => {
            OpenFileDialog ofd = new() {
                Filter = "executable (.exe) file |*.exe",
                Multiselect = false
            };
            if (ofd.ShowDialog() == true) {
                ItemPath = ofd.FileName;
                AppName = Path.GetFileNameWithoutExtension(ofd.FileName.AsSpan()).ToString();
            }
        });
    }
}