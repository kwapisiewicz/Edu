using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Exams.Host
{
    public class ExamsWindowsServiceDefinition : ServiceBase
    {
        private IDisposable _app;
        private string _hostAddress;

        public ExamsWindowsServiceDefinition()
        {
            _hostAddress = ConfigurationManager.AppSettings["HostAddress"];
            base.ServiceName = "ExamsService.Host";
        }

        public void Start()
        {
            _app = WebApp.Start<WebApiConfig>(url: _hostAddress);
        }

        public new void Stop()
        {
            _app.Dispose();
        }

        protected override void OnStart(string[] args)
        {
            Start();
            base.OnStart(args);
        }      

        protected override void OnStop()
        {
            Stop();
            base.OnStop();
        }
    }
}
