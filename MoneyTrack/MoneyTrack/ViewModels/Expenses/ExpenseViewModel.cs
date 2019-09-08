using MoneyTrack.Models;
using MoneyTrack.Views.Expenses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MoneyTrack.ViewModels.Expenses
{
    public class ExpenseViewModel: BaseViewModel<Expense>
    {
        public ObservableCollection<Expense> Expenses { get; set; }
        public Command LoadExpensesCommand { get; set; }
        public Command DeleteExpenseCommand { get; set; }
        public ExpenseViewModel()
        {
            Title = "Expenses History";
            Expenses = new ObservableCollection<Expense>();
            LoadExpensesCommand = new Command(async () => await ExecuteLoadExpensesCommand());
            DeleteExpenseCommand = new Command(async (name) => await ExecuteDeleteExpenseCommand(name));

            MessagingCenter.Subscribe<NewExpensePage, Expense>(this, "AddExpense", async (obj, item) =>
            {
                DataStore.Add(item);
                Expenses.Add(item);
            });
            MessagingCenter.Subscribe<ExpenseDetailPage, Expense>(this, "UpdateExpense", async (obj, item) =>
            {
                item.DisplayName = $"{item.Name}-{item.Date.ToString("dd/MM/yyyy")}";
                DataStore.Update(item);
            });
        }
        public IEnumerable<Expense> GetAllExpenses()
        {
            ExecuteLoadExpensesCommand().Wait();
            return Expenses;
        }
        async Task ExecuteDeleteExpenseCommand(object item)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            Expense itemAsExpense = (Expense)item;
            try
            {
                var expense = Expenses.FirstOrDefault(x => x.Id == itemAsExpense.Id);
                DataStore.Delete(expense);
                Expenses.Remove(expense);
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

        async Task ExecuteLoadExpensesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Expenses.Clear();
                var expenses = DataStore.Get(true).OrderByDescending(x => x.Date);
                foreach (var item in expenses)
                {
                    item.BackgroundColor = new Color(255, 0, 0);
                    Expenses.Add(item);
                }
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
