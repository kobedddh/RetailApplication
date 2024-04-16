using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailApplication.Infrastructure.DBHelpers
{
    public class BaseDBHelper
    {
        private string _connectionString;

        public BaseDBHelper(string connectionString)

        {
            _connectionString = connectionString;
        }

        public IDbConnection GetNewConnection()
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
            return new MySqlConnection(_connectionString);
        }
    }
}
