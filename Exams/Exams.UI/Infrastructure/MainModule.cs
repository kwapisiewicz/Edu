using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Exams.UI.Views;
using Exams.UI.Windows;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exams.UI.Infrastructure
{
    public class MainModule : IModule
    {
        IRegionManager _regionManager;
        IWindsorContainer _container;

        public MainModule(IRegionManager regionManager, IWindsorContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            _container.Register(Component.For<LoginView>().Named(typeof(LoginView).FullName));
            _container.Register(Component.For<MainView>().Named(typeof(MainView).FullName));
            _container.Register(Component.For<NavigationView>().Named(typeof(NavigationView).FullName));
            _container.Register(Component.For<CategoriesView>().Named(typeof(CategoriesView).FullName));
            _container.Register(Component.For<QuestionsView>().Named(typeof(QuestionsView).FullName));
            _container.Register(Component.For<PasswordView>().Named(typeof(PasswordView).FullName));
            _container.Register(Component.For<StartPageView>().Named(typeof(StartPageView).FullName));

            //Starting regions registration
            _regionManager.RegisterViewWithRegion(Regions.MainWindow, typeof(LoginView));
            _regionManager.RegisterViewWithRegion(Regions.MainNavigation, typeof(NavigationView));
            _regionManager.RegisterViewWithRegion(Regions.MainContent, typeof(StartPageView));
            //_regionManager.RegisterViewWithRegion(Regions.MainContent, typeof(StartPageView));

            //View models
            _container.Register(Component.For<QuestionsViewModel>());
            _container.Register(Component.For<CategoriesViewModel>());            
        }
    }
}
