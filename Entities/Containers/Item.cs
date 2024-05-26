using System.Runtime.Serialization;
using ALauncher.Abstractions.Entities;
namespace ALauncher.Entities.Containers;

public sealed class Item : IItem, ISearchable
{
    public string Name { get ; set ; } = "New Item";
    public string Path { get ; set ; } = string.Empty;
    public string Params {get;set;} = string.Empty;
    public bool IsFavorite {get;set;} = false;
    [IgnoreDataMember]
    public IFormattable? Icon {get;set;}

    public string type => "item";

    public Item() {  }
}