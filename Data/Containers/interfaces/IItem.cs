
using System.Windows.Media;

namespace ALauncher.Data;

public interface IItem {
    public string Name {get; set;} 

    public string Path {get; set;}

    public string Params {get;set;}
    /// <summary>
    /// Icon of exe file.
    /// ImageSource implement IFormattable
    /// </summary>
    public ImageSource Icon {get; set;}

}