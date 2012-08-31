using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Baki.Install;

namespace Baki.Registration
{
    public class InstallerConfigurator
    {
        readonly InstallConfig _config;
        public InstallerConfigurator(InstallConfig config)
        {
            _config = config;
        }

        public void ServiceName(string value)
        {
            _config.ServiceName = value;
        }

        public void Description(string value)
        {
            _config.Description = value;
        }

        public void DisplayName(string value)
        {
            _config.DisplayName = value;
        }

        public void StartType(ServiceStartMode value)
        {
            _config.StartType = value;
        }

        public void Account(ServiceAccount value)
        {
            _config.Account = value;
        }

        public void Username(string value)
        {
            _config.Username = value;
        }

        public void Password(string value)
        {
            _config.Password = value;
        }
    }
}
