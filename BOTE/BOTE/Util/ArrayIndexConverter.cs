using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace BOTE.Util
{
    class ArrayIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string parameterString)
            {
                // Parse parameter string '0,0' into two integers
                var indices = parameterString.Split(',');
                int row = int.Parse(indices[0]);
                int column = int.Parse(indices[1]);

                // Assuming value is a 2D array (BattlefieldUnits)
                int[,] array = value as int[,];
                if (array != null)
                {
                    return array[row, column];
                }
            }

            return DependencyProperty.UnsetValue; // or some default value
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
