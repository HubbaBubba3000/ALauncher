
namespace ALauncher.ViewModel;

public class BottomPanelVM : BaseVM {
    private string _status;
    public string Version {
        get {
            string ver ="v0.0.5 - ";
            #if DEBUG
                ver += "DEBUG";
            #else 
                ver += "Release";
            #endif
            return ver;
        }
    }
    public string Status {
        get {return _status;} 
        set {
            _status = value;
            OnPropertyChanged("Status");
        }
    }
    public BottomPanelVM() {
        _status = string.Empty;
    }
}