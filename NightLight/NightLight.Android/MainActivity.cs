using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using NightLight.Utils;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace NightLight.Droid
{
    [Activity(Label = "NightLight", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            Forms.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            LoadApplication(new App());
            
            if (!Settings.CanDrawOverlays(this))
            {
                // Request permission. OnActivityResult method will be called when result is received.
                Intent intent = new Intent(Settings.ActionManageOverlayPermission);
                StartActivityForResult(intent, 0);
            }
            else
            {
                StartScreenFilterService();
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == 0)
            {
                if (Settings.CanDrawOverlays(this))
                {
                    StartScreenFilterService();
                }
            }
        }

        /// <summary>
        /// Needed by Xamarin.Essentials.
        /// </summary>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private Intent _service;

        private void StartScreenFilterService()
        {
            _service = new Intent(this, typeof(ScreenFilter));

            if (!IsServiceRunning(typeof(ScreenFilter)))
            {
                StartService(_service);
            }
        }

        private bool IsServiceRunning(Type serviceType)
        {
            ActivityManager activityManager = (ActivityManager)GetSystemService(Context.ActivityService);
            string className = Java.Lang.Class.FromType(serviceType).CanonicalName;

            foreach (ActivityManager.RunningServiceInfo serviceInfo in activityManager.GetRunningServices(int.MaxValue))
            {
                if (className == serviceInfo.Service.ClassName)
                {
                    return true;
                }
            }

            return false;
        }

        protected override void OnDestroy()
        {
            // Stop service so that BroadcastReceiver can restart ScreenFilter service.
            if (Preferences.Get("isActive", true))
            {
                StopService(_service);
            }

            base.OnDestroy();
        }
    }
}