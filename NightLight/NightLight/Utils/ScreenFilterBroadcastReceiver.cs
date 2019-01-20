using Android.Content;

namespace NightLight.Utils
{
    [BroadcastReceiver]
    public class ScreenFilterBroadcastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            context.StartService(new Intent(context, typeof(ScreenFilter)));
        }
    }
}