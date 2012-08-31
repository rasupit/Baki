using System.ServiceProcess;

namespace Baki.Install
{
    public class InstallConfig
    {
        //ServiceInstaller 
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public ServiceStartMode StartType { get; set; }

        //ServiceProcessInstaller
        public ServiceAccount Account { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public InstallConfig(string serviceName)
        {
            ServiceName = serviceName;
            Account = ServiceAccount.LocalSystem;
        }
    }
}
