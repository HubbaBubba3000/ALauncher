using System.Collections.ObjectModel;
using System.Windows.Input;
using ALauncher.Data;
using ALauncher.Model;
using System.Linq;

namespace ALauncher.ViewModel;

public sealed class ControlPanelVM : BaseVM{
    private FolderManager folderManager;
    private Logger logger; 
    private WrapPanelVM wrapPanel;
    public CommandWrapper commandWrapper;
    private AddFolderService addFolderService;
    private bool IsAddWindowOpen;
    private SettingsService settingsService;
    public ObservableCollection<Folder> Folders {
        get {
            return folderManager.folders;
        }
        set {
            if (value != null) 
                folderManager.folders = value;
            OnPropertyChanged("Folders");
        }
    }
    public Folder CurrentFolder {
        get {
            return wrapPanel.CurrentFolder;
        }
        set {
            logger.TimerStart(value.Name);
            //folderManager.CheckAndSetIcons(value);
            GetIcons(value);
            wrapPanel.CurrentFolder = value;
            logger.TimerStop();
            OnPropertyChanged("CurrentFolder");
        }
    }
    public void GetIcons(Folder folder) {
        foreach(Item item in folder.Items)
            item.Icon = packManager.GetIcon(item);
    }

    public ICommand AddFolder {
        get {
            return commandWrapper.GetCommand((obj) => {
                if (IsAddWindowOpen) return;
                IsAddWindowOpen = true;
                if (addFolderService.Show() == true) {
                    Folders.Add(addFolderService.Result);
                }
                IsAddWindowOpen = false;
            });
        }
    }
    public ICommand OpenSettings {
        get {
            return commandWrapper.GetCommand((obj) => {
                packManager.SerializeIcons(Folders.ToArray());
                settingsService.Show();
            });
        }
    }
    public ICommand EditFolder {
        get {
            return commandWrapper.GetCommand((obj) => {
                if (IsAddWindowOpen) return;
                IsAddWindowOpen = true;
                if (addFolderService.Show(CurrentFolder) == true) {
                    int i = Folders.IndexOf(CurrentFolder);
                    Folders.RemoveAt(i);
                    Folders.Insert(i,addFolderService.Result);
                    CurrentFolder = Folders.ElementAt(i);
                }
                IsAddWindowOpen = false;
            });
        }
    } 
    public ICommand DeleteFolder {
        get {
            return commandWrapper.GetCommand((obj) => {
                var buf = CurrentFolder;
                CurrentFolder = Folders.First();
                Folders.Remove(buf);
                folderManager.UpdateFolders();
            });
        }
    }
    private void UpdateByStatus(LoggerCode code) {
        if (code == LoggerCode.FolderAsyncParseComplete) {
            Folders = null; // updating list
            packManager.DeserializeIconsAsync().ConfigureAwait(false);
            CurrentFolder = Folders[0]; 
        }
    }
    private IconPackManager packManager;
    public ControlPanelVM(Logger bp,WrapPanelVM wp,IconPackManager ipm, FolderManager b, SettingsService ss, AddFolderService afs, CommandWrapper cw) {
        logger = bp;
        IsAddWindowOpen = false;
        packManager = ipm;

        wrapPanel = wp;
        commandWrapper = cw;
        logger.StatusChanged += UpdateByStatus;
        folderManager = b; 
        settingsService = ss;
        addFolderService = afs;
        //CurrentFolder = Folders.Count() == 0 ? new Folder() : Folders[0];
    }
}