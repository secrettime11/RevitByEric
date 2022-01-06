using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit_v2018.ViewModel
{
    public class VM_CList : INotifyPropertyChanged
    {
        private ObservableCollection<VM_Category> _CList;
        public ObservableCollection<VM_Category> CList
        {
            get
            {
                return this._CList;
            }
            set
            {
                if (this._CList != value)
                {
                    this._CList = value;
                    OnPropertyChanged("CList");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
