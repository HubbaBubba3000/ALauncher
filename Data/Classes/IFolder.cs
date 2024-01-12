

namespace ALauncher.Data;

public interface IFolder {
    public string Name {get;set;}

    public List<IItem> Items {get;set;}

}