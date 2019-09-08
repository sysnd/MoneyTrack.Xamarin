using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace MoneyTrack.ViewModels
{
    public class AboutViewModel
    {
        public AboutViewModel()
        {
            var Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

        public ICommand OpenWebCommand { get; }
    }
}