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
    public partial class ExpenseDetailPage : ContentPage
    {
        public ExpenseDetailViewModel viewModel { get; set; }
        public ExpenseDetailPage()
        {
            InitializeComponent();
        }
        public ExpenseDetailPage(ExpenseDetailViewModel expenseDetailViewModel)
        {
            InitializeComponent();
            this.viewModel = expenseDetailViewModel;
            BindingContext = this.viewModel;
            categoryPicker.ItemsSource = this.viewModel.CategoryNames;
            categoryPicker.SelectedItem = this.viewModel.Expense.CategoryName;
        }
        async void Update_Clicked(object sender, EventArgs e)
        {
            viewModel.Expense.CategoryName = categoryPicker.SelectedItem.ToString();
            viewModel.Expense.Category = viewModel.Categories.FirstOrDefault(x => x.Name == viewModel.Expense.CategoryName);
            viewModel.Expense.CategoryId = viewModel.Expense.Category.Id;            
            MessagingCenter.Send(this, "UpdateExpense", this.viewModel.Expense);
            await Navigation.PopAsync();
        }
        void OnDateSelected(object sender, DateChangedEventArgs args)
        {
            var datePicker = (DatePicker)sender;
            viewModel.Expense.Date = datePicker.Date;
        }
    }
}