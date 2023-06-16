using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListViewMaui
{
    public class InverseBoolConverter : IValueConverter
    {
        /// <summary>
        /// Convert either true or false.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The TargetType.</param>
        /// <param name="parameter">The converter parameter.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns>Returns the boolean value based on the CultureInfo.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }

            return true;
        }

        /// <summary>
        /// Convert either true or false.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The TargetType.</param>
        /// <param name="parameter">The converter parameter.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns>Returns the boolean value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
