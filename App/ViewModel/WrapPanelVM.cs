
using System.Windows.Input;
using System.Windows;
using ALauncher.Data;
using ALauncher.Model;
using System.Linq;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

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
    public ImageSource AddImage {
        get => new BitmapImage(
            new Uri("pack://application:,,,/ALauncher;component/Resources/add.png"));
    }
    public Brush Background {
        get {
            var bg = settings.GetSettings().BackgroundPath;

            if (string.IsNullOrEmpty(bg))
                return new SolidColorBrush(Color.FromRgb(4,41,58)); //04293A

            return new ImageBrush(new BitmapImage(new Uri(bg)));
        }
        set {
            OnPropertyChanged("Background");
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
    public ICommand CloseWindow {
        get => commandWrapper.GetCommand((obj) => {
            logger.SetStatusLog(LoggerCode.ProcessStarted, "Window Closed");
        });
    }
    private void OnSettingsChanged(object sender, EventArgs e) {
        Background = null;
    }
    private AddItemService addItemService;
    private CommandWrapper commandWrapper;
    private SettingsManager settings;
    private Logger logger;
    public WrapPanelVM(FolderManager b,SettingsManager sm, AddItemService ais, CommandWrapper cw, Logger bp) {
        folderManager = b;
        settings = sm;
        settings.SettingsChanged += OnSettingsChanged;
        logger = bp;
        commandWrapper = cw;
        addItemService = ais;
    }
}