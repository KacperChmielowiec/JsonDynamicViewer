
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
using Newtonsoft.Json;

namespace Avalonia3.Services
{
    public class DialogFileServieces
    {
        public string Name { get; set; }

        public string Ext { get; set; }
        
        public string Filter { get; set; }
        public DialogFileServieces(string _name, string _ext, string _filter) { 
        
            Name = _name;
            Ext = _ext;
            Filter = _filter;  
        
        
        }

        public async Task<Guid> Show()
        {

            var dialog = new OpenFileDialog();

            

            dialog.InitialFileName = this.Name ?? "Document";

            //this.Filter ?? "Text documents (.json)|*.json"; // Filter files by extension
            dialog.Filters = new List<FileDialogFilter>() { new FileDialogFilter { Name="*", Extensions= new List<string>() { "json" } } };

            // Show open file dialog box
            var window = ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current.ApplicationLifetime).MainWindow;
            var result = await dialog.ShowAsync(window);

            // Process open file dialog box results
            if (result != null)
            {
                // Open document
                string filename = result[0];

                string fileContent = "";

                StringBuilder sb = new StringBuilder();

                using (StreamReader sr = new StreamReader(filename))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        // Przetwarzaj każdą linijkę osobno
                      sb.AppendLine(line);
                    }
                }
                fileContent = sb.ToString();

                JsonTextReader reader = new JsonTextReader(new StringReader(fileContent));
                Dictionary<Guid, ObjectContext> keyValuePairs = new Dictionary<Guid, ObjectContext>();

                var _guid = Guid.NewGuid();
                try
                {

                    var jObject = Process.ProcessJsonData(reader, keyValuePairs);
                    JsonMap.files.Add(new JsonFile(_guid, keyValuePairs, jObject.Id, filename));

                    return _guid;

                }
                catch(ApplicationException e)
                {
                    throw new ApplicationException();
                }
                
                



            }
            else
            {
                return Guid.Empty;
            }

        }

    }
}
