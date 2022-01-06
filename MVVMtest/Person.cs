using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMtest
{
    [Serializable]
    public class Person : INotifyPropertyChanged
    {
        private String _name = "張三";
        private int _age = 24;
        private String _hobby = "籃球";

        public String Name
        {
            set
            {
                _name = value;
                if (PropertyChanged != null)//有改變
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));//對Name進行監聽
                }
            }
            get
            {
                return _name;
            }
        }

        public int Age
        {
            set
            {
                _age = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Age"));//對Age進行監聽
                }
            }
            get
            {
                return _age;
            }
        }
        public String Hobby//沒有對Hobby進行監聽
        {
            set
            {
                _hobby = value; 
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Hobby"));//對Hobby進行監聽
                }
            }
            get { return _hobby; }
        }

        private string _Good;

        public string Good
        {
            set
            {
                _Good = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Good"));
                }
            }
            get { return _Good; }
        }
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
