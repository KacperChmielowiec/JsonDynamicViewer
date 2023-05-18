using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia3.Models;
using Avalonia3.ViewModels;

namespace Avalonia3.Services
{
    public class JarrayLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is JContainerTree)) return null;
            ItreeToken jToken = (value as ItreeToken).GetValue();
            if (jToken == null)
                return null;

            switch (jToken.Type)
            {
                case ItreeToken.JTokenType.Array:
                    var arrayLen = (jToken as JContainerTree).Children().Count();
                    return string.Format("[{0}]", arrayLen);
                case ItreeToken.JTokenType.Property:
                    var propertyArrayLen = (jToken as JContainerTree).Children().Count();
                    return string.Format("[ {0} ]", propertyArrayLen);
                case ItreeToken.JTokenType.Object:
                    var ObjLen = (jToken as JContainerTree).Children().Count();
                    return string.Format("[ {0} ]", ObjLen);
                default:
                    throw new Exception("Type should be JProperty or JArray");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException(GetType().Name + " can only be used for one way conversion.");
        }
    }
}
