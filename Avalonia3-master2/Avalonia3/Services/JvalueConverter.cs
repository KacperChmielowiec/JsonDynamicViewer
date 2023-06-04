using Avalonia.Data.Converters;
using Avalonia3.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Avalonia3.Services
{
    public class JvalueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
          
            if (value is JValueTree jval)
            {
                switch (jval.valueType)
                {
                    case JValueTree.ValueType.String:
                        return "\"" + jval.Value.ToString() + "\"";
                    case JValueTree.ValueType.Null:
                        return "Null";
                    default: return jval.Value.ToString();
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException(GetType().Name + " can only be used for one way conversion.");
        }
    }
}
