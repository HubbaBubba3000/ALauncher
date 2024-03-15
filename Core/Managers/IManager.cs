namespace ALauncher.Data;

public interface IManager {

    public void Save();

    public void Load(string path);

    public IConfig GetConfig {get;}
}