using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace diplom.Models
{
    [Table("Categories")]
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; }

        [ForeignKey(typeof(User))]
        public string UserId { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Transaction> Transactions { get; set; }
    }
}
