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
            JContainerTree jToken;
            if (value is JPropertyTree)
            {
                jToken = (value as JPropertyTree).Value as JArrayTree;
            }
            else
            {
                jToken = value as JArrayTree;
            }

            if (jToken == null)
                return null;

            switch (jToken.Type)
            {
                case ItreeToken.JTokenType.Array:
                    var arrayLen = jToken.Children().Count();
                    return string.Format("[{0}]", arrayLen);
                case ItreeToken.JTokenType.Property:
                    var propertyArrayLen = jToken.Children().Count();
                    return string.Format("[ {0} ]", propertyArrayLen);
                case ItreeToken.JTokenType.Object:
                    var ObjLen = jToken.Children().Count();
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
