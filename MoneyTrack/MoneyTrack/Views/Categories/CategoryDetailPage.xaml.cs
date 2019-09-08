using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyTrack.Models;
using MoneyTrack.ViewModels.Categories;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyTrack.Views.Categories
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryDetailPage : ContentPage
    {
        public Category Category { get; set; }
        private CategoryDetailViewModel categoryDetailViewModel;

        public CategoryDetailPage()
        {
            InitializeComponent();
        }

        public CategoryDetailPage(CategoryDetailViewModel categoryDetailViewModel)
        {
            InitializeComponent();
            this.categoryDetailViewModel = categoryDetailViewModel;
            BindingContext = this.categoryDetailViewModel;
        }

        async void Update_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "UpdateCategory", this.categoryDetailViewModel.Category);
            await Navigation.PopAsync();
        }
    }
}