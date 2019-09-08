using MoneyTrack.Models;
using MoneyTrack.ViewModels.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyTrack.Views.Expenses
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpenseHistoryPage : ContentPage
    {
        public ExpenseViewModel expenseViewModel { get; set; }
        public ExpenseHistoryPage()
        {
            InitializeComponent();
            expenseViewModel = new ExpenseViewModel();
            BindingContext = expenseViewModel;
        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var expense = args.SelectedItem as Expense;
            if (expense == null)
                return;

            await Navigation.PushAsync(new ExpenseDetailPage(new ExpenseDetailViewModel(expense)));

            // Manually deselect item.
            ExpensesList.SelectedItem = null;
        }
        async void AddExpense_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewExpensePage()));
        }
        async void Delete_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var answer = await DisplayAlert("Delete", "Are you sure you want to delete this item?", "Yes", "No");
            if (answer)
            {
                expenseViewModel.DeleteExpenseCommand.Execute((Expense)button.CommandParameter);
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (expenseViewModel.Expenses.Count == 0)
                expenseViewModel.LoadExpensesCommand.Execute(null);
            ExpensesList.ItemsSource = expenseViewModel.Expenses;
        }
    }
}