
namespace ALauncher.ViewModel;

public class MainVM : BaseVM {
    public ControlPanelVM controlPanelVM {get ;set;}
    public WrapPanelVM wrapPanelVM {get ;set;}
    public BottomPanelVM bottomPanelVM {get;set;}
    public MainVM(ControlPanelVM cp, WrapPanelVM wp, BottomPanelVM bp) {
        wrapPanelVM = wp;
        controlPanelVM = cp;
        bottomPanelVM = bp;
    }
}