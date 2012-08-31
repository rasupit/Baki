using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Baki.Install;
using Baki.Service;

namespace Baki.Registration
{
    class WindowsServiceRegistration<TServiceHost> where TServiceHost : class
    {
        private readonly InstallConfig _installConfig;
        private readonly ServiceHostConfig<TServiceHost> _hostConfig = new ServiceHostConfig<TServiceHost>();

        public InstallConfig InstallConfig
        {
            get { return _installConfig; }
        }

        public ServiceHostConfig<TServiceHost> HostConfig
        {
            get { return _hostConfig; }
        }

        public WindowsServiceRegistration()
            : this(null)
        {}

        public WindowsServiceRegistration(string serviceName)
        {
            serviceName = serviceName ?? Assembly.GetEntryAssembly().GetName().Name;
            _installConfig = new InstallConfig(serviceName);
        }

        public WindowsServiceRegistration<TServiceHost> InstallOption(Action<InstallerConfigurator> installerOptionAction)
        {
            if (installerOptionAction == null) return this;

            var cfgtor = new InstallerConfigurator(InstallConfig);
            installerOptionAction(cfgtor);

            return this;
        }

        public WindowsServiceRegistration<TServiceHost> RunOption(Action<RunHostConfigurator<TServiceHost>> runHostOptionAction)
        {
            if (runHostOptionAction == null) return this;

            var cfgtor = new RunHostConfigurator<TServiceHost>(HostConfig);
            runHostOptionAction(cfgtor);

            return this;
        }

        public void Run(string[] args)
        {
            if (Environment.UserInteractive && args != null && args.Length> 0)
            {
                new InteractiveCommandService(InstallConfig).Run(args);
            }
            else
            {
                new WindowsServiceRunner<TServiceHost>(InstallConfig, HostConfig).Run();
            }
        }
    }
}
