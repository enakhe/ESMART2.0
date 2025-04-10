﻿using ESMART.Presentation.Forms.FrontDesk.Guest;
using ESMART.Presentation.Forms.RoomSetting;
using ESMART.Presentation.Forms.RoomSetting.Area;
using ESMART.Presentation.Forms.RoomSetting.Floor;
using ESMART.Presentation.Forms.RoomSetting.RoomType;
using Microsoft.Extensions.DependencyInjection;

namespace ESMART.Presentation
{
    public static class PresentationDependencyInjection
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddPages();
            return services;
        }

        private static IServiceCollection AddPages(this IServiceCollection services)
        {
            services.AddScoped<GuestPage>();
            services.AddScoped<AddGuestDialog>();

            services.AddScoped<RoomSettingPage>();
            services.AddScoped<AddBuildingDialog>();
            services.AddScoped<AddFloorDialog>();
            services.AddScoped<AddAreaDialog>();
            services.AddScoped<AddRoomTypeDialog>();

            return services;
        }
    }
}
