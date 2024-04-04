using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace diplom.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public int Type { get; set; }

        [NotNull]
        public decimal Sum { get; set; }
        public DateTime Date { get; set; }

        [NotNull, ForeignKey(typeof(Category))]
        public int CategoryId { get; set; }

        [NotNull, ForeignKey(typeof(User))]
        public int UserId { get; set; }
    }
}
