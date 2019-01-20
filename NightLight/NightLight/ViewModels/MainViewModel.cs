using Android.Graphics;
using NightLight.Models;
using NightLight.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;

namespace NightLight.ViewModels
{
    public class MainViewModel : NotifyPropertyChanged
    {
        public MainViewModel()
        {
            LoadSettings();
        }

        private bool _isActive;
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
                RaisePropertyChanged();

                if (ScreenFilter.Instance != null)
                {
                    // Turning service on/off.
                    ScreenFilter.Instance.IsActive = IsActive;
                }
            }
        }
        
        public List<NotificationModeEnum> NotificationModes
        {
            get
            {
                return Enum.GetValues(typeof(NotificationModeEnum)).Cast<NotificationModeEnum>().ToList();
            }
        }

        private NotificationModeEnum _notificationMode;
        public NotificationModeEnum NotificationMode
        {
            get
            {
                return _notificationMode;
            }
            set
            {
                _notificationMode = value;
                RaisePropertyChanged();
            }
        }

        private int _colourTemperature;
        public int ColourTemperature
        {
            get
            {
                return _colourTemperature;
            }
            set
            {
                int number = ((int)Math.Round(value / 10.0)) * 10;

                if (ColourTemperature != number)
                {
                    _colourTemperature = number;
                    RaisePropertyChanged();
                    ApplyColourTemperature();
                }
            }
        }

        private void ApplyColourTemperature()
        {
            if (ScreenFilter.ScreenFilterView != null)
            {
                ScreenFilter.ScreenFilterView.Colour = new Colour(ColourTemperature);
                ScreenFilter.ScreenFilterView.Colour.Alpha = Intensity;
                ScreenFilter.ScreenFilterView.Draw(new Canvas());

                Red = ScreenFilter.ScreenFilterView.Colour.Red;
                Green = ScreenFilter.ScreenFilterView.Colour.Green;
                Blue = ScreenFilter.ScreenFilterView.Colour.Blue;
            }
        }
        
        private byte _red;
        public byte Red
        {
            get
            {
                return _red;
            }
            set
            {
                _red = value;
                RaisePropertyChanged();

                if (ScreenFilter.ScreenFilterView?.Colour != null)
                {
                    if (ScreenFilter.ScreenFilterView.Colour.Red != Red)
                    {
                        ScreenFilter.ScreenFilterView.Colour.Red = Red;
                        ScreenFilter.ScreenFilterView.Draw(new Canvas());
                    }
                }
            }
        }

        private byte _green;
        public byte Green
        {
            get
            {
                return _green;
            }
            set
            {
                _green = value;
                RaisePropertyChanged();

                if (ScreenFilter.ScreenFilterView?.Colour != null)
                {
                    if (ScreenFilter.ScreenFilterView.Colour.Green != Green)
                    {
                        ScreenFilter.ScreenFilterView.Colour.Green = Green;
                        ScreenFilter.ScreenFilterView.Draw(new Canvas());
                    }
                }
            }
        }

        private byte _blue;
        public byte Blue
        {
            get
            {
                return _blue;
            }
            set
            {
                _blue = value;
                RaisePropertyChanged();

                if (ScreenFilter.ScreenFilterView?.Colour != null)
                {
                    if (ScreenFilter.ScreenFilterView.Colour.Blue != Blue)
                    {
                        ScreenFilter.ScreenFilterView.Colour.Blue = Blue;
                        ScreenFilter.ScreenFilterView.Draw(new Canvas());
                    }
                }
            }
        }

        private byte _intensity;
        public byte Intensity
        {
            get
            {
                return _intensity;
            }
            set
            {
                _intensity = value;
                RaisePropertyChanged();

                if (ScreenFilter.ScreenFilterView?.Colour?.Alpha != null)
                {
                    if (ScreenFilter.ScreenFilterView.Colour.Alpha != Intensity)
                    {
                        ScreenFilter.ScreenFilterView.Colour.Alpha = Intensity;
                        ScreenFilter.ScreenFilterView.Draw(new Canvas());
                    }
                }
            }
        }

        private TimeSpan _startTime;
        public TimeSpan StartTime
        {
            get
            {
                return _startTime;
            }
            set
            {
                _startTime = value;
                RaisePropertyChanged();
            }
        }

        private TimeSpan _endTime;
        public TimeSpan EndTime
        {
            get
            {
                return _endTime;
            }
            set
            {
                _endTime = value;
                RaisePropertyChanged();
            }
        }

        private RelayCommand _saveChangesCommand;
        public ICommand SaveChangesCommand
        {
            get
            {
                return _saveChangesCommand ?? (_saveChangesCommand = new RelayCommand(param => SaveChanges()));
            }
        }

        private RelayCommand _undoChangesCommand;
        public ICommand UndoChangesCommand
        {
            get
            {
                return _undoChangesCommand ?? (_undoChangesCommand = new RelayCommand(param => UndoChanges()));
            }
        }
        
        public void SaveChanges()
        {
            FilterSettings filterSettings = new FilterSettings
            {
                NotificationMode = this.NotificationMode,
                ColourTemperature = this.ColourTemperature,
                Red = this.Red,
                Green = this.Green,
                Blue = this.Blue,
                Intensity = this.Intensity,
                StartTime = this.StartTime,
                EndTime = this.EndTime
            };

            filterSettings.SaveSettings();
            ScreenFilter.Instance.FilterSettings = FilterSettings.LoadSettings();
        }

        public void UndoChanges()
        {
            FilterSettings filterSettings = FilterSettings.LoadSettings();
            NotificationMode = filterSettings.NotificationMode;
            ColourTemperature = filterSettings.ColourTemperature;
            Red = filterSettings.Red;
            Green = filterSettings.Green;
            Blue = filterSettings.Blue;
            Intensity = filterSettings.Intensity;
            StartTime = filterSettings.StartTime;
            EndTime = filterSettings.EndTime;
        }

        private void LoadSettings()
        {
            FilterSettings filterSettings = FilterSettings.LoadSettings();
            _isActive = Preferences.Get("isActive", true);
            _notificationMode = filterSettings.NotificationMode;
            _colourTemperature = filterSettings.ColourTemperature;
            _red = filterSettings.Red;
            _green = filterSettings.Green;
            _blue = filterSettings.Blue;
            _intensity = filterSettings.Intensity;
            _startTime = filterSettings.StartTime;
            _endTime = filterSettings.EndTime;
        }
    }
}