using System;
using System.Linq;
using System.ServiceProcess;
using Baki.Install;
using Baki.Registration;
using Baki.Service;

namespace Baki
{
    public class WindowsServiceRunner<TServiceHost> where TServiceHost : class
    {
        private readonly InstallConfig _installConfig;
        private readonly ServiceHostConfig<TServiceHost> _serviceHostConfig;

        public WindowsServiceRunner(InstallConfig installConfig, ServiceHostConfig<TServiceHost> serviceHostConfig)
        {
            _installConfig = installConfig;
            _serviceHostConfig = serviceHostConfig;
        }

        public void Run()
        {
            if (Environment.UserInteractive)
            {
                //debug or console
                RunAsConsole();
            }
            else
            {
                RunAsService();
            }
        }

        void RunAsConsole()
        {
            //debug or console
            var host = new ServiceHostAdapter<TServiceHost>(_serviceHostConfig);
            host.Start();

            Console.WriteLine("Service Host '{0}' is currently running, press <enter> to stop...", _installConfig.ServiceName);
            Console.ReadLine();
            host.Stop();            
        }

        void RunAsService()
        {
            //run service
            var host = new ServiceHostAdapter<TServiceHost>(_serviceHostConfig);
            var svc = new ServiceShell(_installConfig.ServiceName, host);
            ServiceBase.Run(svc);            
        }
    }
}
