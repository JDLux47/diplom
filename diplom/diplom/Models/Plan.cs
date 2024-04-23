using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace diplom.Models
{
    [Table("Plan")]
    public class Plan : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private string name;
        [NotNull]
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
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

        [NotNull, ForeignKey(typeof(User))]
        public int UserId { get; set; }


        private DateTime deadline;
        [NotNull]
        public DateTime Deadline
        {
            get { return deadline; }
            set
            {
                if (deadline != value)
                {
                    deadline = value;
                    OnPropertyChanged(nameof(Deadline));
                }
            }
        }

        private bool done;
        [NotNull]
        public bool Done
        {
            get { return done; }
            set
            {
                if(done != value)
                {
                    done = value;
                    OnPropertyChanged(nameof(Done));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
