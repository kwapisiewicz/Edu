using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Exams.UI.Core.PrismExtensions;
using Exams.UI.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exams.UI.Infrastructure
{
    public class UIInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<Shell>());
            container.Register(Component.For<MainModule>());
            container.Register(Component.For<TeacherModule>());
            container.Register(Component.For<StackPanelRegionAdapter>());
        }
    }
}
