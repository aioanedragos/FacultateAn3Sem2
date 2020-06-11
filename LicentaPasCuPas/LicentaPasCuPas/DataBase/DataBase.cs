using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace LicentaPasCuPas.DataBase
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
        }

        public Task<List<User>> GetPeopleAsync()
        {
            return _database.Table<User>().ToListAsync();
        }

        public Task<User> GetPersonAsync(string email)
        {
            var ceva = _database.Table<User>().Where(i => i.Email.Equals(email)).FirstOrDefaultAsync();
            if (ceva != null)
                return ceva;
            else
                return null;
        }

        public Task<int> SavePersonAsync(User user)
        {
            return _database.InsertAsync(user);
        }
    }
}
