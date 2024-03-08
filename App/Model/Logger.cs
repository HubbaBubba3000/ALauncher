
using ALauncher.ViewModel;

namespace ALauncher.Model;
public enum LoggerCode {
    FolderAsyncParseComplete = 201,
    FolderAsyncParseFailed = 401,
    ProcessStarted = 102,
    ProcessClosed = 202
}
public class Logger : BaseVM {
    private LoggerCode _code;
    public LoggerCode Code {
        get => _code;
        set {
            _code = value;
            OnPropertyChanged();
            StatusChanged?.Invoke(_code);
        }
    }
    private string _status;
     public string Status {
        get {return _status;} 
        set {
            _status = value;
            OnPropertyChanged("Status");
        }
    }
    public delegate void StatusChangedHandler(LoggerCode code);
    public delegate void PropertyChangedHandler();
    public event StatusChangedHandler StatusChanged;
    public void SetStatusLog(LoggerCode code, string status) {
        Status = status;
        Code = code;
    }
    public void SetStatusLog(int code, string status) {
        Status = status;
        Code = (LoggerCode)code;
    }
}