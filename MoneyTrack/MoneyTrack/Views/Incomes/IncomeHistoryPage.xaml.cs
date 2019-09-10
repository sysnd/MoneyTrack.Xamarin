using MoneyTrack.Models;
using MoneyTrack.ViewModels.Incomes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyTrack.Views.Incomes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncomeHistoryPage : ContentPage
    {
        public IncomeViewModel incomeViewModel { get; set; }
        public IncomeHistoryPage()
        {
            InitializeComponent();
            incomeViewModel = IncomeViewModel.GetInstance();
            BindingContext = incomeViewModel;
        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var income = args.SelectedItem as Income;
            if (income == null)
                return;

            await Navigation.PushAsync(new IncomeDetailPage(new IncomeDetailViewModel(income)));

            // Manually deselect item.
            IncomesList.SelectedItem = null;
        }
        async void AddIncome_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewIncomePage()));
        }
        async void Delete_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var answer = await DisplayAlert("Delete", "Are you sure you want to delete this item?", "Yes", "No");
            if (answer)
            {
                incomeViewModel.DeleteIncomeCommand.Execute((Income)button.CommandParameter);
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            incomeViewModel.LoadIncomesCommand.Execute(null);
            IncomesList.ItemsSource = incomeViewModel.Incomes;
        }
    }
}