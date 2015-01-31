using Exams.Model;
using Exams.UI.Context;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Exams.UI.Views
{
    public class CategoriesViewModel : BindableBase
    {
        ODataClient _client;

        public ObservableCollection<Category> Categories { get; set; }

        private Category _SelectedCategory;
        public Category SelectedCategory
        {
            get
            {
                return _SelectedCategory;
            }
            set
            {
                _SelectedCategory = value;
                OnPropertyChanged(() => SelectedCategory);
                DeleteCategoryCommand.RaiseCanExecuteChanged();
            }
        }

        public CategoriesViewModel(ODataClient client)
        {
            _client = client;

            Categories = new ObservableCollection<Category>(
                _client.Context.Categories.ToList());

            AddCategoryCommand = new DelegateCommand(AddCategory);
            DeleteCategoryCommand = new DelegateCommand(DeleteCategory, CanDeleteCategory);
        }

        private void AddCategory()
        {
            if (System.Windows.MessageBox.Show(
                "Na pewno?",
                "Na pewno?",
                System.Windows.MessageBoxButton.OKCancel) == System.Windows.MessageBoxResult.OK)
            {
                Category newCategory = new Category();
                newCategory.Name = "Nowa";
                _client.Context.AddToCategories(newCategory);
                _client.Context.SaveChanges();
                Categories.Add(newCategory);
            }
        }

        private bool CanDeleteCategory()
        {
            return SelectedCategory != null;
        }

        private void DeleteCategory()
        {
            if (System.Windows.MessageBox.Show(
                "Na pewno?",
                "Na pewno?",
                System.Windows.MessageBoxButton.OKCancel) == System.Windows.MessageBoxResult.OK)
            {
                _client.Context.DeleteObject(SelectedCategory);
                _client.Context.SaveChanges();
                Categories.Remove(SelectedCategory);
            }
        }

        public DelegateCommand AddCategoryCommand { get; set; }
        public DelegateCommand DeleteCategoryCommand { get; set; }
    }
}
