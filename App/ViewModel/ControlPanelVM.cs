using System.Collections.ObjectModel;
using System.Windows.Input;
using ALauncher.Data;
using ALauncher.Core;
using ALauncher.Model;
using System.Linq;

namespace ALauncher.ViewModel;

public sealed class ControlPanelVM : BaseVM
{
    private FolderManager folderManager;
    private Logger logger;
    private IconPackManager packManager;
    private WrapPanelVM wrapPanel;
    public CommandWrapper commandWrapper;
    private AddictionFolderFactory AddictionFolder;
    private bool IsAddWindowOpen;
    private SettingsFactory Settings;
    public ObservableCollection<Folder> Folders
    {
        get => folderManager.Folders;
        set
        {
            OnPropertyChanged("Folders");
        }
    }
    public Folder CurrentFolder
    {
        get => wrapPanel.CurrentFolder;
        set
        {
            GetIcons(value);
            wrapPanel.CurrentFolder = value;
            OnPropertyChanged("CurrentFolder");
        }
    }
    public void GetIcons(Folder folder)
    {
        foreach (Item item in folder.Items)
            item.Icon ??= packManager.GetIcon(item);
    }

    public ICommand AddFolder
    {
        get
        {
            return commandWrapper.GetCommand((obj) =>
            {
                if (IsAddWindowOpen) return;
                IsAddWindowOpen = true;
                if (AddictionFolder.Show() == true)
                {
                    Folders.Add(AddictionFolder.Result);
                }
                IsAddWindowOpen = false;
            });
        }
    }
    public ICommand OpenSettings
    {
        get
        {
            return commandWrapper.GetCommand((obj) =>
            {
                packManager.SerializeIcons((FolderConfig)folderManager.GetConfig).ConfigureAwait(false);
                Settings.Show();
            });
        }
    }
    public ICommand EditFolder
    {
        get
        {
            return commandWrapper.GetCommand((obj) =>
            {
                if (IsAddWindowOpen) return;
                IsAddWindowOpen = true;
                if (AddictionFolder.Show(CurrentFolder) == true)
                {
                    int i = Folders.IndexOf(CurrentFolder);
                    Folders.RemoveAt(i);
                    Folders.Insert(i, AddictionFolder.Result);
                    CurrentFolder = Folders.ElementAt(i);
                }
                IsAddWindowOpen = false;
            });
        }
    }
    public ICommand DeleteFolder
    {
        get
        {
            return commandWrapper.GetCommand((obj) =>
            {
                var buf = CurrentFolder;
                CurrentFolder = Folders.First();
                Folders.Remove(buf);
                folderManager.Save();
            });
        }
    }
    private void UpdateByStatus(LoggerCode code)
    {
        if (code == LoggerCode.FolderAsyncParseComplete)
        {
            Folders = null; // updating list
            CurrentFolder = Folders[0];
        }
    }
    public ControlPanelVM(Logger bp, WrapPanelVM wp, IconPackManager ipm, FolderManager b, SettingsFactory ss, AddictionFolderFactory afs, CommandWrapper cw)
    {
        logger = bp;
        packManager = ipm;
        wrapPanel = wp;
        commandWrapper = cw;
        folderManager = b;
        Settings = ss;
        AddictionFolder = afs;

        IsAddWindowOpen = false;
        logger.StatusChanged += UpdateByStatus;
    }
}