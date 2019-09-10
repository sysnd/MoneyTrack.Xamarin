﻿using MoneyTrack.Models;
using MoneyTrack.Views.Categories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            LoadCategoriesCommand = new Command(async () => await ExecuteLoadCategoriesCommand());
            DeleteCategoriesCommand = new Command(async (name) => await ExecuteDeleteCategoriesCommand(name));

            MessagingCenter.Subscribe<NewCategoryPage, Category>(this, "AddCategory", async (obj, item) =>
            {
                Categories.Add(item);
                DataStore.Add(item);
            });
            MessagingCenter.Subscribe<CategoryDetailPage, Category>(this, "UpdateCategory", async (obj, item) =>
            {
                DataStore.Update(item);
            });
        }
        public IEnumerable<Category> GetAllCategories()
        {
            ExecuteLoadCategoriesCommand().Wait();
            return Categories;
        } 
        async Task ExecuteLoadCategoriesCommand()
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
        async Task ExecuteDeleteCategoriesCommand(object name)
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
