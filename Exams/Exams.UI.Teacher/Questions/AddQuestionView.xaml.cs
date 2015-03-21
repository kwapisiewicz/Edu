using Exams.UI.Core;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Exams.UI.Teacher.Questions
{
    /// <summary>
    /// Interaction logic for AddQuestionView.xaml
    /// </summary>
    public partial class AddQuestionView : UserControl, INavigationAware
    {
        IRegionManager _regionManager;

        public AddQuestionView(AddQuestionViewModel viewModel, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            ViewModel = viewModel;
            DataContext = this.ViewModel;
            InitializeComponent();
        }

        public AddQuestionViewModel ViewModel { get; set; }

        private void InitializeButtons()
        {
            IRegion region = _regionManager.Regions[Regions.MainToolbar];
            region.Add(new Button()
            {
                Content = "Zapisz",
                Command = ViewModel.SaveCommand
            });
        }

        private void CleanButtons()
        {
            IRegion region = _regionManager.Regions[Regions.MainToolbar];
            foreach (var item in region.Views)
            {
                region.Remove(item);
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            CleanButtons();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            InitializeButtons();
        }
    }
}
