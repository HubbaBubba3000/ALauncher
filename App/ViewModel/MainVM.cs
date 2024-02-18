using ALauncher.Model;
using System.ComponentModel;
using System.Windows;

namespace ALauncher.ViewModel;

public class MainVM : BaseVM {
    public ControlPanelVM controlPanelVM {get ;set;}
    public WrapPanelVM wrapPanelVM {get ;set;}
    public BottomPanelVM bottomPanelVM {get;set;}
    public SettingsManager settings {get;set;}
    public MainVM(ControlPanelVM cp, WrapPanelVM wp, BottomPanelVM bp, SettingsManager sm) {
        wrapPanelVM = wp;
        controlPanelVM = cp;
        bottomPanelVM = bp;
        settings = sm;
    }
    public void LoadSettings(Window window) {
        settings.SetMainWindow(window);
        settings.SetWindowDefaultSize();
    }

    public void OnClosing(object sender, CancelEventArgs e) {
        settings.SaveWindowSize();
    }
}