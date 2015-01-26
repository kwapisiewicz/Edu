using Exams.UI.PrismExtensions;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using PrismContrib.WindsorExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Exams.UI.Infrastructure
{
    public class UIBootstrapper : WindsorBootstrapper
    {
        protected override System.Windows.DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            RegisterModule(typeof(MainModule));
        }

        protected override void ConfigureContainer()
        {
            base.Container.Install(
                new InfrastructureInstaller(),
                new MainWindsorInstaller());
            base.ConfigureContainer();
        }

        private void RegisterModule(Type mainModuleType)
        {
            ModuleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = mainModuleType.Name,
                ModuleType = mainModuleType.AssemblyQualifiedName,
                InitializationMode = InitializationMode.WhenAvailable
            });
        }

        protected override Microsoft.Practices.Prism.Regions.RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
            return mappings;
        }
    }
}
