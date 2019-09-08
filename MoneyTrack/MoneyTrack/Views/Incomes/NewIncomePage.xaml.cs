using MoneyTrack.Helpers;
using MoneyTrack.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyTrack.Views.Incomes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewIncomePage : ContentPage
    {
        public Income Income { get; set; }
        public DateTime Today { get; set; }
        public string ValueString
        {
            get
            {
                return Income.Value.ToString();
            }
            set
            {
                decimal.TryParse(value, out decimal _value);
                Income.Value = _value;
            }
        }
        public NewIncomePage()
        {
            InitializeComponent();
            Income = new Income()
            {
                Name = "Salary",
                Date = DateTime.Now,
                Value = 0,
            };
            Today = DateTime.Now.Date.ToLocalTime();
            BindingContext = this;
        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            Income.DisplayName = $"{Income.Name}-{Income.Date.ToString("dd/MM/yyyy")}";
            MessagingCenter.Send(this, "AddIncome", Income);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        void OnDateSelected(object sender, DateChangedEventArgs args)
        {
            var datePicker = (DatePicker)sender;
            Income.Date = datePicker.Date;
        }
    }
}