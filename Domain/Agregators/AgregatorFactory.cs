
using ALauncher.Domain.ConfigRepositories;
using ALauncher.Domain.CacheRepositories;

namespace ALauncher.Domain.Agregators;

public class AgregatorFactory 
{
    ConfigLoadRepository ConfigLoader;
    CacheLoadRepository CacheLoader;
    StorageAgregator? StorageInstance;
    SettingsAgregator? SettingsInstance;
    IconCacheAgregator? IconCacheInstance;
    public StorageAgregator GetStorage() 
    {
        StorageInstance ??= ConfigLoader.Load<StorageAgregator>("Storage");
        return StorageInstance; 
    }    
    public SettingsAgregator GetSettings() 
    {
        SettingsInstance ??= ConfigLoader.Load<SettingsAgregator>("Settings");
        return SettingsInstance;
    }
    public IconCacheAgregator GetIconCache() 
    {
        IconCacheInstance ??= CacheLoader.Load<IconCacheAgregator>("Icons");
        return IconCacheInstance;
    }

    public AgregatorFactory(ConfigLoadRepository clr, CacheLoadRepository hlr)
    {
        ConfigLoader = clr;
        CacheLoader = hlr;
       // if (!Directory.Exists(WorkFolder))
       //     Directory.CreateDirectory(WorkFolder);
    }
}