using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace diplom.Models
{
    [Table("Deals")]
    public class Deal
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; }
        public bool Notification { get; set; }
        public string Note { get; set; }

        [NotNull]
        public DateTime DateOfCreation { get; set; }

        [NotNull]
        public DateTime Deadline { get; set; }

        [NotNull, ForeignKey(typeof(User))]
        public int UserId { get; set; }

        [NotNull, ForeignKey(typeof(Deal))]
        public int OtherDealId { get; set; }

        [NotNull, ForeignKey(typeof(Status))]
        public int StatusId { get; set; }

        [NotNull, ForeignKey(typeof(Importance))]
        public int ImportanceId { get; set; }
    }
}
