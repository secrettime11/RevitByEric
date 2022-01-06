using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Revit_v2018.Defined
{
    public class UserMember : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void OnPropertyChanged([CallerMemberName] string name ="")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private ObservableCollection<string> _ID;
        public ObservableCollection<string> ID
        {
            set
            {
                _ID = value;
                OnPropertyChanged("Csort");
            }
            get { return _ID; }
        }

        private String _hobby;
        public String Hobby//沒有對Hobby進行監聽
        {
            set
            {
                _hobby = value;
                OnPropertyChanged("Hobby");
            }
            get { return _hobby; }
        }
    }
}
