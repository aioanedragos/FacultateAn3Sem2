using SQLite;

namespace LicentaPasCuPas.DataBase
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
