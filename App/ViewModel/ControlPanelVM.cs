using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ALauncher.Data;
using ALauncher.Model;
using ALauncher.View;
using System.Linq;

namespace ALauncher.ViewModel;

public class ControlPanelVM : BaseVM{
    private FolderManager folderManager;
    private WrapPanelVM wrapPanelVM;
    private SettingsService settingsService;
    public ObservableCollection<Folder> Folders {
        get {
            return folderManager.folders;
        }
        set {
            folderManager.folders = value;
            OnPropertyChanged("Folders");
        }
    }
    public Folder CurrentFolder {
        get {
            return wrapPanelVM.CurrentFolder;
        }
        set {
            wrapPanelVM.CurrentFolder = value;
            OnPropertyChanged("CurrentFolder");
        }
    }
    public ICommand AddFolder {
        get {
            return new RelayCommand((obj) => {
                using (AddictionFolder af = new AddictionFolder()) {
                    af.Closing += new CancelEventHandler((obj, e) => {
                        if (!af.IsAdd) return;
                        Folders.Add(af.GetFolder);
                    });
                    af.Show();
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
    public ICommand DeleteFolder {
        get {
            return new RelayCommand((obj) => {
                var folder = Folders.Single(i => i.Name == (string)obj);
                if (CurrentFolder == folder) 
                    CurrentFolder = Folders.ElementAt(Folders.IndexOf(folder)-1);
                Folders.Remove(folder);
            });
        }
    }
    public ControlPanelVM(FolderManager b, WrapPanelVM wp, SettingsService ss) {
        folderManager = b;
        wrapPanelVM = wp;
        settingsService = ss;
        CurrentFolder = Folders.Count() == 0 ? new Folder() : Folders[0];
    }
}