using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Tables
{
    public class Databasechildren
    {
        readonly SQLiteAsyncConnection _database;

        public Databasechildren(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Children>().Wait();
        }

        public Task<List<Children>> GetPeopleAsync()
        {
            return _database.Table<Children>().ToListAsync();
        }

        public Task<List<Children>> DeletePersonAsync(int ID)
        {
            _database.Table<Children>().DeleteAsync(x => x.UserId == ID);
            return _database.Table<Children>().ToListAsync();
        }

        public Task<Children> GetPersonAsync()
        {
            var ceva = _database.Table<Children>().OrderBy(x => x.ChildrenName).FirstOrDefaultAsync();
            if (ceva != null)
                return ceva;
            else
                return null;
        }

        public Task<int> SavePersonAsync(Children children)
        {
            return _database.InsertAsync(children);
        }
    }
}
