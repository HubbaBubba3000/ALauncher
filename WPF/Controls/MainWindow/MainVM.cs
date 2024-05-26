using ALauncher.WPF.Common;
using ALauncher.WPF.Controls.SidePanel;
using ALauncher.WPF.Controls.BottomPanel;

namespace ALauncher.WPF.Controls.MainWindow;
public sealed class MainVM : BaseVM
{
    public SidePanelVM SPVM { get; set; }
    public BottomPanelVM BPVM { get; set; }
    public MainVM(SidePanelVM cp, BottomPanelVM bp)
    {
        SPVM = cp;
        BPVM = bp;
    }

}