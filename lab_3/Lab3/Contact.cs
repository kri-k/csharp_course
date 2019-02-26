using System;
using System.Collections.ObjectModel;

namespace Lab3
{
    [Serializable]
    public class Contact
    {
        public Contact() { }

        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; }
        public string Comment { get; set; }
    }
}
