using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Revit_v2018.ViewModel
{
    public class VM_SymbolData : INotifyPropertyChanged
    {
        BitmapImage _Img;
        public BitmapImage Img_
        {
            get { return _Img; }
            set { _Img = value; OnPropertyChanged("Img_"); }
        }

        string _Family;
        public string Family_
        {
            get { return _Family; }
            set { _Family = value; OnPropertyChanged("Family_"); }
        }

        List<string> _SymbolName;
        public List<string> SymbolName_
        {
            get { return _SymbolName; }
            set { _SymbolName = value; OnPropertyChanged("SymbolName_"); }
        }

        Dictionary<string, string> _Symbol;
        public Dictionary<string, string> Symbol_
        {
            get { return _Symbol; }
            set { _Symbol = value; OnPropertyChanged("Symbol_"); }
        }

        string _Name;
        public string Name_
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged("Name_"); }
        }
        protected internal virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
