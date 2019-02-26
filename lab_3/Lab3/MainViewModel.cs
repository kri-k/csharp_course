using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using System.Diagnostics;

namespace Lab3
{
    class MainViewModel : INotifyPropertyChanged
    {
        Contacts _contacts = new Contacts();
        Contacts _selectedContacts = new Contacts();

        ObservableCollection<ContactViewModel> _contactViewModels = new ObservableCollection<ContactViewModel>();

        CollectionViewSource _cvs = new CollectionViewSource();

        public string Alphabet { get; } = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

        string _startSymb;

        void RefreshView()
        {
            _selectedContacts.Clear();
            UpdatePropertiesCanExecute();
            foreach(var c in _contacts.OrderBy(x => x.Name))
            {
                _contacts.Remove(c);
                _contacts.Add(c);
            }
            _cvs.View.Refresh();
        }

        void UpdatePropertiesCanExecute()
        {
            DeleteCommand.CanExecuteProperty = (_selectedContacts.Count > 0);
            ChangeCommand.CanExecuteProperty = (_selectedContacts.Count == 1);
        }

        public ICollectionView contacts
        {
            get
            {
                return _cvs.View;
            }
        }

        public MainViewModel()
        {
            _contacts = new Contacts();
            _contacts.Load();

            foreach(Contact contact in _contacts)
            {
                _contactViewModels.Add(new ContactViewModel(contact));
            }

            _contacts.CollectionChanged += _contacts_CollectionChanged;

            _cvs.Source = _contactViewModels;
            _cvs.Filter += new FilterEventHandler(_cvs_Filter);

            AddCommand = new DelegateCommand(Add);
            DeleteCommand = new DelegateCommand(Delete);
            ChangeCommand = new DelegateCommand(Change);

            RefreshView();
        }

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        string _filter = "";

        public string Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                _filter = value;
                RefreshView();
            }
        }

        bool _nearestBirthdays = false;

        public bool NearestBirthdays
        {
            get
            {
                return _nearestBirthdays;
            }
            set
            {
                _nearestBirthdays = value;
                RefreshView();
            }
        }

        void _cvs_Filter(object sender, FilterEventArgs e)
        {
            ContactViewModel contactVM = (ContactViewModel)e.Item;
            if (_nearestBirthdays)
            {
                if (contactVM.Birthday != null)
                {
                    var now = DateTime.Now.DayOfYear;
                    var date = DateTime.Parse(contactVM.Birthday).DayOfYear;
                    var weekLater = now + 7;
                    if (now > date || date > weekLater)
                    {
                        e.Accepted = false;
                        return;
                    }
                }
            }
            if (contactVM.Name != null)
            {
                if (_startSymb != null)
                {
                    if (!contactVM.Name.StartsWith(_startSymb))
                    {
                        e.Accepted = false;
                        return;
                    }
                }
                e.Accepted = contactVM.Name.Contains(Filter);
            }
        }

        void _contacts_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                _contactViewModels.Add(new ContactViewModel((Contact)e.NewItems[0]));
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var c = (Contact)e.OldItems[0];
                var v = _contactViewModels.FirstOrDefault(x => x.Contact == c);
                _contactViewModels.Remove(v);
            }
        }

        void Add()
        {
            var newContactViewModel = new ContactViewModel(new Contact());
            if (WindowManager.ShowDialog(newContactViewModel, "add"))
            {
                _contacts.Add(newContactViewModel.Contact);
            }
            _contacts.Save();
        }

        void Delete()
        {
            foreach (var selectedContact in _selectedContacts)
            {
                _contacts.Remove(selectedContact);
            }
            _selectedContacts.Clear();
            _contacts.Save();
        }

        void Change()
        {
            _contacts.Remove(_selectedContacts.First());
            WindowManager.ShowDialog(
                new ContactViewModel(_selectedContacts.First()), "change");
            _contacts.Add(_selectedContacts.First());
            _contacts.Save();
        }

        void ChangeStartSymb(char c)
        {
            if (_startSymb == c.ToString())
            {
                _startSymb = null;
            }
            else
            {
                _startSymb = c.ToString();
            }
            RefreshView();
        }

        void ChangeContactSelection(ContactViewModel contactVM)
        {
            if (_selectedContacts.Contains(contactVM.Contact))
            {
                _selectedContacts.Remove(contactVM.Contact);
            }
            else
            {
                _selectedContacts.Add(contactVM.Contact);
            }
            UpdatePropertiesCanExecute();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DelegateCommand AddCommand { get; set; }

        public DelegateCommand DeleteCommand { get; set; }

        public DelegateCommand ChangeCommand { get; set; }

        public ICommand ChangeStartSymbCommand { get { return new DelegateCommandWithArgs<char>(ChangeStartSymb); } }

        public ICommand ChangeContactSelectionCommand {
            get { return new DelegateCommandWithArgs<ContactViewModel>(ChangeContactSelection); }
        }
    }
}
