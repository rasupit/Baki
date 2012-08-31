using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baki.Install;
using Baki.Registration;
using Baki.Service;

namespace Baki
{
    public class WindowsService
    {
        private WindowsService() { }

        public static void Run<TServiceHost>(string[] args) where TServiceHost : class
        {
            Run<TServiceHost>(args, null, null);
        }

        public static void Run<TServiceHost>(string[] args, Action<InstallerConfigurator> installerOption, Action<RunHostConfigurator<TServiceHost>> runHostOption) where TServiceHost : class
        {
            var wsr = new WindowsServiceRegistration<TServiceHost>();

            wsr.InstallOption(installerOption)
                .RunOption(runHostOption)
                .Run(args);
        }
    }

}
