using System;
using ALauncher.Data;
using ALauncher.Model;

namespace ALauncher.ViewModel;

public class ControlPanelVM : BaseVM{
    private Base BaseModel;
    private WrapPanelVM wrapPanelVM;
    public Folder[] folders {
        get {
            return BaseModel.folders;
        }
        set {
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
    public ControlPanelVM(Base b, WrapPanelVM wp) {
        BaseModel = b;
        wrapPanelVM = wp;
        CurrentFolder = folders[0];
    }
}