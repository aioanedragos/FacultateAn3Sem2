using SQLite;
using System;

namespace Licenta.Tables
{
    class Children
    {
        [PrimaryKey, AutoIncrement]
        public Guid UserId { get; set; }

        public string ChildrenName { get; set; }

        public int Age { get; set; }

        public string LetterRemane { get; set; }

        public int Accuracy { get; set; }
    }
}
