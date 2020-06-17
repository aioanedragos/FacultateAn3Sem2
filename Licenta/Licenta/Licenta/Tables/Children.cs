using SQLite;
using System;

namespace Licenta.Tables
{
    public class Children
    {
        [PrimaryKey, AutoIncrement]
        public Guid UserId { get; set; }
        
        public string ChildrenName { get; set; }

        public int Age { get; set; }

        public char LetterRemane { get; set; }

        public int Accuracy { get; set; }
    }
}
