﻿using ESMART.Application.Interface;
using ESMART.Domain.Entities.FrontDesk;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace ESMART.Presentation.Forms.FrontDesk.Guest
{
    public partial class GuestPage : Page
    {
        private readonly IGuestRepository _guestRepository;
        public GuestPage(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
            InitializeComponent();
        }

        public async Task LoadGuests()
        {
            LoaderOverlay.Visibility = Visibility.Visible;
            try
            {
                var guests = await _guestRepository.GetAllGuestsAsync();
                GuestDataGrid.ItemsSource = guests;
                txtGuestCount.Text = guests.Count.ToString();
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

        private async void AddGuest_Click(object sender, RoutedEventArgs e)
        {
            var services = new ServiceCollection();
            DependencyInjection.ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            AddGuestDialog addGuestDialog = serviceProvider.GetRequiredService<AddGuestDialog>();
            if (addGuestDialog.ShowDialog() == true)
            {
                await LoadGuests();
            }
        }

        private async void SearchGuest_Click(object sender, RoutedEventArgs e)
        {
            LoaderOverlay.Visibility = Visibility.Visible;
            try
            {
                var searchText = txtSearch.Text.Trim();
                if (string.IsNullOrEmpty(searchText) || searchText == "Search")
                {
                    await LoadGuests();
                    return;
                }
                var guests = await _guestRepository.SearchGuestAsync(searchText);
                GuestDataGrid.ItemsSource = guests;
                txtGuestCount.Text = guests.Count.ToString();
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

        private async void GuestDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadGuests();
        }

        private void EditGuest_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string Id)
            {
                var selectedGuest = (Domain.ViewModels.FrontDesk.GuestViewModel)GuestDataGrid.SelectedItem;

                if (selectedGuest.Id != null)
                {
                    UpdateGuestDialog updateGuestDialog = new UpdateGuestDialog(selectedGuest.Id, _guestRepository);
                    if (updateGuestDialog.ShowDialog() == true)
                    {
                        _ = LoadGuests();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a guest before editing.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private async void ViewGuest_Click(Object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string Id)
            {
                var selectedGuest = (Domain.Entities.FrontDesk.Guest)GuestDataGrid.SelectedItem;
                if (selectedGuest.Id != null)
                {
                    GuestDetailsDialog viewGuestDialog = new GuestDetailsDialog(selectedGuest.Id, _guestRepository);
                    if (viewGuestDialog.ShowDialog() == true)
                    {
                        await LoadGuests();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a guest before viewing.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private async void DeleteGuest_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string Id)
            {
                var selectedGuest = (Domain.ViewModels.FrontDesk.GuestViewModel)GuestDataGrid.SelectedItem;
                if (selectedGuest.Id != null)
                {
                    LoaderOverlay.Visibility = Visibility.Visible;
                    try
                    {
                        MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this guest?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (result == MessageBoxResult.Yes)
                        {
                            await _guestRepository.DeleteGuestAsync(selectedGuest.Id);
                            await LoadGuests();
                        }
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
                else
                {
                    MessageBox.Show("Please select a guest before deleting.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "Search")
            {
                txtSearch.Text = "";
                txtSearch.Foreground = Brushes.Black;
            }
        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search";
                txtSearch.Foreground = Brushes.Gray;
            }
        }
    }
}
