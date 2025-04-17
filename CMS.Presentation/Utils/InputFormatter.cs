﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;

namespace ESMART.Application.Common.Utils
{
    public static class InputFormatter
    {
        public static void AllowDecimalOnly(object sender, TextCompositionEventArgs e)
        {
            // Allow digits and only one decimal point
            e.Handled = !Regex.IsMatch(((TextBox)sender).Text + e.Text, @"^\d*\.?\d{0,2}$");
        }

        public static void FormatAsPercentageOnLostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && double.TryParse(textBox.Text, out double value))
            {
                // Format to 2 decimal places followed by %
                textBox.Text = $"{value:F2}%";
            }
        }

        public static void FormatAsDecimalOnLostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && double.TryParse(textBox.Text, out double value))
            {
                // Format to 2 decimal places
                textBox.Text = $"{value:F2}";
            }
        }

        public static void StripPercentageOnGotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = textBox.Text.Replace("%", "").Trim();
            }
        }
    }

}
