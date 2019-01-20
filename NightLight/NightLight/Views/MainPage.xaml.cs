using Xamarin.Forms;

namespace NightLight.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Text must be a number between 0 and 255.
        /// </summary>
        private void ByteEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                bool isParsed = uint.TryParse(e.NewTextValue, out uint number);

                if (!isParsed)
                {
                    (sender as Entry).Text = e.OldTextValue;
                }
                else if (number > byte.MaxValue)
                {
                    (sender as Entry).Text = byte.MaxValue.ToString();
                }
            }
        }

        /// <summary>
        /// Text must be a number between 0 and 100.
        /// </summary>
        private void PercentEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                bool isParsed = uint.TryParse(e.NewTextValue, out uint number);

                if (!isParsed)
                {
                    (sender as Entry).Text = e.OldTextValue;
                }
                else if (number > 100)
                {
                    (sender as Entry).Text = "100";
                }
            }
        }
    }
}