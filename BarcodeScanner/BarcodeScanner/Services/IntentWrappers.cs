using System.Threading.Tasks;

namespace BarcodeScanner.Services
{
    public interface IIntentWrappers
    {
        Task MakeIntentAsync(string intent);
    }
}
