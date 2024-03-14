using ALauncher.Data;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using MessagePack;
using System.IO;
using MessagePack.Resolvers;
using System.Buffers;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using System.Threading;

namespace ALauncher.Model;

public class IconPackManager : Manager {
    private Dictionary<string, string> Icons; // name, Icon(base64)
    private byte[] buffer;

    public static string ImageToBase64(BitmapSource bitmap) {
        if (bitmap == null) return string.Empty;
        var encoder = new PngBitmapEncoder();
        var frame = BitmapFrame.Create(bitmap);
        encoder.Frames.Add(frame);
        using(var stream = new MemoryStream()) {
            encoder.Save(stream);
            return Convert.ToBase64String(stream.ToArray());
        }
    }
    public static BitmapSource? Base64ToImage(string? base64) {
        if (string.IsNullOrEmpty(base64)) return null;
        byte[] bytes = Convert.FromBase64String(base64);
        using(var stream = new MemoryStream(bytes))
            return BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
    }
    
    public void SerializeIcons(Folder folder) {
        foreach (var item in folder.Items) {
            if (Icons.ContainsKey(item.AppName)) 
                Icons.Remove(item.AppName);
            Icons.Add(item.AppName, ImageToBase64((BitmapSource)item.Icon));
        }

        using (var stream = new StreamWriter(WorkFolder+"/Icons.bin").BaseStream)
            MessagePackSerializer.Serialize(stream, Icons);
    }
    public void SerializeIcons(Folder[] folders) {
        foreach (var folder in folders)
            foreach (var item in folder.Items) {
                if (Icons.ContainsKey(item.AppName)) 
                    Icons.Remove(item.AppName);
                Icons.Add(item.AppName, ImageToBase64((BitmapSource)item.Icon));
            }

        using (var stream = new StreamWriter(WorkFolder+"/Icons.bin").BaseStream)
            MessagePackSerializer.Serialize(stream, Icons);
    }
    public void DeserializeIcons() {
        var options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.None);

        buffer = File.ReadAllBytes((WorkFolder+"/Icons.bin"));
        Icons = MessagePackSerializer.Deserialize<Dictionary<string, string>>(buffer, options);
    }
    public async Task DeserializeIconsAsync() {
        var options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.None);
        
        using (var stream = new MessagePackStreamReader(new StreamReader(WorkFolder+"/Icons.bin").BaseStream)) {
            while (await stream.ReadAsync(CancellationToken.None) is ReadOnlySequence<byte> msgpack) {
                var kv = MessagePackSerializer.Deserialize<KeyValuePair<string, string>>(msgpack);
                Icons.Add(kv.Key, kv.Value);
            }
            logger.SetStatusLog(0, "Icons was loaded");
        }
    }
    public IFormattable? GetIcon(Item item) {
        return Base64ToImage(Icons.GetValueOrDefault(item.AppName)) ?? IconExtractor.GetIcon(item.Path);
    }
    public IEnumerable<IFormattable?> GetIcons(Folder folder) {
        foreach(Item item in folder.Items)
            yield return Base64ToImage(Icons.GetValueOrDefault(item.AppName)) ?? IconExtractor.GetIcon(item.Path);
    }
    private Logger logger;
    public IconPackManager(Logger logger) {
        this.logger = logger;
        buffer = Array.Empty<byte>();
        Icons = new Dictionary<string,string>();
    }
}