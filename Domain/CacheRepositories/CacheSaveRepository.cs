using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using ALauncher.Abstractions.Domain;
using ALauncher.Domain.Cache;


namespace ALauncher.Domain.CacheRepositories;

public class CacheSaveRepository : Repository
{
    public void Save<T>(T cache, string cacheName) where T : IAgregator 
    {
        CacheSerializer.Serialize<T>(cache, $"{WorkFolder}/{cacheName}.json");

    }
    public Task SaveAsync<T>(T cache, string cacheName) where T : IAgregator 
    {
        return CacheSerializer.SerializeAsync<T>(cache, $"{WorkFolder}/{cacheName}.json");
    }
    CacheSerializingService CacheSerializer;
    public CacheSaveRepository(CacheSerializingService css) 
    {
        CacheSerializer = css;
    }
}