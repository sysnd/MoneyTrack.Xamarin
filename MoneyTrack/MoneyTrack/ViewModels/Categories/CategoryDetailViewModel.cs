using MoneyTrack.Models;
using MoneyTrack.Views.Categories;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MoneyTrack.ViewModels.Categories
{
    public class CategoryDetailViewModel :BaseViewModel<Category>
    {
        public Category Category { get; set; }
        public CategoryDetailViewModel(Category category)
        {
            Category = category;
            Title = "Category Details";
        }
    }
}
