﻿using ESMART.Application.Common.Models;
using ESMART.Application.Interface;
using ESMART.Infrastructure.Repositories.FrontDesk;
using ESMART.Presentation.Forms.FrontDesk.Guest;
using ESMART.Presentation.Forms.RoomSetting.Area;
using ESMART.Presentation.Forms.RoomSetting.Building;
using ESMART.Presentation.Forms.RoomSetting.Floor;
using ESMART.Presentation.Forms.RoomSetting.RoomType;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace ESMART.Presentation.Forms.RoomSetting
{
    /// <summary>
    /// Interaction logic for RoomSettingPage.xaml
    /// </summary>
    public partial class RoomSettingPage : Page
    {
        private readonly IRoomRepository _roomRepository;
        public RoomSettingPage(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
            InitializeComponent();
        }

        public async Task LoadBuilding()
        {
            LoaderOverlay.Visibility = Visibility.Visible;
            try
            {
                var buildings = await _roomRepository.GetAllBuildings();
                BuildingDataGrid.ItemsSource = buildings;
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

        private async void AddBuildingButton_Click(object sender, RoutedEventArgs e)
        {
            var services = new ServiceCollection();
            DependencyInjection.ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            AddBuildingDialog addBuilding = serviceProvider.GetRequiredService<AddBuildingDialog>();
            if (addBuilding.ShowDialog() == true)
            {
               await LoadBuilding();
            }
        }

        private async void EditBuildingButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string Id)
            {
                var selectedBuilding = (Domain.Entities.RoomSettings.Building)BuildingDataGrid.SelectedItem;

                if (selectedBuilding.Id != null)
                {
                    var result = await _roomRepository.GetBuildingById(selectedBuilding.Id);

                    if (!result.Succeeded)
                    {
                        var sb = new StringBuilder();
                        foreach (var item in result.Errors)
                        {
                            sb.AppendLine(item);
                        }
                        MessageBox.Show(sb.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (result.Response == null)
                    {
                        MessageBox.Show("Building not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    EditBuildingDialog updateBuildingDialog = new EditBuildingDialog(_roomRepository, result.Response);
                    if (updateBuildingDialog.ShowDialog() == true)
                    {
                        _ = LoadBuilding();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a guest before editing.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private async void DeleteBuilding_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag is string Id)
                {
                    var selectedBuilding = (Domain.Entities.RoomSettings.Building)BuildingDataGrid.SelectedItem;

                    if (selectedBuilding.Id != null)
                    {
                        MessageBoxResult messageResult = MessageBox.Show("Are you sure you want to delete this building?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (messageResult == MessageBoxResult.Yes)
                        {
                            LoaderOverlay.Visibility = Visibility.Visible;
                            var result = await _roomRepository.DeleteBuilding(selectedBuilding.Id);

                            if (!result.Succeeded)
                            {
                                var sb = new StringBuilder();
                                foreach (var item in result.Errors)
                                {
                                    sb.AppendLine(item);
                                }
                                MessageBox.Show(sb.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                            await LoadBuilding();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a guest before deleting.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
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

        // Floor codes
        public async Task LoadFloor()
        {
            LoaderOverlay.Visibility = Visibility.Visible;
            try
            {
                var floors = await _roomRepository.GetAllFloors();
                FloorDataGrid.ItemsSource = floors;
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

        private async void AddFloorButton_Click(object sender, RoutedEventArgs e)
        {
            var services = new ServiceCollection();
            DependencyInjection.ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            AddFloorDialog addFloor = serviceProvider.GetRequiredService<AddFloorDialog>();
            if (addFloor.ShowDialog() == true)
            {
                await LoadFloor();
            }
        }

        private async void EditFloorButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string Id)
            {
                var selectedFloor = (Domain.Entities.RoomSettings.Floor)FloorDataGrid.SelectedItem;
                if (selectedFloor.Id != null)
                {
                    var result = await _roomRepository.GetFloorById(selectedFloor.Id);
                    if (!result.Succeeded)
                    {
                        var sb = new StringBuilder();
                        foreach (var item in result.Errors)
                        {
                            sb.AppendLine(item);
                        }
                        MessageBox.Show(sb.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (result.Response == null)
                    {
                        MessageBox.Show("Floor not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    UpdateFloorDialog updateFloorDialog = new UpdateFloorDialog(_roomRepository, result.Response);
                    if (updateFloorDialog.ShowDialog() == true)
                    {
                        _ = LoadFloor();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a guest before editing.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private async void DeleteFloor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag is string Id)
                {
                    var selectedFloor = (Domain.Entities.RoomSettings.Floor)FloorDataGrid.SelectedItem;
                    if (selectedFloor.Id != null)
                    {
                        MessageBoxResult messageResult = MessageBox.Show("Are you sure you want to delete this floor?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (messageResult == MessageBoxResult.Yes)
                        {
                            LoaderOverlay.Visibility = Visibility.Visible;
                            var result = await _roomRepository.DeleteFloor(selectedFloor.Id);
                            if (!result.Succeeded)
                            {
                                var sb = new StringBuilder();
                                foreach (var item in result.Errors)
                                {
                                    sb.AppendLine(item);
                                }
                                MessageBox.Show(sb.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                            await LoadFloor();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a guest before deleting.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
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


        // Area codes
        public async Task LoadArea()
        {
            LoaderOverlay.Visibility = Visibility.Visible;
            try
            {
                var areas = await _roomRepository.GetAllAreas();
                AreaDataGrid.ItemsSource = areas;
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

        private async void AddAreaButton_Click(object sender, RoutedEventArgs e)
        {
            var services = new ServiceCollection();
            DependencyInjection.ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            AddAreaDialog addArea = serviceProvider.GetRequiredService<AddAreaDialog>();
            if (addArea.ShowDialog() == true)
            {
                await LoadArea();
            }
        }

        private async void EditAreaButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string Id)
            {
                var selectedArea = (Domain.Entities.RoomSettings.Area)AreaDataGrid.SelectedItem;
                if (selectedArea.Id != null)
                {
                    var result = await _roomRepository.GetAreaById(selectedArea.Id);
                    if (!result.Succeeded)
                    {
                        var sb = new StringBuilder();
                        foreach (var item in result.Errors)
                        {
                            sb.AppendLine(item);
                        }
                        MessageBox.Show(sb.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (result.Response == null)
                    {
                        MessageBox.Show("Area not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    UpdateAreaDialog updateAreaDialog = new(_roomRepository, result.Response);
                    if (updateAreaDialog.ShowDialog() == true)
                    {
                        await LoadArea();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a guest before editing.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private async void DeleteArea_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag is string Id)
                {
                    var selectedArea = (Domain.Entities.RoomSettings.Area)AreaDataGrid.SelectedItem;
                    if (selectedArea.Id != null)
                    {
                        MessageBoxResult messageResult = MessageBox.Show("Are you sure you want to delete this area?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (messageResult == MessageBoxResult.Yes)
                        {
                            LoaderOverlay.Visibility = Visibility.Visible;
                            var result = await _roomRepository.DeleteArea(selectedArea.Id);
                            if (!result.Succeeded)
                            {
                                var sb = new StringBuilder();
                                foreach (var item in result.Errors)
                                {
                                    sb.AppendLine(item);
                                }
                                MessageBox.Show(sb.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                            await LoadArea();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a guest before deleting.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
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


        // Roomtype codes
        public async Task LoadRoomType()
        {
            LoaderOverlay.Visibility = Visibility.Visible;
            try
            {
                var roomTypes = await _roomRepository.GetAllRoomTypes();
                RoomTypeDataGrid.ItemsSource = roomTypes;
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

        private async void AddRoomTypeButton_Click(object sender, RoutedEventArgs e)
        {
            var services = new ServiceCollection();
            DependencyInjection.ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            AddRoomTypeDialog addRoomType = serviceProvider.GetRequiredService<AddRoomTypeDialog>();

            if (addRoomType.ShowDialog() == true)
            {
                await LoadRoomType();
            }
        }

        private async void EditRoomTypeButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string Id)
            {
                var selectedRoomType = (Domain.Entities.RoomSettings.RoomType)RoomTypeDataGrid.SelectedItem;
                if (selectedRoomType.Id != null)
                {
                    var result = await _roomRepository.GetRoomTypeById(selectedRoomType.Id);
                    if (!result.Succeeded)
                    {
                        var sb = new StringBuilder();
                        foreach (var item in result.Errors)
                        {
                            sb.AppendLine(item);
                        }
                        MessageBox.Show(sb.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (result.Response == null)
                    {
                        MessageBox.Show("Room type not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    UpdateRoomTypeDialog updateRoomTypeDialog = new(_roomRepository, result.Response);
                    if (updateRoomTypeDialog.ShowDialog() == true)
                    {
                        await LoadRoomType();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a guest before editing.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private async void DeleteRoomType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag is string Id)
                {
                    var selectedRoomType = (Domain.Entities.RoomSettings.RoomType)RoomTypeDataGrid.SelectedItem;
                    if (selectedRoomType.Id != null)
                    {
                        MessageBoxResult messageResult = MessageBox.Show("Are you sure you want to delete this room type?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (messageResult == MessageBoxResult.Yes)
                        {
                            LoaderOverlay.Visibility = Visibility.Visible;
                            var result = await _roomRepository.DeleteRoomType(selectedRoomType.Id);
                            if (!result.Succeeded)
                            {
                                var sb = new StringBuilder();
                                foreach (var item in result.Errors)
                                {
                                    sb.AppendLine(item);
                                }
                                MessageBox.Show(sb.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                            await LoadRoomType();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a guest before deleting.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
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

        private async void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl tabControl)
            {
                var selectedTab = tabControl.SelectedItem as TabItem;

                if (selectedTab == tbBuilding)
                {
                    await LoadBuilding();
                }
                else if (selectedTab == tbFloor)
                {
                    await LoadFloor();
                }
                else if (selectedTab == tbArea)
                {
                    await LoadArea();
                }
                else if (selectedTab == tbRoomType)
                {
                    await LoadRoomType();
                }
            }
        }
    }
}
