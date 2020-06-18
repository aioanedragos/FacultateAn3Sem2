using SQLite;
using System;

namespace Licenta.Tables
{
    public class Children
    {

        public Guid ParentId { get; set; }

        [PrimaryKey, AutoIncrement]
        public Guid UserId { get; set; }
        
        public string ChildrenName { get; set; }

        public int Age { get; set; }

        public string LetterRemane { get; set; }

        public string Accuracy { get; set; }
    }
}
