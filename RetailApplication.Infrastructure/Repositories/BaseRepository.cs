using RetailApplication.Core.IDBHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailApplication.Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        protected IDBHelper DBHelper { get; set; }
        public BaseRepository(IDBHelper dbHelper)
        {
            DBHelper = dbHelper;
        }
    }
}
