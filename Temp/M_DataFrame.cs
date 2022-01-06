using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temp
{
    public class M_DataFrame : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _TypeOne;
        public string TypeOne
        {
            get { return _TypeOne; }
            set
            {
                _TypeOne = value;
                OnPropertyChanged("TypeOne");
            }
        }

        private string _TypeTwo;
        public string TypeTwo
        {
            get { return _TypeTwo; }
            set
            {
                _TypeTwo = value;
                OnPropertyChanged("TypeTwo");
            }
        }

        private string _ImageUrl;
        public string ImageUrl
        {
            get { return _ImageUrl; }
            set
            {
                _ImageUrl = value;
                OnPropertyChanged("ImageUrl");
            }
        }
    }
    public static class Data
    {
        public static string TypeOne { get; set; }
        public static string TypeTwo { get; set; }
        public static System.Drawing.Bitmap images { get; set; }
    }
}
