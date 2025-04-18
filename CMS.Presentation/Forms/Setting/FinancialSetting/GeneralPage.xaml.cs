﻿using ESMART.Application.Common.Interface;
using ESMART.Application.Common.Utils;
using ESMART.Domain.Entities.Configuration;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ESMART.Presentation.Forms.Setting.FinancialSetting
{
    /// <summary>
    /// Interaction logic for General.xaml
    /// </summary>
    public partial class General : Page
    {
        private readonly IHotelSettingsService _hotelSettingsService;
        public General(IHotelSettingsService hotelSettingsService)
        {
            _hotelSettingsService = hotelSettingsService;
            InitializeComponent();
        }

        private void LoadCurrencyOption()
        {
            var currencies = new List<CurrencyOption>
            {
                new CurrencyOption { Symbol = "₦", Code = "NGN" },
                new CurrencyOption { Symbol = "$", Code = "USD" },
                new CurrencyOption { Symbol = "€", Code = "EUR" },
                new CurrencyOption { Symbol = "£", Code = "GBP" },
                new CurrencyOption { Symbol = "¥", Code = "JPY" },
                new CurrencyOption { Symbol = "₵", Code = "GHS" },
                new CurrencyOption { Symbol = "R", Code = "ZAR" },
            };

            cmbCurrency.ItemsSource = currencies;
            cmbCurrency.DisplayMemberPath = "Display";
            cmbCurrency.SelectedValuePath = "Code";
        }

        private async Task LoadFiancialData()
        {
            try
            {
                LoaderOverlay.Visibility = Visibility.Visible;
                var financialSettings = await _hotelSettingsService.GetSettingsByCategoryAsync("Financial Settings");
                if (financialSettings != null)
                {
                    foreach (var setting in financialSettings)
                    {
                        switch (setting.Key)
                        {
                            case "VAT":
                                txtVAT.Text = setting.Value;
                                break;
                            case "ServiceCharge":
                                txtServicCharge.Text = setting.Value;
                                break;
                            case "Discount":
                                txtDiscount.Text = setting.Value;
                                break;
                            case "CurrencySymbol":
                                cmbCurrency.SelectedValue = setting.Value;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading financial data: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                LoaderOverlay.Visibility = Visibility.Collapsed;
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoaderOverlay.Visibility = Visibility.Visible;

                var vat = txtVAT.Text.Replace("%", "").Trim();
                var serviceCharge = txtServicCharge.Text.Replace("%", "").Trim();
                var discount = txtDiscount.Text;
                var currencySymbol = cmbCurrency.SelectedValue.ToString();

                var vatSetting = await _hotelSettingsService.GetSettingAsync("VAT");
                var serviceChargeSetting = await _hotelSettingsService.GetSettingAsync("ServiceCharge");
                var discountSetting = await _hotelSettingsService.GetSettingAsync("Discount");
                var currencySetting = await _hotelSettingsService.GetSettingAsync("CurrencySymbol");

                if (vatSetting == null || serviceChargeSetting == null || discountSetting == null || currencySetting == null)
                {
                    MessageBox.Show("One or more settings not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                vatSetting.Value = vat;
                serviceChargeSetting.Value = serviceCharge;
                discountSetting.Value = discount;
                currencySetting.Value = currencySymbol!;

                await _hotelSettingsService.UpdateSettingAsync(vatSetting);
                await _hotelSettingsService.UpdateSettingAsync(serviceChargeSetting);
                await _hotelSettingsService.UpdateSettingAsync(discountSetting);
                await _hotelSettingsService.UpdateSettingAsync(currencySetting);

                MessageBox.Show("Financial settings updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                await LoadFiancialData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while saving financial settings: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                LoaderOverlay.Visibility = Visibility.Collapsed;
            }
        }

        // VAT
        private void VatTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputFormatter.AllowDecimalOnly(sender, e);
        }

        private void VatTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            InputFormatter.FormatAsPercentageOnLostFocus(sender, e);
        }

        private void VatTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            InputFormatter.StripPercentageOnGotFocus(sender, e);
        }


        // Service Charge
        private void ServiceChargeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputFormatter.AllowDecimalOnly(sender, e);
        }

        private void ServiceChargeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            InputFormatter.FormatAsPercentageOnLostFocus(sender, e);
        }

        private void ServiceChargeTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            InputFormatter.StripPercentageOnGotFocus(sender, e);
        }


        // Discount
        private void DiscountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputFormatter.AllowDecimalOnly(sender, e);
        }

        private void DiscountTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            InputFormatter.FormatAsDecimalOnLostFocus(sender, e);
        }

        private void DiscountTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            InputFormatter.StripPercentageOnGotFocus(sender, e);
        }

        private void txtDiscount_GotFocus(object sender, RoutedEventArgs e)
        {
            InputFormatter.StripPercentageOnGotFocus(sender, e);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCurrencyOption();
            await LoadFiancialData();
        }
    }
}
