using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia3.Models;
using Avalonia3.ViewModels;
using Avalonia3.Interface;

namespace Avalonia3.Services
{
    public class JarrayLengthConverter : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ItreeToken jToken = (value as ItreeToken);
            if (jToken == null)
                return null;
            var jTokenValue = jToken.GetValue();
           
            
            switch (jTokenValue.Type)
            {
                case ItreeToken.JTokenType.Array:
                    var arrayLen = (jTokenValue as JContainerTree).Children().Count();
                    return string.Format("[{0}]", arrayLen);
                case ItreeToken.JTokenType.Object:
                    var ObjLen = (jTokenValue as JContainerTree).Children().Count();
                    return string.Format("[ {0} ]", ObjLen);
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException(GetType().Name + " can only be used for one way conversion.");
        }
    }
}
