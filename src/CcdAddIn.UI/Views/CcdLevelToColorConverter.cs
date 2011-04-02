using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using CcdAddIn.UI.ViewModels;

namespace CcdAddIn.UI.Views
{
    class CcdLevelToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((CcdLevel)value)
            {
                case CcdLevel.Black:
                    break;
                case CcdLevel.Red:
                    if ((string)parameter == "Principles")
                        return new SolidColorBrush(Color.FromArgb(255, 224, 6, 40));
                    else
                        return new SolidColorBrush(Color.FromArgb(255, 149, 0, 4));
                case CcdLevel.Orange:
                    break;
                case CcdLevel.Yellow:
                    break;
                case CcdLevel.Green:
                    if ((string)parameter == "Principles")
                        return new SolidColorBrush(Color.FromArgb(255, 87, 172, 43));
                    else
                        return new SolidColorBrush(Color.FromArgb(255, 18, 142, 46));
                case CcdLevel.Blue:
                    break;
                case CcdLevel.White:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("value");
            }

            throw new ArgumentException(value + " is not a valid level.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
