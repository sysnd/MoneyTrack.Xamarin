using MoneyTrack.Models;
using MoneyTrack.ViewModels.Categories;
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
    public partial class NewExpensePage : ContentPage
    {
        public Expense Expense { get; set; }
        public DateTime Today { get; set; }
        public int Index { get; set; }
        public List<Category> Categories { get; set; }
        public List<string> CategoryNames { get; set; }
        public CategoriesViewModel catViewModel { get; set; }
        public string ValueString
        {
            get
            {
                return Expense.Value.ToString();
            }
            set
            {
                decimal.TryParse(value, out decimal _value);
                Expense.Value = _value;
            }
        }
        public NewExpensePage()
        {
            InitializeComponent();
            catViewModel = CategoriesViewModel.GetInstance();
            Categories = catViewModel.GetAllCategoriesAsync().Result.ToList();
            Expense = new Expense()
            {
                Name = "",
                Date = DateTime.Now,
                Value = 0,
                Category = null,
                CategoryName = Categories.FirstOrDefault().Name
            };
            Today = DateTime.Now.Date.ToLocalTime();
            BindingContext = this;
            PopulateCategoryNames();
            categoryPicker.ItemsSource = CategoryNames;
            SetSelectedItem();
        }

        private void SetSelectedItem()
        {
            Index = -1;
        }

        private void PopulateCategoryNames()
        {
            CategoryNames = new List<string>();
            foreach (var item in Categories)
            {
                CategoryNames.Add(item.Name);
            }
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (Index < 0)
            {
                await DisplayAlert("Category Missing", "Please select a category", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(Expense.Name))
            {
                await DisplayAlert("Name Missing", "Please fill in the Name field", "OK");
                return;
            }
            if(Expense.Value<=0)
            {
                await DisplayAlert("Value Missing", "Please fill in the Value field", "OK");
                return;
            }
            Expense.DisplayName = $"{Expense.Name}-{Expense.Date.ToString("dd/MM/yyyy")}";
            Expense.Category = Categories[Index];
            Expense.CategoryId = Expense.Category.Id;
            MessagingCenter.Send(this, "AddExpense", Expense);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        void OnDateSelected(object sender, DateChangedEventArgs args)
        {
            var datePicker = (DatePicker)sender;
            Expense.Date = datePicker.Date;
        }
    }
}