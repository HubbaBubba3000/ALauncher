using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using ALauncher.Data;
using ALauncher.Core;
using Microsoft.Win32;

namespace ALauncher.ViewModel;

public sealed class AddItemVM : BaseVM
{
    public Item GetItem { get; private set; }
    public AddItemVM(Item item)
    {
        GetItem = item;
    }
    public string AppName
    {
        get => GetItem.Name ?? "";
        set
        {
            GetItem.Name = value;
            OnPropertyChanged("AppName");
        }
    }
    public string ItemPath
    {
        get => GetItem.Path ?? "";
        set
        {
            GetItem.Path = value;
            if (Path.IsPathRooted(value))
                GetItem.Icon = IconExtractor.GetIcon(value);
            OnPropertyChanged("ItemPath");
        }
    }
    private ICommand _browse;
    public ICommand Browse
    {
        get
        {
            if (_browse == null)
                _browse = new RelayCommand((obj) =>
                {
                    OpenFileDialog ofd = new()
                    {
                        Filter = "executable (.exe) file |*.exe",
                        Multiselect = false
                    };
                    if (ofd.ShowDialog() == true)
                    {
                        ItemPath = ofd.FileName;
                        AppName = Path.GetFileNameWithoutExtension(ofd.FileName.AsSpan()).ToString();
                    }
                });
            return _browse;
        }
    }
}