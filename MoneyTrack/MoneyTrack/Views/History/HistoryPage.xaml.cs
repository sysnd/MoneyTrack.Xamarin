using MoneyTrack.Helpers;
using MoneyTrack.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyTrack.Views.History
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryViewModel HistoryViewModel { get; set; }
        public HistoryPage()
        {
            InitializeComponent();
            HistoryViewModel = new HistoryViewModel();
            BindingContext = HistoryViewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            HistoryViewModel.LoadHistoryItemsCommand.Execute(null);
            HistoryItemsList.ItemsSource = HistoryViewModel.ItemsGrouped;
            HistoryItemsList.IsGroupingEnabled = true;
            HistoryItemsList.GroupDisplayBinding = new Binding("Key");
            HistoryItemsList.GroupHeaderTemplate = new DataTemplate(typeof(HeaderCell));
        }
    }
}