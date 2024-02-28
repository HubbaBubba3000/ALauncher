
namespace ALauncher.ViewModel;

public class BottomPanelVM : BaseVM {
    private string _status;
    public delegate void StatusChangedHandler(string status);
    public event StatusChangedHandler StatusChanged;
    public string Version {
        get {
            string ver ="v0.1.1 - ";
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
            if(StatusChanged != null)
                StatusChanged.Invoke(_status);
            OnPropertyChanged("Status");
        }
    }
    public BottomPanelVM() {
        _status = "status : ok";
    }
}