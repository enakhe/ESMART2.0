﻿using ESMART.Application.Common.Interface;
using ESMART.Application.Common.Utils;
using ESMART.Domain.Entities.FrontDesk;
using ESMART.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ESMART.Presentation.Forms.FrontDesk.Guest
{
    /// <summary>
    /// Interaction logic for FundGuestAccountDialog.xaml
    /// </summary>
    public partial class FundGuestAccountDialog : Window
    {
        private DispatcherTimer _formatTimer;
        private bool _suppressTextChanged = false;
        private Domain.Entities.FrontDesk.Guest _guest;
        private readonly IGuestRepository _guestRepository;
        public FundGuestAccountDialog(Domain.Entities.FrontDesk.Guest guest, IGuestRepository guestRepository)
        {
            InitializeComponent();
            _guestRepository = guestRepository;

            _formatTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            _formatTimer.Tick += FormatTimer_Tick;

            _guest = guest;
        }

        private void DecimalInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_suppressTextChanged) return;

            _formatTimer.Stop(); // restart timer
            _formatTimer.Tag = sender;
            _formatTimer.Start();
        }

        private void FormatTimer_Tick(object sender, EventArgs e)
        {
            _formatTimer.Stop();

            var textBox = _formatTimer.Tag as TextBox;
            if (textBox == null || string.IsNullOrWhiteSpace(textBox.Text)) return;

            int caretIndex = textBox.CaretIndex;
            string unformatted = textBox.Text.Replace(",", "");

            if (decimal.TryParse(unformatted, out decimal value))
            {
                _suppressTextChanged = true;

                textBox.Text = string.Format(CultureInfo.InvariantCulture, "{0:N}", value);
                textBox.CaretIndex = Math.Min(caretIndex, textBox.Text.Length);

                _suppressTextChanged = false;
            }
        }

        private async void FundAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isNull = Helper.AreAnyNullOrEmpty(txtAmount.Text);
                if (isNull)
                {
                    MessageBox.Show("Please enter an amount to fund.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!decimal.TryParse(txtAmount.Text.Replace(",", ""), out decimal amount))
                {
                    MessageBox.Show("Please enter a valid amount.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                LoaderOverlay.Visibility = Visibility.Visible;

                var guestAccount = new GuestAccounts
                {
                    GuestId = _guest.Id,
                    FundedBalance = amount,
                    LastFunded = DateTime.Now
                };

                await _guestRepository.FundAccountAsync(guestAccount);

                var guestTransaction = new GuestTransaction
                {
                    GuestId = _guest.Id,
                    Amount = amount,
                    TransactionType = TransactionType.Credit,
                    Description = $"Account funded with ₦{amount}",
                    Date = DateTime.Now
                };

                await _guestRepository.AddGuestTransactionAsync(guestTransaction);

                LoaderOverlay.Visibility = Visibility.Collapsed;
                MessageBox.Show("Account funded successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                LoaderOverlay.Visibility = Visibility.Collapsed;
                MessageBox.Show($"An error occurred while funding the account: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtGuest.Text = _guest.FullName;
        }
    }
}
