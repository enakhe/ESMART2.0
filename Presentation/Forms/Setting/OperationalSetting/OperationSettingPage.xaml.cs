﻿using ESMART.Application.Common.Interface;
using ESMART.Domain.Entities.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace ESMART.Presentation.Forms.Setting.OperationalSetting
{
    /// <summary>
    /// Interaction logic for OperationSettingPage.xaml
    /// </summary>
    public partial class OperationSettingPage : Page
    {
        private readonly IHotelSettingsService _hotelSettingsService;
        private readonly IBackupRepository _backupRepository;
        public OperationSettingPage(IHotelSettingsService hotelSettingsService, IBackupRepository backupRepository)
        {
            _hotelSettingsService = hotelSettingsService;
            _backupRepository = backupRepository;
            InitializeComponent();
        }

        private async Task LoadLockSetting()
        {
            LoaderOverlay.Visibility = Visibility.Visible;
            try
            {
                var operationSettings = await _hotelSettingsService.GetSettingsByCategoryAsync("Operation Settings");
                if (operationSettings != null)
                {
                    foreach (var setting in operationSettings)
                    {
                        switch (setting.Key)
                        {
                            case "LockType":
                                cmbLockType.Text = setting.Value;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading lock type settings: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                LoaderOverlay.Visibility = Visibility.Collapsed;
            }
        }

        private async Task LoadBackupSetting()
        {
            LoaderOverlay.Visibility = Visibility.Visible;
            try
            {
                var backupSetting = await _hotelSettingsService.GetSettingAsync("Backup");
                if (backupSetting != null)
                {
                    cmbBackUp.Text = backupSetting.Value;
                    var userBackUpSetting = await _backupRepository.GetBackupSettingsAsync();

                    if (userBackUpSetting != null)
                    {
                        txtLastBackUp.Text = $"Last backup: {userBackUpSetting.LastBackup.ToString("dd/MM/yyyy")}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading backup settings: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                LoaderOverlay.Visibility = Visibility.Collapsed;
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            LoaderOverlay.Visibility = Visibility.Visible;
            try
            {
                var lockType = cmbLockType.Text;
                var backupFrequency = cmbBackUp.Text;

                var setting = await _hotelSettingsService.GetSettingAsync("LockType");
                var backupSetting = await _hotelSettingsService.GetSettingAsync("Backup");

                if (setting != null || backupSetting != null)
                {
                    setting.Value = lockType;
                    backupSetting.Value = backupFrequency;

                    var result = await _hotelSettingsService.UpdateSettingAsync(setting);
                    var backupResult = await _hotelSettingsService.UpdateSettingAsync(backupSetting);

                    if (result || backupResult)
                    {
                        var userBackUp = await _backupRepository.GetBackupSettingsAsync();
                        Enum.TryParse(backupFrequency, out BackupFrequency frequency);

                        userBackUp.Frequency = frequency;

                        await _backupRepository.UpdateBackupSettingsAsync(userBackUp);

                        MessageBox.Show("Settings saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to save settings.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while saving settings: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                LoaderOverlay.Visibility = Visibility.Collapsed;
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadLockSetting();
            await LoadBackupSetting();
        }
    }
}
