using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using System.Net;
using System;

namespace AvaloniaApplication2.Views
{
    public partial class MainWindow : Window
    {
       
            public MainWindow()
            {
                InitializeComponent();
                
            try
            {
                TextBlock txt = new TextBlock();
                TreeViewer tree = new TreeViewer();
                var client = new WebClient { Proxy = null };
                client.DownloadStringCompleted += delegate (object sender, DownloadStringCompletedEventArgs args)
                {
                    tree.Load(args.Result);
                    
                    txt.Text = "Loading finished";
                };
                txt.Text = "Loading...";

                // Choose 1
                //client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/posts"));
                //client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/comments"));
                //client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/albums"));
                //client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/photos"));
                //client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/todos"));
                client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/users"));
            }
            catch (Exception ex)
            {
                TreeViewer tree = new TreeViewer();
              
                Debug.WriteLine(ex.Message);
                const string json = "{\"one\": \"two\",\"key\": \"value\"}";
                tree.Load(json);
            }

        }
       
           
    }
}