using System;
using System.Diagnostics;
using System.Text;
using ALauncher.ViewModel;

namespace ALauncher.Model;
public enum LoggerCode {
    FolderAsyncParseComplete = 201,
    FolderAsyncParseFailed = 401,
    ProcessStarted = 102,
    ProcessClosed = 202,
    TimerStart = 110,
    TimerStop = 210
}
public class Logger {
    private LoggerCode _code;
    private Stopwatch timer;
    public Logger() {
        timer = new Stopwatch();
        _status = new();
    }
    public void TimerStart(string method) {
        timer.Start();
        SetStatusLog(210,method);
    }
    public void TimerStop() {
        timer.Stop();
        SetStatusLog(210,_status.Append($" loaded for {timer.ElapsedMilliseconds} ms").ToString()) ;
        timer.Reset();
    }

    public LoggerCode Code {
        get => _code;
        set {
            _code = value;
            StatusChanged?.Invoke(_code);
        }
    }
    private StringBuilder _status;
     public string Status {
        get {return _status.ToString();} 
        set {
            _status = new StringBuilder(value);
        }
    }
    public delegate void StatusChangedHandler(LoggerCode code);
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