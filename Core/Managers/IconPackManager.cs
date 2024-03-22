using ALauncher.Data;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using MessagePack;
using System.IO;
using System.Buffers;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using System.Threading;

namespace ALauncher.Core;

public class IconPackManager : IManager
{
    private Dictionary<string, string> Icons; // item path, Icon(base64)
    private byte[] buffer;

    public void SerializeIcons(Folder folder)
    {
        foreach (var item in folder.Items)
        {
            if (Icons.ContainsKey(item.Path))
                Icons.Remove(item.Path);
            Icons.Add(item.Path, Base64ToImageConverter.ConvertBack((BitmapSource)item.Icon));
        }
        using (var stream = new StreamWriter(ManagerHelper.WorkFolder + "/Icons.bin").BaseStream)
            MessagePackSerializer.Serialize(stream, Icons);
    }
    public async Task SerializeIcons(FolderConfig config)
    {
        foreach (var folder in config.Folders)
            foreach (var item in folder.Items)
            {
                if (Icons.ContainsKey(item.Path) && item.Name != null)
                    Icons.Remove(item.Path);
                Icons.Add(item.Path, Base64ToImageConverter.ConvertBack((BitmapSource)item.Icon));
            }
        using (var stream = new StreamWriter(ManagerHelper.WorkFolder + "/Icons.bin").BaseStream)
            await MessagePackSerializer.SerializeAsync(stream, Icons);
    }
    public async Task DeserializeIconsAsync(string path)
    {
        using (var stream = new StreamReader(path).BaseStream)
        {
            Icons = await MessagePackSerializer.DeserializeAsync<Dictionary<string, string>>(stream);
            logger.SetStatusLog(LoggerCode.FolderAsyncParseComplete, "Icons was loaded");
        }
    }
    public ImageSource? GetIcon(Item item)
    {
        return Base64ToImageConverter.Convert(Icons.GetValueOrDefault(item.Path)) ?? IconExtractor.GetIcon(item.Path);
    }
    public IEnumerable<ImageSource?> GetIcons(Folder folder)
    {
        foreach (Item item in folder.Items)
            yield return Base64ToImageConverter.Convert(Icons.GetValueOrDefault(item.Path)) ?? IconExtractor.GetIcon(item.Path);
    }

    public void Save()
    {
        // SerializeIcons();
    }

    public void Load(string path)
    {
        DeserializeIconsAsync(path).ConfigureAwait(false);
    }

    private Logger logger;

    public IConfig GetConfig => throw new NotImplementedException();

    public IconPackManager(Logger logger)
    {
        this.logger = logger;
        buffer = Array.Empty<byte>();
        Icons = new Dictionary<string, string>();
        Load(ManagerHelper.WorkFolder + "/Icons.bin");
    }
}