using Exams.UI.Infrastructure;
using Microsoft.Practices.Prism.Commands;
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

namespace Exams.UI.Views
{
    /// <summary>
    /// Interaction logic for NavigationView.xaml
    /// </summary>
    public partial class NavigationView : UserControl
    {
        public NavigationView(IRegionManager regionManager)
        {
            DataContext = this;

            NavigateStartPage = new DelegateCommand(() =>
regionManager.RequestNavigate(Regions.MainContent, typeof(StartPageView).FullName));
            NavigateCategories = new DelegateCommand(() =>
regionManager.RequestNavigate(Regions.MainContent, typeof(CategoriesView).FullName));
            NavigateQuestions = new DelegateCommand(() =>
regionManager.RequestNavigate(Regions.MainContent, typeof(QuestionsView).FullName));
            
            InitializeComponent();
        }

        public ICommand NavigateStartPage { get; set; }        
        public ICommand NavigateCategories { get; set; }
        public ICommand NavigateQuestions { get; set; }
    }
}
