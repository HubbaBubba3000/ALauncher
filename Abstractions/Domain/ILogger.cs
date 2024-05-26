using System;
using System.Text;
namespace ALauncher.Abstractions.Domain;

public interface ILogger
{
    public StringBuilder Status { get; set; }

    public void SetStatusLog(int code, string status);
}