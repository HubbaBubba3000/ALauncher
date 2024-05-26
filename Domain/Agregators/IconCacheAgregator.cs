
using ALauncher.Abstractions.Domain;

namespace ALauncher.Domain.Agregators;

public class IconCacheAgregator : IAgregator
{
    public IconCacheAgregator() {}
    public Dictionary<string, string> Icons = new();
}