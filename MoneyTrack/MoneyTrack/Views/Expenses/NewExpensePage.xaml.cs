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
            Expense = new Expense()
            {
                Name = "Shoes",
                Date = DateTime.Now,
                Value = 0,
                Category = new Category() { Name = "Clothes" },
                CategoryName = "Clothes"
            };
            Today = DateTime.Now.Date.ToLocalTime();
            BindingContext = this;
            catViewModel = new CategoriesViewModel();
            Categories = catViewModel.GetAllCategories().ToList();
            PopulateCategoryNames();
            categoryPicker.ItemsSource = CategoryNames;
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
            Expense.DisplayName = $"{Expense.Name}-{Expense.Date.ToString("dd/MM/yyyy")}";
            Expense.Category = Categories.FirstOrDefault(x => x.Name == Expense.CategoryName);
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