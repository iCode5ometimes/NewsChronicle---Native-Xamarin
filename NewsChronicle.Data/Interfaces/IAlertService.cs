using System.Threading.Tasks;

namespace NewsChronicle.Data.Interfaces
{
    public interface IAlertService
    {
        Task<bool> ShowAlertAsync(string title, string message);
    }
}
