using System.Runtime.Serialization;
using System.Windows.Media;

namespace ALauncher.Data;

public sealed class Item : IItem, ISearchable
{
    public string Name { get ; set ; } = "New Item";
    public string Path { get ; set ; } = string.Empty;
    public string Params {get;set;} = string.Empty;
    public bool IsFavorite {get;set;} = false;
    [IgnoreDataMember]
    public string type {get;} = "Item";
    [IgnoreDataMember]
    public ImageSource? Icon {get;set;}

    public Item() {  }
}