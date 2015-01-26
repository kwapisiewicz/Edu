using Exams.UI.Context;
using Exams.UI.Infrastructure;
using Microsoft.Practices.Prism;
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
    /// Interaction logic for CategoriesView.xaml
    /// </summary>
    public partial class CategoriesView : UserControl, INavigationAware
    {
        ODataClient _client;
        IRegionManager _regionManager;

        public CategoriesView(ODataClient client, IRegionManager regionManager)
        {
            _client = client;
            _regionManager = regionManager;
            InitializeComponent();
            try
            {
                var cats = _client.Context.Categories.ToArray().Select(a => a.Name);
                Categories.ItemsSource = cats;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        private void InitializeButtons()
        {           
            IRegion region = _regionManager.Regions[Regions.MainToolbar];
            region.Add(new Button() { Content = "Siema1" });
            region.Add(new Button() { Content = "Siema2" });
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
