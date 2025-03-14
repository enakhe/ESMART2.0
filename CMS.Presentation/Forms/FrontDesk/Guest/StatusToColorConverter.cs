﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ESMART.Presentation.Forms.FrontDesk.Guest
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return new SolidColorBrush(Colors.Gray);

            string status = value.ToString().ToLowerInvariant();
            switch (status)
            {
                case "active":
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4EAD16")); // Light Green
                case "inactive":
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#bd0101")); // Light Red
                default:
                    return new SolidColorBrush(Colors.LightGray);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
