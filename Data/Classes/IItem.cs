
namespace ALauncher.Data;

public interface IItem {
    public string Text {get; set;} 

    public string Path {get; set;}

    /// <summary>
    /// Icon of exe file.
    /// ImageSource implement IFormattable
    /// </summary>
    public IFormattable Icon {get; set;}
}