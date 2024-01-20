using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Input;
using ALauncher.Data;
using ALauncher.Model;
using ALauncher.View;
using System.Collections.ObjectModel;

namespace ALauncher.ViewModel;

public class ControlPanelVM : BaseVM{
    private Base BaseModel;
    private WrapPanelVM wrapPanelVM;
    private ObservableCollection<Folder> folders;
    public ObservableCollection<Folder> Folders {
        get {
            return folders;
        }
        set {
            folders = value;
            OnPropertyChanged("Folders");
        }
    }
    public Folder CurrentFolder {
        set {
            wrapPanelVM.CurrentFolder = value;
            wrapPanelVM.Items = value.Items;
            OnPropertyChanged("CurrentFolder");
        }
    }
    public ICommand AddFolder {
        get {
            return new RelayCommand((obj) => {
                AddictionWindow af = new AddictionWindow();
                af.Closing += new CancelEventHandler((obj, e) => {Folders.Add(af.GetFolder);});
                af.Show();
            });
        }
    }
    public ControlPanelVM(Base b, WrapPanelVM wp) {
        BaseModel = b;
        wrapPanelVM = wp;
        Folders = new(BaseModel.folders);
        CurrentFolder = Folders[0];
    }
}