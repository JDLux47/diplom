using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace diplom.Models
{
    [Table("Deals")]
    public class Deal : INotifyPropertyChanged
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

        private bool notification;
        public bool Notification
        {
            get { return notification; }
            set
            {
                if (notification != value)
                {
                    notification = value;
                    OnPropertyChanged(nameof(Notification));
                }
            }
        }

        private string note;
        public string Note
        {
            get { return note; }
            set
            {
                if (note != value)
                {
                    note = value;
                    OnPropertyChanged(nameof(Note));
                }
            }
        }

        private DateTime dateOfCreation;
        [NotNull]
        public DateTime DateOfCreation 
        {
            get { return dateOfCreation; }
            set
            {
                if (dateOfCreation != value)
                {
                    dateOfCreation = value;
                    OnPropertyChanged(nameof(DateOfCreation));
                }
            }
        }

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

        private int userId;
        [NotNull, ForeignKey(typeof(User))]
        public int UserId 
        {
            get { return userId; }
            set
            {
                if (userId != value)
                {
                    userId = value;
                    OnPropertyChanged(nameof(UserId));
                }
            }
        }

        private int otherDealId;
        [NotNull, ForeignKey(typeof(Deal))]
        public int OtherDealId 
        {
            get { return otherDealId; }
            set
            {
                if (otherDealId != value)
                {
                    otherDealId = value;
                    OnPropertyChanged(nameof(OtherDealId));
                }
            }
        }

        private int statusId;
        [NotNull, ForeignKey(typeof(Status))]
        public int StatusId 
        {
            get { return statusId; }
            set
            {
                if (statusId != value)
                {
                    statusId = value;
                    OnPropertyChanged(nameof(StatusId));
                }
            }
        }

        private int importanceId;
        [NotNull, ForeignKey(typeof(Importance))]
        public int ImportanceId 
        {
            get { return importanceId; }
            set
            {
                if (importanceId != value)
                {
                    importanceId = value;
                    OnPropertyChanged(nameof(ImportanceId));
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
