using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailApplication.Core.IDBHelpers
{
    public interface IDBHelper
    {
        IDbConnection GetNewConnection();
        Task<int> ExecuteAsync(string sql, object parameter);
        Task<T> FirstAsync<T>(string sql, object parameter);
        Task<T> FirstProcAsync<T>(string sql, object parameter);
        Task<List<T>> ListAsync<T>(string sql, object parameter = null);
        Task<List<T>> ListProcAsync<T>(string sql, object parameter = null);
        Task<List<T>> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<int> InsertAsync<T>(T entity, IDbTransaction trx = null) where T : class;
        Task<int> UpdateAsync<T>(T entity, IDbTransaction trx = null) where T : class;
        Task<int> DeleteAsync<T>(T entity, IDbTransaction trx = null) where T : class;

        //non-async
        List<T> List<T>(string sql, object parameter = null);
        List<T> ListProc<T>(string sql, object parameter = null);

        int Execute(string sql, object parameter);
        T First<T>(string sql, object parameter);
        T FirstProc<T>(string sql, object parameter);
        T Get<T>(int id);
        int Insert<T>(T entity, IDbTransaction trx = null) where T : class;
        int Update<T>(T entity, IDbTransaction trx = null) where T : class;
        int Delete<T>(T entity, IDbTransaction trx = null) where T : class;
    }
}
