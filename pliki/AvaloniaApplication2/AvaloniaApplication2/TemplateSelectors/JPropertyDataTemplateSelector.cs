using System.Windows;
using Avalonia.Controls.Templates;

using Avalonia.Controls;
using Avalonia.Metadata;
using System;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

namespace AvaloniaApplication2.TemplateSelectors
{
    public sealed class JPropertyDataTemplateSelector : IDataTemplate
    {
      

        [Content]
        public Dictionary<JToken ,IDataTemplate> AvailableTemplates { get; } = new Dictionary<JToken, IDataTemplate>();

        //public override DataTemplate SelectTemplate(object item, DependencyObject container)
        //{
        //    if(item == null)
        //        return null;

        //    var frameworkElement = container as FrameworkElement;
        //    if(frameworkElement == null)
        //        return null;

        //    var type = item.GetType();
        //    if (type == typeof(JProperty))
        //    {
        //        var jProperty = item as JProperty;
        //        switch (jProperty.Value.Type)
        //        {
        //            case JTokenType.Object:
        //                return frameworkElement.FindResource("ObjectPropertyTemplate") as DataTemplate;
        //            case JTokenType.Array:
        //                return frameworkElement.FindResource("ArrayPropertyTemplate") as DataTemplate;
        //            default:
        //                return frameworkElement.FindResource("PrimitivePropertyTemplate") as DataTemplate;

        //        }
        //    }

        //    var key = new DataTemplateKey(type);
        //    return frameworkElement.FindResource(key) as DataTemplate;


            public IControl Build(object param)
            {
                var key = param.ToString(); // Our Keys in the dictionary are strings, so we call .ToString() to get the key to look up
                if (key is null) // If the key is null, we throw an ArgumentNullException
                {
                    throw new ArgumentNullException(nameof(param));
                }
                return AvailableTemplates[key].Build(param); // finally we look up the provided key and let the System build the DataTemplate for us
            }

            // Check if we can accept the provided data
            public bool Match(object data)
            {
                // Our Keys in the dictionary are strings, so we call .ToString() to get the key to look up
                var key = data.ToString();

                return data is JToken                       // the provided data needs to be our enum type
                        && !string.IsNullOrEmpty(key)           // and the key must not be null or empty
                        && AvailableTemplates.ContainsKey(key); // and the key must be found in our Dictionary
            }
        
    }
}
