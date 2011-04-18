﻿using System;
using System.Globalization;
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
                    break;
                case Level.Red:
                    if ((string)parameter == "Principles")
                        return new SolidColorBrush(Color.FromArgb(255, 224, 6, 40));
                    else
                        return new SolidColorBrush(Color.FromArgb(255, 149, 0, 4));
                case Level.Orange:
                    break;
                case Level.Yellow:
                    break;
                case Level.Green:
                    if ((string)parameter == "Principles")
                        return new SolidColorBrush(Color.FromArgb(255, 87, 172, 43));
                    else
                        return new SolidColorBrush(Color.FromArgb(255, 18, 142, 46));
                case Level.Blue:
                    break;
                case Level.White:
                    break;
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
