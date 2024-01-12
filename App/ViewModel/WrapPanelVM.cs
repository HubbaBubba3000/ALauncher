using System;
using System.Collections.Generic;
using ALauncher.Data;

namespace ALauncher.ViewModel;

public class WrapPanelVM : BaseVM {
    public Folder CurrentFolder;
    public List<IItem> Items {
        get {
            return CurrentFolder.Items;
        }
        set {
            OnPropertyChanged("Items");
        }
    }
    public WrapPanelVM() {

    }
}