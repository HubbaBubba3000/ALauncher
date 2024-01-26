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
            return new(BaseModel.folders);
        }
        set {
            BaseModel.folders = value.ToList();
            OnPropertyChanged("Folders");
        }
    }
    public Folder CurrentFolder {
        get {
            return wrapPanelVM.CurrentFolder;
        }
        set {
            wrapPanelVM.CurrentFolder = value;
            wrapPanelVM.Items = new(value.Items);
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
    public ControlPanelVM(Base b, WrapPanelVM wp) {
        BaseModel = b;
        wrapPanelVM = wp;

    }
}