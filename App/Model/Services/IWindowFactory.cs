
namespace ALauncher.Data;

public interface IWindowFactory
{
    /// <summary>
    /// show dialog window, return true if it close successful
    /// </summary>
    public bool Show();
}