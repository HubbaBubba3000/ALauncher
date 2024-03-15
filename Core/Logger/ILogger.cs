using System;
using System.Text;
namespace ALauncher.Core;

public interface ILogger
{
    public StringBuilder Status { get; set; }

    public void SetStatusLog(LoggerCode code, string status);
}