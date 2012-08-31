using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace Baki.Service
{
    public class ServiceShell : ServiceBase
    {
        private IContainer _components;
        private IWindowsServiceHost _host;

        public ServiceShell(string serviceName, IWindowsServiceHost host)
        {
            ServiceName = serviceName;
            _host = host;
            _components = new Container();
        }

        protected override void OnStart(string[] args)
        {
            _host.Start();
        }

        protected override void OnStop()
        {
            _host.Stop();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (_components != null))
            {
                _components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
