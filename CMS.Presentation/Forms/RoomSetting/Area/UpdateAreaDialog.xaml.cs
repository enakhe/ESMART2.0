﻿using ESMART.Application.Common.Utils;
using ESMART.Application.Interface;
using ESMART.Domain.Entities.RoomSettings;
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
using System.Windows.Shapes;

namespace ESMART.Presentation.Forms.RoomSetting.Area
{
    /// <summary>
    /// Interaction logic for UpdateAreaDialog.xaml
    /// </summary>
    public partial class UpdateAreaDialog : Window
    {
        private readonly IRoomRepository _roomRepository;
        private readonly Domain.Entities.RoomSettings.Area _area;
        public UpdateAreaDialog(IRoomRepository roomRepository, Domain.Entities.RoomSettings.Area area)
        {
            _roomRepository = roomRepository;
            _area = area;
            InitializeComponent();
        }

        private void LoadArea()
        {
            try
            {
                LoaderOverlay.Visibility = Visibility.Visible;

                txtAreaName.Text = _area.Name;
                txtAreaNumber.Text = _area.Number?.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally
            {
                LoaderOverlay.Visibility = Visibility.Collapsed;
            }
        }

        private async void UpdateArea_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isNull = Helper.AreAnyNullOrEmpty(txtAreaName.Text, txtAreaNumber.Text);
                if (isNull)
                {
                    MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(txtAreaNumber.Text, out int areaNumber))
                {
                    MessageBox.Show("Please enter a valid area number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                LoaderOverlay.Visibility = Visibility.Visible;

                var areaName = txtAreaName.Text;
                var areaNo = txtAreaNumber.Text;

                _area.Name = areaName;
                _area.Number = areaNo;

                await _roomRepository.UpdateArea(_area);
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                LoaderOverlay.Visibility = Visibility.Collapsed;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void NumberOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private bool IsTextNumeric(string text)
        {
            return text.All(char.IsDigit);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            LoadArea();
        }
    }
}
