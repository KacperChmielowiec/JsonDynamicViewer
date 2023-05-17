﻿using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia3.Models
{
    public class TabItemContent : ReactiveObject { 

        private string _header;


        public string Header
        {
            get => _header;
            set => this.RaiseAndSetIfChanged(ref _header, value);
        }

        private string _text;

        public TabItemContent()
        {
           
        }

        public string Text
        {
            get { return _text; }
            set { this.RaiseAndSetIfChanged(ref _text, value); }
        }


        private JContainerTree _json;


        public JContainerTree Json
        {
            get => _json;
            set => this.RaiseAndSetIfChanged(ref _json, value);
        }



        public int Tag { get; set; }

        public Guid File { get; set; }

        public JsonFile ctx { get; set; }

        //public event PropertyChangedEventHandler PropertyChanged;
        //private void OnPropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}


    }
}