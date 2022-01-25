using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit_v2018.ViewModel
{
    public class VM_SymbolList
    {
        private ObservableCollection<VM_SymbolData> _SList;
        public ObservableCollection<VM_SymbolData> SList
        {
            get
            {
                return this._SList;
            }
            set
            {
                if (this._SList != value)
                {
                    this._SList = value;
                    OnPropertyChanged("SList");
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
