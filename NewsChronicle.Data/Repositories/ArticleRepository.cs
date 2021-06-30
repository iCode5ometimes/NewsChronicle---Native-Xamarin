using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewsChronicle.Data.Interfaces;
using NewsChronicle.Data.Model;
using SQLite;

namespace NewsChronicle.Data.Repositories
{
    public class ArticleRepository : IDBRepository<Article>
    {
        #region Properties

        public string StatusMessage { get; set; }

        #endregion

        #region Constructor(s) (and Dependencies)

        private readonly SQLiteAsyncConnection _sqlConn;
        private readonly IDBFileAccessHelper _dBFileAccessHelper;

        public ArticleRepository(IDBFileAccessHelper dBFileAccessHelper)
        {
            _dBFileAccessHelper = dBFileAccessHelper ?? throw new ArgumentNullException(nameof(dBFileAccessHelper));

            _sqlConn = new SQLiteAsyncConnection(_dBFileAccessHelper.GetLocalFilePath());
            _sqlConn.CreateTableAsync<Article>().Wait();
        }

        #endregion

        #region IDBRepository<Article>

        public async Task<bool> AddNewRecordAsync(Article record)
        {
            if(record == null)
            {
                return false;
            }

            var result = 0;
            try
            {
                result = await _sqlConn.InsertAsync(record);
                StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, record.Title);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", record.Title, ex.Message);
            }

            return result != 0;
        }

        public async Task<bool> AddNewRecordListAsync(IEnumerable<Article> records)
        {
            if(records == null)
            {
                return false;
            }

            var result = 0;
            try
            {
                result = await _sqlConn.InsertAllAsync(records);    //runs in transaction by default
                StatusMessage = string.Format("{0} record(s) added.", result);
            }
            catch(Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0} records. Error: {1}", result, ex.Message);
            }

            return result != 0;
        }

        public async Task<IEnumerable<Article>> GetAllRecordsAsync()
        {
            try
            {
                return await _sqlConn.Table<Article>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Article>();
        }

        public Task<bool> DeleteRecordAsync(Article record)
        {
            throw new NotImplementedException("DeleteRecords is not implemented.");
        }

        public async Task<bool> DeleteAllRecordsAsync()
        {
            var result = 0;
            try
            {
                result =  await _sqlConn.DeleteAllAsync<Article>();
            }
            catch(Exception ex)
            {
                StatusMessage = string.Format("Failed to delete {0} records. Error: {1}", result, ex.Message);
            }

            return result != 0;
        }

        #endregion
    }
}
