using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia3.ViewModels;
using Avalonia3.Interface;

namespace Avalonia3.Services
{
    public class TypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is ItreeToken.JTokenType token && parameter != null)
            {
                string type = parameter.ToString();
                switch(type)
                {
                    case "Array":
                        return token == ItreeToken.JTokenType.Array;
                    case "Value":
                        return token == ItreeToken.JTokenType.Value;
                    case "Object":
                        return token == ItreeToken.JTokenType.Object;
                    default:
                        return false;
                }
                
            }
            else { return false; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException(GetType().Name + " can only be used for one way conversion.");
        }
    }
}
