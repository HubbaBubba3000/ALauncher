using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using ALauncher.Data;
namespace ALauncher.Model;

public sealed class ProcessWorker {
    ProcessStartInfo processInfo;
    EventHandler ExitEvent;
    public string ProcessName {
        get => processInfo.FileName;
    }
    public void RunProcess() {
        try {
            var p = Process.Start(processInfo);
            p.Exited += ExitEvent;
        }
        catch (Exception e){
            MessageBox.Show($"error : {e.Message}");
        }
    }
    public void SetExitEvent(EventHandler handler) {
        ExitEvent = handler;
    }

    public ProcessWorker(Item item) {
        processInfo = new() {
            WorkingDirectory = Path.GetDirectoryName(item.Path),
            FileName = item.Path
        };
    }

}