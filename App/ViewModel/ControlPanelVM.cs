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
    private Base BaseModel;
    private WrapPanelVM wrapPanelVM;
    public ObservableCollection<Folder> Folders {
        get {
            return BaseModel.folders;
        }
        set {
            BaseModel.folders = value;
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
                if ((string)obj != CurrentFolder.Name) return;
                int i = Folders.IndexOf(CurrentFolder);
                Folders.Remove(CurrentFolder);
                CurrentFolder = Folders[i-1];
            });
        }
    }
    public ControlPanelVM(Base b, WrapPanelVM wp) {
        BaseModel = b;
        wrapPanelVM = wp;
        CurrentFolder = Folders[0];
    }
}