using System.Runtime.Serialization;

namespace ALauncher.Data;

public class Item : IItem
{
    public string AppName { get ; set ; }
    public string Path { get ; set ; }
    [IgnoreDataMember]
    public IFormattable Icon {get;set;}
}