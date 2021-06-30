using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsChronicle.Data.Interfaces
{
    public interface IDBRepository<T>
    {
        Task<bool> AddNewRecordAsync(T record);
        Task<IEnumerable<T>> GetAllRecordsAsync();
        Task<bool> AddNewRecordListAsync(IEnumerable<T> records);
        Task<bool> DeleteRecordAsync(T record);
        Task<bool> DeleteAllRecordsAsync();
    }
}
