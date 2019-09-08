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
    public partial class IncomeDetailPage : ContentPage
    {
        private IncomeDetailViewModel incomeDetailViewModel;
        public Income Income { get; set; }
        
        public IncomeDetailPage()
        {
            InitializeComponent();
        }

        public IncomeDetailPage(IncomeDetailViewModel incomeDetailViewModel)
        {
            InitializeComponent();
            this.incomeDetailViewModel = incomeDetailViewModel;
            BindingContext = this.incomeDetailViewModel;
        }

        async void Update_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "UpdateIncome", this.incomeDetailViewModel.Income);
            await Navigation.PopAsync();
        }
        void OnDateSelected(object sender, DateChangedEventArgs args)
        {
            var datePicker = (DatePicker)sender;
            incomeDetailViewModel.Income.Date = datePicker.Date;
        }
    }
}