using System.Diagnostics;
using System;
using System.Threading;

namespace ALauncher;

public static class Program 
{
    [STAThread]
    public static void Main()
    {
        var mutex = new Mutex(true, "ALauncher");
        if (mutex.WaitOne(0, false)) 
        {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }
        
    }
}