using System.Diagnostics;
using System;
using System.Threading;
using System.IO;

namespace ALauncher;

public static class Program 
{
    [STAThread]
    public static void Main()
    {
        var mutex = new Mutex(true, "ALauncher");
        if (mutex.WaitOne(0, false)) 
        {
            try 
            {
                var app = new App();
                app.InitializeComponent();
                app.Run();
            }
            catch (Exception e)
            {
                var fso = new FileStreamOptions() 
                {
                    Access = FileAccess.Write,
                    Mode = FileMode.OpenOrCreate
                };
                using (var stream = new StreamWriter("Error.log",fso) ) 
                {
                    stream.WriteLine(e.Message);
                    stream.WriteLine(e.Source);
                    Process.Start("explorer.exe", "Error.log");
                }
            }
        }
        
    }
}