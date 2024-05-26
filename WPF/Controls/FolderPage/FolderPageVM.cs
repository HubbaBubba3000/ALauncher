
using System.Windows.Input;
using ALauncher.Entities.Containers;
using ALauncher.Domain.Agregators;
using ALauncher.Domain.Logging;
using ALauncher.Domain.Processing;
using System.Linq;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ALauncher.WPF.Common;
using ALauncher.Domain.ConfigRepositories;

namespace ALauncher.WPF.Controls.FolderPage;

public sealed class FolderPageVM : BaseVM, IDisposable
{
    private CommandWrapper commandWrapper;
    private ConfigSaveRepository ConfigSaver;
    private ProcessRunService Process;
    private Logger logger;
    private StorageAgregator Storage;
    private bool IsAddWindowOpen;
    private Item? _item;
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
    private Folder? folder;
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
            var bg = Settings.BackgroundPath;

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
                //Folder folder = Storage.Folders.Single(f => f == CurrentFolder);
                return CurrentFolder.Items;
            }
            catch (Exception e)
            {
                return new();
            }
        }
        set
        {
            //Storage.Folders.Single(f => f == CurrentFolder).Items = value;
            CurrentFolder.Items = value;
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
                    ConfigSaver.Save(Storage, "Storage");
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
                ConfigSaver.Save(Storage, "Storage");

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
                    ConfigSaver.Save(Storage, "Storage");
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
                ConfigSaver.Save(Storage, "Storage");
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

                Process.RunProcess(item);
            });
        }
    }
    private void OnSettingsChanged()
    {
        Background = null;
    }

    public void Dispose()
    {
        ConfigSaver.Save(Storage, "Storage");
    }

    public FolderPage(
        AgregatorFactory af,
        ConfigSaveRepository csr,
        ProcessRunService prs, 
        CommandWrapper cw, 
        Logger bp)
    {   
        Storage = af.GetStorage();
        ConfigSaver = csr;
        Process = prs;
        logger = bp;
        commandWrapper = cw;
    }
}