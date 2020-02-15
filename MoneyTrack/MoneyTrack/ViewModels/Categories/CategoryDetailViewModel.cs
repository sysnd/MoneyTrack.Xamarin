using MoneyTrack.Models;

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
