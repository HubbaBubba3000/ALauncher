using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using ALauncher.Data;

namespace ALauncher.ViewModel;

public class WrapPanelVM : BaseVM {
    public Item SelectedItem { get;set; }
    public Folder CurrentFolder;
    public Item[] Items {
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