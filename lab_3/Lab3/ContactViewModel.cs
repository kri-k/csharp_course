using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lab3
{
    class ContactViewModel : INotifyPropertyChanged
    {
        Contact _c;

        public Contact Contact
        {
            get
            {
                return _c;
            }
        }

        public ContactViewModel(Contact c)
        {
            _c = c;
        }


        public string Name
        {
            get { return _c.Name; }
            set { _c.Name = value; OnPropertyChanged(); }
        }

        public string Birthday
        {
            get
            {
                return _c.Birthday.HasValue ? _c.Birthday.Value.Date.ToString("dd-MM-yyyy") : "не указан";
            }
            set
            {
                try
                {
                    _c.Birthday = DateTime.Parse(value);
                }
                catch (Exception)
                {
                    _c.Birthday = null;
                }
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _c.Email; }
            set { _c.Email = value; OnPropertyChanged(); }
        }

        public string PhoneNumber
        {
            get { return _c.PhoneNumber; }
            set { _c.PhoneNumber = value; OnPropertyChanged(); }
        }

        public string Comment
        {
            get { return _c.Comment; }
            set { _c.Comment = value; OnPropertyChanged(); }
        }

        public bool IsEmpty
        {
            get
            {
                return (Email == null || Email == "") &&
                        (Name == null || Name == "") &&
                        (PhoneNumber == null || PhoneNumber == "");
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
