using System;
using System.Diagnostics;
using System.Text;

namespace ALauncher.Core;
public class Logger {
    private LoggerCode _code;
    private Stopwatch timer;
    public delegate void StatusChangedHandler(LoggerCode code);
    public event StatusChangedHandler StatusChanged;
    public Logger() {
        timer = new Stopwatch();
        StatusChanged += p => {};
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
     public StringBuilder Status {
        get {return _status;} 
        set {
            _status = value;
        }
    }
    public void SetStatusLog(LoggerCode code, string status) {
        Status = new(status);
        Code = code;
    }
    public void SetStatusLog(int code, string status) {
        Status = new(status);
        Code = (LoggerCode)code;
    }
}