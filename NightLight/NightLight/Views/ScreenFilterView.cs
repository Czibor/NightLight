using Android.Content;
using Android.Graphics;
using Android.Views;
using NightLight.Models;

namespace NightLight.Views
{
    public class ScreenFilterView : View
    {
        public Colour Colour { get; set; }

        public ScreenFilterView(Colour colour, Context context) : base(context)
        {
            Colour = colour;
        }

        protected override void OnDraw(Canvas canvas)
        {
            if (Colour != null)
            {
                // Invalidate must be called before redrawing.
                Invalidate();
                base.OnDraw(canvas);
                canvas.DrawARGB(Colour.Alpha, Colour.Red, Colour.Green, Colour.Blue);
            }
        }
    }
}