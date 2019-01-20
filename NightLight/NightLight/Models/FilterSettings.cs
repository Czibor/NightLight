using NightLight.Utils;
using System;
using Xamarin.Essentials;

namespace NightLight.Models
{
    public class FilterSettings : NotifyPropertyChanged
    {
        public NotificationModeEnum NotificationMode { get; set; }
        public int ColourTemperature { get; set; }
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
        public byte Intensity { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public static FilterSettings LoadSettings()
        {
            FilterSettings filterSettings = new FilterSettings
            {
                NotificationMode = (NotificationModeEnum)Preferences.Get("notificationMode", 0),
                ColourTemperature = Preferences.Get("colourTemperature", 3500),
                Red = (byte)Preferences.Get("red", 255),
                Green = (byte)Preferences.Get("green", 196),
                Blue = (byte)Preferences.Get("blue", 137),
                Intensity = (byte)Preferences.Get("intensity", 40),
                StartTime = new TimeSpan(Preferences.Get("startTime", new TimeSpan(22, 0, 0).Ticks)),
                EndTime = new TimeSpan(Preferences.Get("endTime", new TimeSpan(4, 0, 0).Ticks))
            };

            return filterSettings;
        }
        
        public void SaveSettings()
        {
            Preferences.Set("notificationMode", (int)NotificationMode);
            Preferences.Set("colourTemperature", ColourTemperature);
            Preferences.Set("red", Red);
            Preferences.Set("green", Green);
            Preferences.Set("blue", Blue);
            Preferences.Set("intensity", Intensity);
            Preferences.Set("startTime", StartTime.Ticks);
            Preferences.Set("endTime", EndTime.Ticks);
        }
    }
}