using System;
using System.Collections.ObjectModel;
using MoneyTrack.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using MoneyTrack.Views.Incomes;
using System.Collections.Generic;

namespace MoneyTrack.ViewModels.Incomes
{
    public class IncomeViewModel : BaseViewModel<Income>
    {
        public ObservableCollection<Income> Incomes { get; set; }
        public Command LoadIncomesCommand { get; set; }
        public Command DeleteIncomeCommand { get; set; }
        private static IncomeViewModel Instance { get; set; }
        public static IncomeViewModel GetInstance()
        {
            if (Instance == null)
            {
                Instance = new IncomeViewModel();
            }
            return Instance;
        }
        private IncomeViewModel()
        {
            Title = "Income History";
            Incomes = new ObservableCollection<Income>();
            LoadIncomesCommand = new Command(async () => await ExecuteLoadIncomesCommand());
            DeleteIncomeCommand = new Command(async (name) => await ExecuteDeleteIncomeCommand(name));


            MessagingCenter.Subscribe<NewIncomePage, Income>(this, "AddIncome", async (obj, item) =>
            {
                Incomes.Add(item);
                await DataStore.AddAsync(item);
            });
            MessagingCenter.Subscribe<IncomeDetailPage, Income>(this, "UpdateIncome", async (obj, item) =>
            {
                item.DisplayName = $"{item.Name}-{item.Date.ToString("dd/MM/yyyy")}";
                await DataStore.UpdateAsync(item);
            });
        }
        public async Task<IEnumerable<Income>> GetAllIncomesAsync()
        {
            await ExecuteLoadIncomesCommand();
            return Incomes;
        }
        async Task ExecuteDeleteIncomeCommand(object item)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            Income itemAsIncome = (Income)item;
            try
            {
                var income = Incomes.FirstOrDefault(x => x.Id == itemAsIncome.Id);
                await DataStore.DeleteAsync(income);
                Incomes.Remove(income);
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

        async Task ExecuteLoadIncomesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Incomes.Clear();
                var incomes = await DataStore.GetAsync(true);
                incomes = incomes.OrderByDescending(x => x.Date);
                foreach (var item in incomes)
                {
                    item.BackgroundColor = Color.FromRgb(104, 222, 45);
                    Incomes.Add(item);
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
