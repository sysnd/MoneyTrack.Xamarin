using MoneyTrack.Models;
using System;

namespace MoneyTrack.ViewModels.Incomes
{
    public class IncomeDetailViewModel :BaseViewModel<Income>
    {
        public Income Income { get; set; }
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
                return Income.Value.ToString();
            }
            set
            {
                decimal.TryParse(value, out decimal _value);
                Income.Value = _value;
            }
        }
        public IncomeDetailViewModel(Income income)
        {
            Income = income;
            Title = "Income Details";
        }
    }
}
