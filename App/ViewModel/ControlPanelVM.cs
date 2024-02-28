using System.Collections.ObjectModel;
using System.Windows.Input;
using ALauncher.Data;
using ALauncher.Model;
using System.Linq;

namespace ALauncher.ViewModel;

public class ControlPanelVM : BaseVM{
    private FolderManager folderManager;
    private BottomPanelVM Logger; 
    private WrapPanelVM wrapPanel;
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
            folderManager.CheckAndSetIcons(value);
            wrapPanel.CurrentFolder = value;
            OnPropertyChanged("CurrentFolder");
        }
    }

    public ICommand AddFolder {
        get {
            return new RelayCommand((obj) => {
                if (IsAddWindowOpen) return;
                IsAddWindowOpen = true;
                if (addFolderService.Show() == true) {
                    Folders.Add(addFolderService.Result);
                    IsAddWindowOpen = false;
                }
                
            });
        }
    }
    public ICommand OpenSettings {
        get {
            return new RelayCommand((obj) => {
                settingsService.Show();
            });
        }
    }
    public ICommand EditFolder {
        get {
            return new RelayCommand((obj) => {
                if (IsAddWindowOpen) return;
                IsAddWindowOpen = true;
                if (addFolderService.Show(CurrentFolder) == true) {
                    int i = Folders.IndexOf(CurrentFolder);
                    Folders.RemoveAt(i);
                    Folders.Insert(i,addFolderService.Result);

                    IsAddWindowOpen = false;
                }
                
            });
        }
    } 
    public ICommand DeleteFolder {
        get {
            return new RelayCommand((obj) => {
                var buf = CurrentFolder;
                CurrentFolder = Folders.First();
                Folders.Remove(buf);
                folderManager.UpdateFolders();
            });
        }
    }
    private void UpdateByStatus(string status) {
        if (status == "Async parsing complete") {
            Folders = null; // updating list
            CurrentFolder = Folders[0]; 
        }
    }
    public ControlPanelVM(BottomPanelVM bp,WrapPanelVM wp, FolderManager b, SettingsService ss, AddFolderService afs) {
        Logger = bp;
        IsAddWindowOpen = false;
        wrapPanel = wp;
        Logger.StatusChanged += UpdateByStatus;
        folderManager = b; 
        settingsService = ss;
        addFolderService = afs;
        //CurrentFolder = Folders.Count() == 0 ? new Folder() : Folders[0];
    }
}