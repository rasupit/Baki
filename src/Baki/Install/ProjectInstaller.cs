using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Baki.Registration;

namespace Baki.Install
{
    class ProjectInstaller : Installer
    {
        public ProjectInstaller(InstallConfig config)
        {
            if (config == null) 
                throw new ArgumentNullException("config");

            SetupInstaller(config);
        }

        private void SetupInstaller(InstallConfig config)
        {
            var serviceInstaller = new ServiceInstaller
            {
                ServiceName = config.ServiceName,
                Description = config.Description,
                DisplayName = config.DisplayName,
                StartType = Enum.IsDefined(typeof(ServiceStartMode), config.StartType) ? 
                            config.StartType : 
                            ServiceStartMode.Automatic
            };

            var serviceProcessInstaller = new ServiceProcessInstaller
            {
                Account = config.Account
            };

            if (config.Account == ServiceAccount.User)
            {
                serviceProcessInstaller.Username = config.Username;
                serviceProcessInstaller.Password = config.Password;
            }

            Installers.Add(serviceProcessInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
