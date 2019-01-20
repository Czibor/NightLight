using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Runtime;
using NightLight.Models;
using NightLight.Views;
using System;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NightLight.Utils
{
    [Service]
    public class ScreenFilter : Service
    {
        private Timer _timer;
        public bool IsFiltering { get; set; }
        public static ScreenFilter Instance { get; set; }
        public FilterSettings FilterSettings { get; set; }
        public static ScreenFilterView ScreenFilterView { get; set; }
        public WindowManagerLayoutParams LayoutParams { get; set; }

        private IWindowManager _windowManager;
        public IWindowManager WindowManager
        {
            get
            {
                return _windowManager ?? (_windowManager = GetSystemService(WindowService).JavaCast<IWindowManager>());
            }
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
                Preferences.Set("isActive", IsActive);

                if (IsActive)
                {
                    TurnOnScreenFilter();
                }
                else
                {
                    TurnOffScreenFilter();
                }
            }
        }

        public override void OnCreate()
        {
            Instance = this;
            FilterSettings = FilterSettings.LoadSettings();
            _isActive = Preferences.Get("isActive", true);
            _timer = new Timer();
            _timer.Elapsed += new ElapsedEventHandler(OnTimerElapsed);
            _timer.Interval = 60000;

            if (IsActive)
            {
                TurnOnScreenFilter();
            }

            base.OnCreate();
        }

        private void OnTimerElapsed(object source, ElapsedEventArgs e)
        {
            bool isFilterTime = IsFilterTime();

            if (isFilterTime && !IsFiltering)
            {
                // Timer thread wouldn't be able to add view to WindowManager.
                Device.BeginInvokeOnMainThread(() => { AddScreenFilter(); });
            }
            else if (!isFilterTime && IsFiltering)
            {
                Device.BeginInvokeOnMainThread(() => { RemoveScreenFilter(); });
            }
        }

        private void TurnOnScreenFilter()
        {
            _timer.Enabled = true;
            
            if (IsFilterTime())
            {
                AddScreenFilter();
            }
        }

        private void TurnOffScreenFilter()
        {
            _timer.Enabled = false;
            RemoveScreenFilter();
        }
        
        private void AddScreenFilter()
        {
            if (ScreenFilterView?.WindowToken == null)
            {
                Colour colour = new Colour(FilterSettings.Intensity, FilterSettings.Red, FilterSettings.Green, FilterSettings.Blue);
                ScreenFilterView = new ScreenFilterView(colour, this);

                LayoutParams = new WindowManagerLayoutParams(
                    // ApplicationOverlay will be needed for Android 8.0 and above.
                    Build.VERSION.SdkInt < BuildVersionCodes.N ? WindowManagerTypes.SystemOverlay : WindowManagerTypes.SystemOverlay,
                    WindowManagerFlags.NotFocusable | WindowManagerFlags.NotTouchModal | WindowManagerFlags.NotTouchable | WindowManagerFlags.Fullscreen | WindowManagerFlags.LayoutInScreen,
                    Format.Translucent);

                WindowManager.AddView(ScreenFilterView, LayoutParams);
            }
            
            if (FilterSettings.NotificationMode != NotificationModeEnum.Never)
            {
                DependencyService.Get<INotification>().CreateNotification(this.ApplicationContext, "NightLight is running", "Tap here to open the application.");
            }

            IsFiltering = true;
        }

        private void RemoveScreenFilter()
        {
            if (ScreenFilterView?.WindowToken != null)
            {
                WindowManager.RemoveView(ScreenFilterView);
            }

            if (FilterSettings.NotificationMode == NotificationModeEnum.Always)
            {
                DependencyService.Get<INotification>().CreateNotification(this.ApplicationContext, "NightLight is paused", "Tap here to open the application.");
            }
            else
            {
                DependencyService.Get<INotification>().RemoveNotification();
            }

            IsFiltering = false;
        }

        private bool IsFilterTime()
        {
            bool isFilterTime = false;
            TimeSpan startTime = FilterSettings.StartTime;
            TimeSpan endTime = FilterSettings.EndTime;
            TimeSpan currentTime = DateTime.Now.TimeOfDay;

            if (startTime >= endTime)
            {
                if (currentTime > startTime && currentTime > endTime)
                {
                    isFilterTime = true;
                }
            }
            else
            {
                if (currentTime > startTime && currentTime < endTime)
                {
                    isFilterTime = true;
                }
            }

            return isFilterTime;
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            RemoveScreenFilter();
            Intent broadcastReceiver = new Intent(this, typeof(ScreenFilterBroadcastReceiver));
            SendBroadcast(broadcastReceiver);
        }
    }
}