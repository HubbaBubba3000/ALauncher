using ALauncher.Abstractions.Domain;
using MessagePack;


namespace ALauncher.Domain.Cache;

public class CacheDeserialingService 
{ 
    public ValueTask<T> DeserializeAsync<T>(string path) where T : IAgregator
    {
        using (var stream = new StreamReader(path).BaseStream)
        {
            return MessagePackSerializer.DeserializeAsync<T>(stream);
        }
    }
}