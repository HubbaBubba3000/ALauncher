
using System.Windows.Input;
using System.Windows;
using ALauncher.Data;
using ALauncher.Model;
using System.Linq;
using System;
using System.Collections.ObjectModel;

namespace ALauncher.ViewModel;

public sealed class WrapPanelVM : BaseVM {
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
            if (folder != null)
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
            return commandWrapper.GetCommand((_obj) => {
                if (IsAddWindowOpen) return;
                IsAddWindowOpen = true;
                if (addItemService.Show() == true) {
                    Items.Add(addItemService.Result);
                    folderManager.UpdateFolders();
                }
                IsAddWindowOpen = false;
            });
        } 
    }
    public ICommand EditItem {
        get {
            return commandWrapper.GetCommand((_obj) => {
                if (IsAddWindowOpen) return;
                IsAddWindowOpen = true;
                if (addItemService.Show(CurrentItem) == true) {
                    int i = Items.IndexOf(CurrentItem);
                    Items.RemoveAt(i);
                    Items.Insert(i,addItemService.Result);
                    CurrentItem = Items.ElementAt(i);
                    folderManager.UpdateFolders();
                }
                IsAddWindowOpen = false;
            });
        } 
    }
     public ICommand DeleteItem {
        get {
            return commandWrapper.GetCommand((obj) => {
                    var buf = CurrentItem;
                    Items.Remove(buf);
                    folderManager.UpdateFolders();
            });
        }
    } 
    public ICommand Run {
        get {
            return commandWrapper.GetCommand((obj) => {
                var item = Items.Single(item => item.Path == (string)obj);
                process = new(item);
                logger.SetStatusLog(LoggerCode.ProcessStarted, $"process {process.ProcessName} Started");
                process.RunProcess();
                process.SetExitEvent((_obj, e) => {
                    logger.SetStatusLog(LoggerCode.ProcessClosed, $"process {process.ProcessName} Closed ");
                });
            });
        }
    }
    private AddItemService addItemService;
    private CommandWrapper commandWrapper;
    private Logger logger;
    public WrapPanelVM(FolderManager b, AddItemService ais, CommandWrapper cw, Logger bp) {
        folderManager = b;
        logger = bp;
        commandWrapper = cw;
        addItemService = ais;
    }
}