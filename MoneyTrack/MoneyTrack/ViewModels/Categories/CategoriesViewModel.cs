using MoneyTrack.Models;
using MoneyTrack.Views.Categories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace MoneyTrack.ViewModels.Categories
{
    public class CategoriesViewModel : BaseViewModel<Category>
    {
        public ObservableCollection<Category> Categories { get; set; }
        public Command LoadCategoriesCommand { get; set; }
        public Command DeleteCategoriesCommand { get; set; }
        private static CategoriesViewModel Instance { get; set; }
        public static CategoriesViewModel GetInstance()
        {
            if (Instance == null)
            {
                Instance = new CategoriesViewModel();
            }
            return Instance;
        }
        private CategoriesViewModel()
        {
            Title = "Categories";
            Categories = new ObservableCollection<Category>();            
            LoadCategoriesCommand = new Command(ExecuteLoadCategoriesCommand);
            DeleteCategoriesCommand = new Command((name) => ExecuteDeleteCategoriesCommand(name));

            MessagingCenter.Subscribe<NewCategoryPage, Category>(this, "AddCategory", (obj, item) =>
            {
                Categories.Add(item);
                DataStore.Add(item);
            });
            MessagingCenter.Subscribe<CategoryDetailPage, Category>(this, "UpdateCategory", (obj, item) =>
            {
                DataStore.Update(item);
            });
        }
        public IEnumerable<Category> GetAllCategories()
        {
            ExecuteLoadCategoriesCommand();
            return Categories;
        } 
        void ExecuteLoadCategoriesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Categories.Clear();
                var categories = DataStore.Get(true);
                foreach (var item in categories)
                {
                    Categories.Add(item);
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
        void ExecuteDeleteCategoriesCommand(object name)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var cat = Categories.FirstOrDefault(x => x.Name == name.ToString());
                DataStore.Delete(cat);
                Categories.Remove(cat);
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
