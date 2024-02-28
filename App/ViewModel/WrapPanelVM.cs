
using System.Windows.Input;
using System.Windows;
using ALauncher.Data;
using ALauncher.Model;
using System.Linq;
using System;
using System.Collections.ObjectModel;

namespace ALauncher.ViewModel;

public class WrapPanelVM : BaseVM {
    private ProcessWorker process;
    private bool IsAddWindowOpen;
    private Item _item;
    public Item CurrentItem {
        get {
            return _item;
        } 
        set {
            if (value == null) return;
            _item = value;
        }
    }
    private Folder folder;
    public Folder CurrentFolder {
        get {
            return folder;
        }
        set {
            folder = value;
            Items = folder.Items;
            OnPropertyChanged("CurrentFolder");
        }
    }
    WindowState _windowState;
    /// <summary>
    /// MainWindow State
    /// <summary>
    public WindowState windowState {
        get { return _windowState; }
        set { 
            _windowState = value; 
            OnPropertyChanged("windowState");
        }
    } 

    private FolderManager folderManager;
    //ObservableCollection<Item> items;
    public ObservableCollection<Item> Items {
        get {
            try {
                Folder folder = folderManager.folders.Single(f => f == CurrentFolder);
                return folder.Items;
            }
            catch (Exception e) {
                return new();
            }
        }
        set {
            folderManager.folders.Single(f => f == CurrentFolder).Items = value;
            OnPropertyChanged("Items");
        }
    }
    public ICommand AddItem {
        get {
            return new RelayCommand((_obj) => {
                if (IsAddWindowOpen) return;
                IsAddWindowOpen = true;
                if (addItemService.Show() == true) {
                    Items.Add(addItemService.Result);
                    folderManager.UpdateFolders();
                    IsAddWindowOpen = false;
                }
            });
        } 
    }
    public ICommand EditItem {
        get {
            return new RelayCommand((_obj) => {
                if (IsAddWindowOpen) return;
                IsAddWindowOpen = true;
                if (addItemService.Show(CurrentItem) == true) {
                    int i = Items.IndexOf(CurrentItem);
                    Items.RemoveAt(i);
                    Items.Insert(i,addItemService.Result);
                    folderManager.UpdateFolders();
                    IsAddWindowOpen = false;
                }
            });
        } 
    }
     public ICommand DeleteItem {
        get {
            return new RelayCommand((obj) => {
                    var buf = CurrentItem;
                    Items.Remove(buf);
                    folderManager.UpdateFolders();
            });
        }
    } 
    public ICommand Run {
        get {
            return new RelayCommand((obj) => {
                var item = Items.Single(item => item.Path == (string)obj);
                process = new(item);
                windowState = WindowState.Minimized;
                GC.Collect();
                process.RunProcess();
                process.SetExitEvent((_obj, e) => {
                    windowState = WindowState.Normal;

                });
            });
        }
    }
    private AddItemService addItemService;
    public WrapPanelVM(FolderManager b, AddItemService ais) {
        folderManager = b;
        addItemService = ais;
    }
}