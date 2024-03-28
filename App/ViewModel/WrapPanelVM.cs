
using System.Windows.Input;
using ALauncher.Data;
using ALauncher.Core;
using System.Linq;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ALauncher.View;

namespace ALauncher.ViewModel;

public sealed class WrapPanelVM : BaseVM, IDisposable
{
    private CommandWrapper commandWrapper;
    private SettingsManager settings;
    private Logger logger;
    private FolderManager folderManager;
    private bool IsAddWindowOpen;
    private Item _item;
    public Item CurrentItem
    {
        get
        {
            return _item;
        }
        set
        {
            if (value == null) return;
            _item = value;
        }
    }
    private Folder folder;
    public Folder CurrentFolder
    {
        get
        {
            return folder;
        }
        set
        {
            folder = value;
            if (folder != null)
                Items = folder.Items;
            OnPropertyChanged("CurrentFolder");
        }
    }
    public ImageSource AddImage
    {
        get => new BitmapImage(
            new Uri("pack://application:,,,/ALauncher;component/Resources/add.png"));
    }
    public Brush Background
    {
        get
        {
            var bg = settings.GetSettings().BackgroundPath;

            if (string.IsNullOrEmpty(bg))
                return new SolidColorBrush(Color.FromRgb(4, 41, 58)); //04293A

            return new ImageBrush(new BitmapImage(new Uri(bg)));
        }
        set
        {
            OnPropertyChanged("Background");
        }
    }

    public ObservableCollection<Item> Items
    {
        get
        {
            try
            {
                Folder folder = folderManager.Folders.Single(f => f == CurrentFolder);
                return folder.Items;
            }
            catch (Exception e)
            {
                return new();
            }
        }
        set
        {
            folderManager.Folders.Single(f => f == CurrentFolder).Items = value;
            OnPropertyChanged("Items");
        }
    }
    public ICommand AddItem
    {
        get
        {
            return commandWrapper.GetCommand((_obj) =>
            {
                if (IsAddWindowOpen) return;
                IsAddWindowOpen = true;
                var window = new AddictionItems();
                if (window.ShowDialog() == true)
                {
                    Items.Add(window.GetItem);
                    folderManager.Save();
                }
                IsAddWindowOpen = false;
            });
        }
    }
    public ICommand Starring 
    {
        get => 
            commandWrapper.GetCommand((obj) => 
            {
                CurrentItem.IsFavorite = !CurrentItem.IsFavorite;
                folderManager.Save();

            });
    }
    public ICommand EditItem
    {
        get
        {
            return commandWrapper.GetCommand((_obj) =>
            {
                if (IsAddWindowOpen) return;
                IsAddWindowOpen = true;
                var window = new AddictionItems(CurrentItem);

                if (window.ShowDialog() == true)
                {
                    int i = Items.IndexOf(CurrentItem);
                    Items.RemoveAt(i);
                    Items.Insert(i, window.GetItem);
                    CurrentItem = Items.ElementAt(i);
                    folderManager.Save();
                }
                IsAddWindowOpen = false;
            });
        }
    }
    public ICommand DeleteItem
    {
        get
        {
            return commandWrapper.GetCommand((obj) =>
            {
                var buf = CurrentItem;
                Items.Remove(buf);
                folderManager.Save();
            });
        }
    }
    public ICommand Run
    {
        get
        {
            return commandWrapper.GetCommand((obj) =>
            {
                var item = Items.Single(item => item.Path == (string)obj);
                ProcessWorker process = new(item);
                logger.SetStatusLog(LoggerCode.ProcessStarted, $"process {process.ProcessName} Started");
                process.SetExitEvent((_obj, e) =>
                {
                    logger.SetStatusLog(LoggerCode.ProcessClosed, $"process {process.ProcessName} Closed ");
                    process.Dispose();
                });
                process.RunProcess();
            });
        }
    }
    public ICommand CloseWindow
    {
        get => commandWrapper.GetCommand((obj) =>
        {
            logger.SetStatusLog(LoggerCode.ProcessStarted, "Window Closed");
        });
    }
    private void OnSettingsChanged()
    {
        Background = null;
    }

    public void Dispose()
    {
        folderManager.Save();
        settings.SettingsChanged -= OnSettingsChanged;
    }

    public WrapPanelVM(FolderManager b, SettingsManager sm, CommandWrapper cw, Logger bp)
    {
        folderManager = b;
        settings = sm;
        settings.SettingsChanged += OnSettingsChanged;
        logger = bp;
        commandWrapper = cw;
    }
}