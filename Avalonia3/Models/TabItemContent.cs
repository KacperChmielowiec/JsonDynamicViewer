using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia3.Models
{
    public class TabItemContent : INotifyPropertyChanged
    {
        private string _header;


        public string Header
        {
            get { return _header; }
            set { _header = value; OnPropertyChanged("Header"); }
        }

        private string _text;



        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged("Text"); }
        }


        private JContainerTree _json;


        public JContainerTree Json
        {

            get { return _json; }
            set { _json = value; OnPropertyChanged("Json"); }

        }

        public int Tag { get; set; }

        public Guid File { get; set; }

        public JsonFile ctx { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
