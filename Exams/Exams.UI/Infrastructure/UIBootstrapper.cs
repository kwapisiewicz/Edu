using Microsoft.Practices.Prism.Modularity;
using PrismContrib.WindsorExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            base.Container.Install(new MainWindsorInstaller());
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
    }
}
