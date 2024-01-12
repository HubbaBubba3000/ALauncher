
namespace ALauncher.ViewModel;

public class MainVM : BaseVM {
    public ControlPanelVM controlPanelVM {get ;set;}
    public WrapPanelVM wrapPanelVM {get ;set;}
    public MainVM(ControlPanelVM cp, WrapPanelVM wp) {
        wrapPanelVM = wp;
        controlPanelVM = cp;
    }
}