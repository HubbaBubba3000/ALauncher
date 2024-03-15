using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using ALauncher.Data;

namespace ALauncher.Core;

public sealed class ProcessWorker : IDisposable
{
    ProcessStartInfo processInfo;
    EventHandler ExitEvent;
    public string ProcessName
    {
        get => processInfo.FileName;
    }
    public void RunProcess()
    {
        try
        {
            using (var p = new Process())
            {
                p.StartInfo = processInfo;
                p.Exited += ExitEvent;
                p.Start();
            }
        }
        catch (Exception e)
        {
            MessageBox.Show($"error : {e.Message}");
        }
    }
    public void SetExitEvent(EventHandler handler)
    {
        ExitEvent = handler;
    }

    public void Dispose() {}

    public ProcessWorker(Item item)
    {
        processInfo = new()
        {
            WorkingDirectory = Path.GetDirectoryName(item.Path),
            FileName = item.Path
        };
    }

}