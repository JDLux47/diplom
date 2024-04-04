using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace diplom.Models
{
    [Table("Users")]
    public class User
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public string Login { get; set; }

        [NotNull]
        public string Password { get; set; }

        [NotNull]
        public decimal Balance { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Transaction> Transactions { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Deal> Deals { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Category> Categories { get; set; }
    }
}
