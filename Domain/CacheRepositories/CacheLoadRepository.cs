
using System.Threading.Tasks;
using ALauncher.Abstractions.Domain;
using ALauncher.Domain.Cache;


namespace ALauncher.Domain.CacheRepositories;

public class CacheLoadRepository : Repository
{
    public T Load<T>(string path) where T : IAgregator 
    {
        throw new NotImplementedException("for load cache use LoadAsync");
       // return CacheDeserializer.DeserializeAsync<T>(path);

    }
    public ValueTask<T> LoadAsync<T>(string cacheName) where T : IAgregator 
    {
        
        return CacheDeserializer.DeserializeAsync<T>($"{WorkFolder}/{cacheName}.json");
    }
    readonly CacheDeserialingService CacheDeserializer;
    public CacheLoadRepository(CacheDeserialingService cds) 
    {
        CacheDeserializer = cds;
    }
}