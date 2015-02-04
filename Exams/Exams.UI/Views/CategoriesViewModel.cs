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
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

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

            AddNewCategoryRequest = new InteractionRequest<IConfirmation>();
            AddCategoryCommand = new DelegateCommand(() =>
                AddNewCategoryRequest.Raise(
                    new Confirmation()
                    {
                        Title = "Nowa kategoria",
                        Content = new System.Windows.Controls.TextBox()
                        {
                            VerticalAlignment = System.Windows.VerticalAlignment.Top
                        }
                    },
                    AddCategory));
            ConfirmDeleteCategoryRequest = new InteractionRequest<IConfirmation>();
            DeleteCategoryCommand = new DelegateCommand(() =>
                ConfirmDeleteCategoryRequest.Raise(
                    new Confirmation()
                    {
                        Title = "Potwierdź",
                        Content = "Czy na pewno usunąć wybraną kategorię?"
                    },
                    DeleteCategory)
                , CanDeleteCategory);
        }

        private void AddCategory(IConfirmation confirmation)
        {
            System.Windows.Controls.TextBox tb = confirmation.Content as System.Windows.Controls.TextBox;
            if(confirmation.Confirmed && !string.IsNullOrEmpty(tb.Text))
            {
                Category newCategory = new Category();
                newCategory.Name = tb.Text;
                _client.Context.AddToCategories(newCategory);
                _client.Context.SaveChanges();
                Categories.Add(newCategory);
            }

        }

        private bool CanDeleteCategory()
        {
            return SelectedCategory != null;
        }

        private void DeleteCategory(IConfirmation confirmation)
        {
            if (confirmation.Confirmed)
            {
                _client.Context.DeleteObject(SelectedCategory);
                _client.Context.SaveChanges();
                Categories.Remove(SelectedCategory);
            }
        }

        public DelegateCommand AddCategoryCommand { get; set; }
        public DelegateCommand DeleteCategoryCommand { get; set; }


        public InteractionRequest<IConfirmation> AddNewCategoryRequest { get; set; }
        public InteractionRequest<IConfirmation> ConfirmDeleteCategoryRequest { get; set; }
    }
}
