using Avalonia.Controls.Templates;
using Avalonia.Controls;
using Avalonia.Metadata;
using System;
using System.Windows;
using System.Collections.Generic;
using Avalonia3.Models;
using Avalonia3.ViewModels;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Data;
using ReactiveUI;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace Avalonia3.Services
{
    internal class TemplateSelector : StyledElement, IDataTemplate
    {
        // This Dictionary should store our shapes. We mark this as [Content], so we can directly add elements to it later.
        [Content]
        public Dictionary<string, TreeDataTemplate> AvailableTemplates { get; } = new Dictionary<string, TreeDataTemplate>();

        // Build the DataTemplate here
        public IControl Build(object param)
        {
            Type type = param.GetType();
            if (type == null) return null;
            if( type == typeof(ItreeToken) || type.IsAssignableTo(typeof(ItreeToken))) 
            {
                    var treeToken = (ItreeToken)param;
                    switch(treeToken.Type) 
                    {
                        case ItreeToken.JTokenType.Object:
                            return AvailableTemplates["One"].Build(treeToken as JObjectTree);
                        case ItreeToken.JTokenType.Property:
                            return AvailableTemplates["Two"].Build(treeToken as JPropertyTree);
                        break;
                        default: break;
                        
                
                    }
                return null;
            }
            else
            {
                throw new ArgumentException();
            }

        }

     
        // Check if we can accept the provided data
        public bool Match(object data)
        {
           
            return true;

            
        }
    }
}
