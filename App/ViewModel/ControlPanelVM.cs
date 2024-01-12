using System;
using ALauncher.Data;

namespace ALauncher.ViewModel;

public class ControlPanelVM : BaseVM{
    public Folder[] folders {get {
        return new[] {
            new Folder("folder1"),
            new Folder("folder2"),
            new Folder("folder3"),
            new Folder("folder4"),
            new Folder("folder5")
        }; } 
    }
    public ControlPanelVM(WrapPanelVM wp) {
        wp.CurrentFolder = folders[1];
    }
}