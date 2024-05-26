using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using ALauncher.Entities.Containers;
using ALauncher.WPF.Common;
using Microsoft.Win32;

namespace ALauncher.WPF.Controls.ItemDialog;

public sealed class ItemDialogVM : BaseVM
{
    public Item GetItem { get; private set; }
    public ItemDialogVM(Item item)
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
    public string ItemParams
    {
        get => GetItem.Params ?? "";
        set
        {
            GetItem.Params = value;
            OnPropertyChanged("ItemParams");
        }
    }
    private ICommand _browse;
    public ICommand Browse
    {
        get
        {
            _browse ??= new RelayCommand((obj) =>
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