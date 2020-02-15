using MoneyTrack.Models;
using MoneyTrack.Views.Expenses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace MoneyTrack.ViewModels.Expenses
{
    public class ExpenseViewModel: BaseViewModel<Expense>
    {
        public ObservableCollection<Expense> Expenses { get; set; }
        public Command LoadExpensesCommand { get; set; }
        public Command DeleteExpenseCommand { get; set; }
        private static ExpenseViewModel Instance { get; set; }
        public static ExpenseViewModel GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ExpenseViewModel();
            }
            return Instance;
        }
        private ExpenseViewModel()
        {
            Title = "Expenses History";
            Expenses = new ObservableCollection<Expense>();
            LoadExpensesCommand = new Command(ExecuteLoadExpensesCommand);
            DeleteExpenseCommand = new Command((name) => ExecuteDeleteExpenseCommand(name));

            MessagingCenter.Subscribe<NewExpensePage, Expense>(this, "AddExpense", (obj, item) =>
            {
                DataStore.Add(item);
                Expenses.Add(item);
            });
            MessagingCenter.Subscribe<ExpenseDetailPage, Expense>(this, "UpdateExpense", (obj, item) =>
            {
                item.DisplayName = $"{item.Name}-{item.Date.ToString("dd/MM/yyyy")}";
                DataStore.Update(item);
            });
        }
        public IEnumerable<Expense> GetAllExpenses()
        {
            Instance.ExecuteLoadExpensesCommand();
            return Expenses;
        }
        void ExecuteDeleteExpenseCommand(object item)
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

        void ExecuteLoadExpensesCommand()
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
                    item.BackgroundColor = Color.FromRgb(218, 56, 53);
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
