using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MoneyTrack.Models;
using MoneyTrack.Views.Categories;
using MoneyTrack.Views.Incomes;
using MoneyTrack.Views.Expenses;
using MoneyTrack.Views.History;

namespace MoneyTrack.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;
            Detail = new NavigationPage(new HistoryPage());

            //MenuPages.Add((int)MenuItemType.Expenses, new NavigationPage(new ExpenseHistoryPage()));
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {                   
                    case (int)MenuItemType.Categories:
                        MenuPages.Add(id, new NavigationPage(new CategoriesPage()));
                        break;
                    case (int)MenuItemType.Incomes:
                        MenuPages.Add(id, new NavigationPage(new IncomeHistoryPage()));
                        break;
                    case (int)MenuItemType.Expenses:
                        MenuPages.Add(id, new NavigationPage(new ExpenseHistoryPage()));
                        break;
                    case (int)MenuItemType.History:
                        MenuPages.Add(id, new NavigationPage(new HistoryPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}