using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace Lab3
{
    [Serializable]
    public class Contacts : ObservableCollection<Contact>
    {
        readonly string path = "../../contacts.xml";

        public Contacts() { }

        public void Load()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Contacts));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                Contacts contacts = (Contacts)formatter.Deserialize(fs);
                foreach (var c in contacts)
                {
                    Add(c);
                }
            }
        }

        public void Save()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Contacts));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, this);
            }
        }
    }
}
