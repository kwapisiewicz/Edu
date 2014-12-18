using Castle.MicroKernel.Registration;
using Exams.UI.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exams.UI.Infrastructure
{
    public class InfrastructureInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<IHashFunction>()
                .ImplementedBy<Md5PasswordHash>()
                .LifestyleSingleton());
            container.Register(Component.For<LoginContext>()
                .LifestyleSingleton());
            container.Register(Component.For<ODataClient>()
                .LifestyleSingleton());
        }
    }
}
