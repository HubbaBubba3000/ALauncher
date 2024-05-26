
using System.Diagnostics;
using System.IO;
using System.Windows;
using ALauncher.Abstractions.Entities;
using ALauncher.Domain.Logging;

namespace ALauncher.Domain.Processing;

public sealed class ProcessRunService
{
    private Logger logger;
    public ProcessRunService(Logger log) 
    {
        logger = log;
    }
    public void RunProcess(IItem item) 
    {   
        ProcessStartInfo processInfo = new()
        {
            WorkingDirectory = Path.GetDirectoryName(item.Path),
            FileName = item.Path,
            Arguments = item.Params
        };
        try
        {
            using (var p = new Process())
            {
                p.StartInfo = processInfo;
                p.Start();
            }
        }
        catch (Exception e)
        {
            logger.SetStatusLog(LoggerCode.Error,$"error : {e.Message}");
        }
    }

}