using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using CcdAddIn.UI.CleanCodeDeveloper;
using NLog;

namespace CcdAddIn.UI.Views
{
    class CcdLevelToColorConverter : IValueConverter
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            _logger.Trace("Converting {0}", value);

            switch ((Level)value)
            {
                case Level.Black:
                case Level.White:
                    // do not convert these levels into a color
                    return DependencyProperty.UnsetValue;
                case Level.Red:
                    if ((string)parameter == "Principles")
                        return new SolidColorBrush(Color.FromArgb(255, 224, 6, 40));
                    else
                        return new SolidColorBrush(Color.FromArgb(255, 149, 0, 4));
                case Level.Orange:
                    if ((string)parameter == "Principles")
                        return new SolidColorBrush(Color.FromArgb(255, 242, 148, 0));
                    else
                        return new SolidColorBrush(Color.FromArgb(255, 234, 100, 29));
                case Level.Yellow:
                    if ((string)parameter == "Principles")
                        return new SolidColorBrush(Color.FromArgb(255, 248, 233, 28));
                    else
                        return new SolidColorBrush(Color.FromArgb(255, 242, 179, 12));
                case Level.Green:
                    if ((string)parameter == "Principles")
                        return new SolidColorBrush(Color.FromArgb(255, 87, 172, 43));
                    else
                        return new SolidColorBrush(Color.FromArgb(255, 18, 142, 46));
                case Level.Blue:
                    if ((string)parameter == "Principles")
                        return new SolidColorBrush(Color.FromArgb(255, 46, 170, 221));
                    else
                        return new SolidColorBrush(Color.FromArgb(255, 14, 113, 180));
                default:
                    throw new ArgumentOutOfRangeException("value");
            }

            _logger.Warn("Can't find a suitable color for value {0}", value);
            throw new ArgumentException(value + " is not a valid level.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
