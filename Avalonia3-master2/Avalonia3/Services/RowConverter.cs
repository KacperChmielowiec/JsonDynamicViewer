using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia3.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia3.Services
{
    public class RowConverter : IValueConverter
    {
        private static int stateRow = 0;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (stateRow == 0)
            {
                stateRow = 1;
                return new BrushConverter().ConvertFrom("#D3D3D3");
                    
                    
            }
            stateRow = 0;
            return new BrushConverter().ConvertFrom("#F5F5F5");

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException(GetType().Name + " can only be used for one way conversion.");
        }
        
    }
}
