using ALauncher.Model;
using ALauncher.Core;
using System.Windows;

namespace ALauncher.ViewModel;

public sealed class MainVM : BaseVM {
    public ControlPanelVM controlPanelVM {get ;set;}
    public WrapPanelVM wrapPanelVM {get ;set;}
    public BottomPanelVM bottomPanelVM {get;set;}
    public SettingsManager settings {get;set;}
    public int ControlPanelWidth ;
    public MainVM(ControlPanelVM cp, WrapPanelVM wp, BottomPanelVM bp, SettingsManager sm) {
        wrapPanelVM = wp;
        controlPanelVM = cp;
        bottomPanelVM = bp;
        settings = sm;
    }
    public void InitSettings(Window win) {
        settings.SetMainWindow(win);
        settings.SetWindowSize();
        ControlPanelWidth = settings.GetSettings().ControlPanelWidth;
    }

    public void OnClosing() {
        settings.SetPanelWidth(ControlPanelWidth);
        settings.SaveWindowSize();
        
        settings.Save();
    }
}