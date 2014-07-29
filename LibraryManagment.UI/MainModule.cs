using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagment.UI
{
    public class MainModule : IModule
    {
        IRegionManager _regionManager;

        public MainModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MainContent, typeof(MainContentView));
            _regionManager.RegisterViewWithRegion(RegionNames.MainNavigation, typeof(MainNavigationView));
        }
    }
}
