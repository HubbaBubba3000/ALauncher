
namespace ALauncher.Abstractions.Domain;

public abstract class Repository {
    protected readonly string WorkFolder = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/ALauncher";
}