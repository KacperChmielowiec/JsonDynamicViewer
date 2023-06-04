
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia3.Models;
using Avalonia3.Views;
using MessageBox.Avalonia.Enums;
using Newtonsoft.Json;

namespace Avalonia3.Services
{
    public class DialogFileServieces
    {
        public string Name { get; set; }

        public string Ext { get; set; }
        
        public string Filter { get; set; }

        public string FileName { get;set; }
        public JsonServices Services { get; set; } = new JsonServices();
        public DialogFileServieces(string _name, string _ext, string _filter) { 
        
            Name = _name;
            Ext = _ext;
            Filter = _filter;  
            
        }

        public async Task<string> ShowFileDialog(Window window)
        {

            var dialog = new OpenFileDialog();
            dialog.InitialFileName = this.Name ?? "Document";
            dialog.Filters = new List<FileDialogFilter>() { new FileDialogFilter { Name="*", Extensions= new List<string>() { "json" } } };
            var result = await dialog.ShowAsync(window);

            // Process open file dialog box results
            if (result != null)
            {
                // Open document
                FileName = result[0];
                string fileContent = "";
                fileContent = File.ReadAllText(FileName);
                return fileContent;
                
            }
            else
            {
                return string.Empty;
            }

        }

    }
}
