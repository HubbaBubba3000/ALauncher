using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Input;
using ALauncher.Data;
using ALauncher.Model;
using ALauncher.View;
using System.Collections.ObjectModel;
using System.Linq;

namespace ALauncher.ViewModel;

public class ControlPanelVM : BaseVM{
    private FolderManager folderManager;
    private WrapPanelVM wrapPanelVM;
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
    public ControlPanelVM(FolderManager b, WrapPanelVM wp) {
        folderManager = b;
        wrapPanelVM = wp;
        CurrentFolder = Folders[0];
    }
}