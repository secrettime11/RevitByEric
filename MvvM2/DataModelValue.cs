using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvM2
{
    public class DataModelValueList : ObservableCollection<DataModelValue>

    {

    }
    public class DataModelValue : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int ID { get; set; }

        private string no;
        public string No
        {
            get { return no; }
            set
            {
                if (no != value)
                {
                    no = value;
                    if (this.PropertyChanged != null)
                    {

                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("No"));
                    }
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    if (this.PropertyChanged != null)
                    {

                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Name"));
                    }
                }
            }
        }

        private string v;
        public string V
        {
            get { return v; }
            set
            {
                if (v != value)
                {
                    v = value;
                    if (this.PropertyChanged != null)
                    {

                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("V"));
                    }
                }
            }
        }
        private string i;
        public string I
        {
            get { return i; }
            set
            {
                if (i != value)
                {
                    i = value;
                    if (this.PropertyChanged != null)
                    {

                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("I"));
                    }
                }
            }
        }
        private string c;
        public string C
        {
            get { return c; }
            set
            {
                if (c != value)
                {
                    c = value;
                    if (this.PropertyChanged != null)
                    {

                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("C"));
                    }
                }
            }
        }
        private string t;
        public string T
        {
            get { return t; }
            set
            {
                if (t != value)
                {
                    t = value;
                    if (this.PropertyChanged != null)
                    {

                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("T"));
                    }
                }
            }
        }



    }
}
