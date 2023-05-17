using Avalonia.Media;
using Avalonia3.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
namespace Avalonia3.Services
{
    public sealed class TypePropToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var jprop = (value as JPropertyTree).Value as JValueTree;
            if (jprop != null)
            {
                switch (jprop.valueType)
                {
                    case JValueTree.ValueType.String:
                        return new BrushConverter().ConvertFrom("#4e9a06");
                    case JValueTree.ValueType.Float:
                    case JValueTree.ValueType.Int:
                        return new BrushConverter().ConvertFrom("#ad7fa8");
                    case JValueTree.ValueType.Boolean:
                        return new BrushConverter().ConvertFrom("#c4a000");
                    case JValueTree.ValueType.Null:
                        return new SolidColorBrush(Colors.OrangeRed);
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
