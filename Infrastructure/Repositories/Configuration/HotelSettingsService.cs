﻿using ESMART.Application.Common.Interface;
using ESMART.Domain.Entities.Configuration;
using ESMART.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ESMART.Infrastructure.Repositories.Configuration
{
    public class HotelSettingsService(IDbContextFactory<ApplicationDbContext> contextFactory) : IHotelSettingsService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory = contextFactory;

        public async Task<List<HotelSetting>> GetSettingsByCategoryAsync(string categoryName)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var category = await context.SettingsCategories
                                         .FirstOrDefaultAsync(c => c.Name == categoryName);

                if (category == null) return [];

                return await context.HotelSettings
                    .Where(s => s.CategoryId == category.Id)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when getting settings by category. " + ex.Message);
            }
        }

        public async Task<HotelSetting?> GetSettingAsync(string key)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                return await context.HotelSettings
                    .FirstOrDefaultAsync(s => s.Key == key);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when getting setting. " + ex.Message);
            }
        }

        public async Task<bool> UpdateSettingAsync(HotelSetting setting)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                context.HotelSettings.Update(setting);
                return await context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when updating setting. " + ex.Message);
            }
        }

        public async Task AddHotelSettingAsync(HotelSetting setting)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                await context.HotelSettings.AddAsync(setting);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when adding setting. " + ex.Message);
            }
        }

        public async Task SeedHotelSettingAsync()
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var category = await context.SettingsCategories
                    .FirstOrDefaultAsync(c => c.Name == "Financial Settings");

                if (category == null)
                {
                    category = new SettingsCategory
                    {
                        Name = "Financial Settings"
                    };

                    await context.SettingsCategories.AddAsync(category);
                    await context.SaveChangesAsync();
                }

                // Check for existing settings under this category
                var existingSettings = await context.HotelSettings
                    .Where(s => s.CategoryId == category.Id)
                    .Select(s => s.Key)
                    .ToListAsync();

                var defaultSettings = new List<HotelSetting>();

                void AddSettingIfNotExists(string key, string value, string dataType, string description)
                {
                    if (!existingSettings.Contains(key))
                    {
                        defaultSettings.Add(new HotelSetting
                        {
                            CategoryId = category.Id,
                            Key = key,
                            Value = value,
                            DataType = dataType,
                            Description = description,
                            IsActive = true,
                            UpdatedAt = DateTime.Now,
                            UpdatedBy = "System"
                        });
                    }
                }

                AddSettingIfNotExists("VAT", "7.5", "decimal", "VAT Rate (%)");
                AddSettingIfNotExists("Discount", "0.0", "decimal", "Decimal (%)");
                AddSettingIfNotExists("ServiceCharge", "10", "decimal", "Service Charge Rate (%)");
                AddSettingIfNotExists("CurrencySymbol", "NGN", "string", "Default Currency Symbol");

                if (defaultSettings.Any())
                {
                    await context.HotelSettings.AddRangeAsync(defaultSettings);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when seeding hotel settings: " + ex.Message);
            }
        }

        public async Task<bool> UpdateHotelInformation(Hotel hotel)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                context.Hotels.Update(hotel);
                return await context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when updating hotel information: " + ex.Message);
            }
        }

        public async Task SeedHotelInformation()
        {
            try
            {
                var context = _contextFactory.CreateDbContext();
                var hotel = await context.Hotels.FirstOrDefaultAsync();
                if (hotel == null)
                {
                    var newHotel = new Hotel()
                    {
                        Name = "Default Hotel Name",
                        Address = "Default Hotel Address",
                        PhoneNumber = "+2349000000000",
                        Email = "info@example.com",
                    };

                    await context.Hotels.AddAsync(newHotel);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when seeding hotel information: " + ex.Message);
            }
        }

        public async Task<Hotel?> GetHotelInformation()
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                return await context.Hotels.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when getting hotel information: " + ex.Message);
            }
        }
    }

}
