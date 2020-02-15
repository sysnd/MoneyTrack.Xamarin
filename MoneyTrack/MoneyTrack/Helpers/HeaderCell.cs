using Xamarin.Forms;

namespace MoneyTrack.Helpers
{
    public class HeaderCell : ViewCell
    {
        public HeaderCell()
        {
            this.Height = 25;
            var title = new Label
            {
                TextColor = Color.White,
                FontAttributes = FontAttributes.Bold,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                VerticalOptions = LayoutOptions.Center
            };

            title.SetBinding(Label.TextProperty, "Key");

            View = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 25,
                BackgroundColor = Color.FromRgb(52, 152, 218),
                Padding = 5,
                Orientation = StackOrientation.Horizontal,
                Children = { title }
            };
        }
    }
}
