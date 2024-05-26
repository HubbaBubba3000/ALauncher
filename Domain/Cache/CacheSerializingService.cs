using MessagePack;
using ALauncher.Abstractions.Domain;
namespace ALauncher.Domain.Cache;


public class CacheSerializingService 
{
    public void Serialize<T>(object cache, string path) where T : IAgregator
    {
        if (cache.GetType() != typeof(T) ) 
            throw new ArgumentException("Cache object type not valid");
        using (var stream = new StreamWriter(path).BaseStream)
            MessagePackSerializer.Serialize(stream, cache);
    }
    public Task SerializeAsync<T>(T cache, string path) where T : IAgregator
    {
        if (cache.GetType() != typeof(T) ) 
            throw new ArgumentException("Cache object type not valid");
        using (var stream = new StreamWriter(path).BaseStream)
            return MessagePackSerializer.SerializeAsync(stream, cache);
    }
}