using MoneyTrack.Models;
using MoneyTrack.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyTrack.Views.Categories
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesPage : ContentPage
    {
        public CategoriesViewModel viewModel;
        public CategoriesPage()
        {
            InitializeComponent();
            viewModel = CategoriesViewModel.GetInstance();
            BindingContext = viewModel;
        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var category = args.SelectedItem as Category;
            if (category == null)
                return;

            await Navigation.PushAsync(new CategoryDetailPage(new CategoryDetailViewModel(category)));

            // Manually deselect item.
            CategoriesList.SelectedItem = null;
        }

        async void AddCategory_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewCategoryPage()));
        }
        async void Delete_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var answer = await DisplayAlert("Delete", "Are you sure you want to delete this item?", "Yes", "No");
            if (answer)
            {
                viewModel.DeleteCategoriesCommand.Execute(button.CommandParameter.ToString());
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadCategoriesCommand.Execute(null);
            CategoriesList.ItemsSource = viewModel.Categories;
        }
    }
}