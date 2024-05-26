using System;
using System.Diagnostics;
using System.Text;
using ALauncher.Abstractions.Domain;

namespace ALauncher.Domain.Logging;
public class Logger
{
    private LoggerCode _code;
    public delegate void StatusChangedHandler(LoggerCode code);
    public event StatusChangedHandler StatusChanged;
    public Logger()
    {
        StatusChanged += p => { };
        _status = new();
    }


    public LoggerCode Code
    {
        get => _code;
        set
        {
            _code = value;
            StatusChanged?.Invoke(_code);
        }
    }
    private StringBuilder _status;
    public StringBuilder Status
    {
        get { return _status; }
        set
        {
            _status = value;
        }
    }
    public void SetStatusLog(LoggerCode code, string status)
    {
        Status = new(status);
        Code = code;
    }
    public void SetStatusLog(int code, string status)
    {
        Status = new(status);
        Code = (LoggerCode)code;
    }
}