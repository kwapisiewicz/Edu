using Castle.MicroKernel.Registration;
using Exams.ODataClient.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exams.ODataClient
{
    public class ODataClientInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<Client>()
                .LifestyleSingleton());
        }
    }
}
