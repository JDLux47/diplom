using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace diplom.Models
{
    [Table("Transactions")]
    public class Transaction : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private int type;
        [NotNull]
        public int Type 
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    OnPropertyChanged(nameof(Type));
                }
            }
        }

        private decimal sum;
        [NotNull]
        public decimal Sum 
        {
            get { return sum; }
            set
            {
                if (sum != value)
                {
                    sum = value;
                    OnPropertyChanged(nameof(Sum));
                }
            }
        }

        private DateTime date;
        public DateTime Date 
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        private int categoryId;
        [NotNull, ForeignKey(typeof(Category))]
        public int CategoryId 
        {
            get { return categoryId; }
            set
            {
                if (categoryId != value)
                {
                    categoryId = value;
                    OnPropertyChanged(nameof(CategoryId));
                }
            }
        }

        [NotNull, ForeignKey(typeof(User))]
        public int UserId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
