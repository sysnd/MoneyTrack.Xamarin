using MoneyTrack.Helpers;
using MoneyTrack.Models;
using MoneyTrack.ViewModels.Expenses;
using MoneyTrack.ViewModels.Incomes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyTrack.ViewModels
{
    public class HistoryViewModel :BaseViewModel<IHistoryItem>
    {
        public List<Income> Incomes { get; set; }
        public List<Expense> Expenses { get; set; }
        public List<IHistoryItem> HistoryItems { get; set; }
        public Command LoadHistoryItemsCommand { get; set; }
        public IncomeViewModel incomeViewModel { get; set; }
        public ExpenseViewModel expenseViewModel { get; set; }
        public Color SubTextColor { get; set; }
        public Color TextColor { get; set; }
        public string Balance { get; set; }
        public List<Grouping<string, IHistoryItem>> ItemsGrouped { get; set; }
        public HistoryViewModel()
        {
            LoadHistoryItemsCommand = new Command(async () => await ExecuteLoadHistoryItemsCommand());
            incomeViewModel = IncomeViewModel.GetInstance();
            expenseViewModel = ExpenseViewModel.GetInstance();
            HistoryItems = new List<IHistoryItem>();
            Expenses = expenseViewModel.GetAllExpenses().ToList();
            Incomes = incomeViewModel.GetAllIncomes().ToList();
            HistoryItems.AddRange(Expenses);
            HistoryItems.AddRange(Incomes);
            HistoryItems = HistoryItems.OrderByDescending(x => x.Date).ToList();
            Balance = CalculateBalance();
            ItemsGrouped = GroupItems();
            SubTextColor = Color.FromRgb(100, 100, 100);
            TextColor = Color.FromRgb(95, 108, 128);
        }

        private List<Grouping<string, IHistoryItem>> GroupItems()
        {
            var grouped = HistoryItems.GroupBy(x => x.Date.ToString("dd/MM/yyyy")).Select(x => new Grouping<string, IHistoryItem>(x.Key, x));
            return new List<Grouping<string, IHistoryItem>>(grouped);
        }

        private string CalculateBalance()
        {
            var income = 0.00M; 
            var expenses = 0.00M;
            Incomes.ForEach(x => income += x.Value);
            Expenses.ForEach(x => expenses += x.Value);
            return (income - expenses).ToString();
        }

        async Task ExecuteLoadHistoryItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Expenses = expenseViewModel.GetAllExpenses().ToList();
                Incomes = incomeViewModel.GetAllIncomes().ToList();
                HistoryItems.Clear();
                HistoryItems.AddRange(Expenses);
                HistoryItems.AddRange(Incomes);
                HistoryItems = HistoryItems.OrderByDescending(x => x.Date).ToList();
                ItemsGrouped = GroupItems();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
