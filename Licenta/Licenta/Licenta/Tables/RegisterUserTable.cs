
using SQLite;
using System;

namespace Licenta.Tables
{   
    public class RegisterUserTable
    {
        [PrimaryKey, AutoIncrement]
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
