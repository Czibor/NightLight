using Android.App;
using Android.Content;
using Android.Support.V4.App;
using NightLight.Utils;

[assembly: Xamarin.Forms.Dependency(typeof(NightLight.Droid.Notification))]
namespace NightLight.Droid
{
    internal class Notification : INotification
    {
        public void CreateNotification(Context context, string title, string text)
        {
            Intent intent = context.PackageManager.GetLaunchIntentForPackage(context.PackageName);
            intent.AddFlags(ActivityFlags.ClearTop);
            PendingIntent pendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.UpdateCurrent);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(Application.Context);
            builder.SetContentTitle(title);
            builder.SetContentText(text);
            builder.SetSmallIcon(Resource.Mipmap.icon);
            builder.SetContentIntent(pendingIntent);

            NotificationManager notificationManager = (NotificationManager)Application.Context.GetSystemService(Context.NotificationService);
            notificationManager.Notify(0, builder.Build());
        }

        public void RemoveNotification()
        {
            NotificationManager notificationManager = (NotificationManager)Application.Context.GetSystemService(Context.NotificationService);
            notificationManager.Cancel(0);
        }
    }
}