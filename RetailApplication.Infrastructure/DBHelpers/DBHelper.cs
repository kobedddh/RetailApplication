using Dapper;
using RetailApplication.Core.IDBHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailApplication.Infrastructure.DBHelpers
{
    public class DBHelper : BaseDBHelper, IDBHelper
    {
        public DBHelper(string connectionString)
            : base(connectionString)
        {

        }

        public async Task<int> ExecuteAsync(string sql, object parameter)
        {
            using (var conn = GetNewConnection())
            {
                return await conn.ExecuteAsync(sql, parameter, commandType: CommandType.Text);
            }
        }

        public async Task<T> FirstAsync<T>(string sql, object parameter)
        {
            return await FirstBaseAsync<T>(sql, parameter, CommandType.Text);
        }

        public async Task<T> FirstProcAsync<T>(string sql, object parameter)
        {
            return await FirstBaseAsync<T>(sql, parameter, CommandType.StoredProcedure);
        }

        private async Task<T> FirstBaseAsync<T>(string sql, object parameter, CommandType commandType)
        {
            using (var conn = GetNewConnection())
            {
                var result = await conn.QueryFirstOrDefaultAsync<T>(sql, parameter, commandType: commandType);
                return result;
            }
        }

        public async Task<List<T>> ListAsync<T>(string sql, object parameter = null)
        {
            var result = await ListBaseAsync<T>(sql, CommandType.Text, parameter);
            return result;
        }

        public async Task<List<T>> ListProcAsync<T>(string sql, object parameter = null)
        {
            return await ListBaseAsync<T>(sql, CommandType.StoredProcedure, parameter);
        }

        private async Task<List<T>> ListBaseAsync<T>(string sql, CommandType commandType, object parameter = null)
        {
            using (var conn = GetNewConnection())
            {
                var result = await conn.QueryAsync<T>(sql, parameter, commandType: commandType);
                return result.ToList();
            }
        }

        public async Task<List<T>> GetAllAsync<T>()
        {
            var table = typeof(T).Name.ToLower();
            using (var conn = GetNewConnection())
            {
                var sql = $@"select * from {table};";
                return await ListAsync<T>(sql);
            }
        }

        public async Task<T> GetAsync<T>(int id)
        {
            using (var conn = GetNewConnection())
            {
                return await conn.GetAsync<T>(id);
            }
        }

        public async Task<int> InsertAsync<T>(T entity, IDbTransaction trx = null) where T : class
        {
            using (var conn = GetNewConnection())
            {
                var id = await conn.InsertAsync<T>(entity, trx);
                return id != null
                    ? id.GetValueOrDefault()
                    : 0;
            }
        }

        public async Task<int> UpdateAsync<T>(T entity, IDbTransaction trx = null) where T : class
        {
            using (var conn = GetNewConnection())
            {
                return await conn.UpdateAsync<T>(entity, trx);
            }
        }

        public async Task<int> DeleteAsync<T>(T entity, IDbTransaction trx = null) where T : class
        {
            using (var conn = GetNewConnection())
            {
                return await conn.DeleteAsync<T>(entity, trx);
            }
        }

        public List<T> List<T>(string sql, object parameter = null)
        {
            var result = ListBase<T>(sql, CommandType.Text, parameter);
            return result;
        }

        public List<T> ListProc<T>(string sql, object parameter = null)
        {
            return ListBase<T>(sql, CommandType.StoredProcedure, parameter);
        }

        private List<T> ListBase<T>(string sql, CommandType commandType, object parameter = null)
        {
            using (var conn = GetNewConnection())
            {
                var result = conn.Query<T>(sql, parameter, commandType: commandType);
                return result.ToList();
            }
        }

        public int Execute(string sql, object parameter)
        {
            using (var conn = GetNewConnection())
            {
                return conn.Execute(sql, parameter, commandType: CommandType.Text);
            }
        }

        public T First<T>(string sql, object parameter)
        {
            return FirstBase<T>(sql, parameter, CommandType.Text);
        }

        public T FirstProc<T>(string sql, object parameter)
        {
            return FirstBase<T>(sql, parameter, CommandType.StoredProcedure);
        }

        private T FirstBase<T>(string sql, object parameter, CommandType commandType)
        {
            using (var conn = GetNewConnection())
            {
                var result = conn.QueryFirstOrDefault<T>(sql, parameter, commandType: commandType);
                return result;
            }
        }

        public T Get<T>(int id)
        {
            using (var conn = GetNewConnection())
            {
                return conn.Get<T>(id);
            }
        }

        public int Insert<T>(T entity, IDbTransaction trx = null) where T : class
        {
            using (var conn = GetNewConnection())
            {
                var id = conn.Insert<T>(entity, trx);
                return id != null
                    ? id.GetValueOrDefault()
                    : 0;
            }
        }

        public int Update<T>(T entity, IDbTransaction trx = null) where T : class
        {
            using (var conn = GetNewConnection())
            {
                return conn.Update<T>(entity, trx);
            }
        }

        public int Delete<T>(T entity, IDbTransaction trx = null) where T : class
        {
            using (var conn = GetNewConnection())
            {
                return conn.Delete<T>(entity, trx);
            }
        }
    }
}
