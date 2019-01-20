namespace NightLight.Utils
{
    public interface INotification
    {
        void CreateNotification(Android.Content.Context context, string title, string text);
        void RemoveNotification();
    }
}