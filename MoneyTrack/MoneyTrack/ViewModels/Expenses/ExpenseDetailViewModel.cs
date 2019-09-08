using MoneyTrack.Models;
using MoneyTrack.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoneyTrack.ViewModels.Expenses
{
    public class ExpenseDetailViewModel : BaseViewModel<Expense>
    {
        public Expense Expense { get; set; }
        public List<Category> Categories { get; set; }
        public List<string> CategoryNames { get; set; }
        public CategoriesViewModel catViewModel { get; set; }
        public DateTime Today
        {
            get
            {
                return DateTime.Now.Date.ToLocalTime();
            }
        }
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
        public ExpenseDetailViewModel(Expense expense)
        {
            Expense = expense;
            Title = "Expense Details";
            catViewModel = new CategoriesViewModel();
            Categories = catViewModel.GetAllCategories().ToList();
            PopulateCategoryNames();
        }
        private void PopulateCategoryNames()
        {
            CategoryNames = new List<string>();
            foreach (var item in Categories)
            {
                CategoryNames.Add(item.Name);
            }
        }
    }
}
