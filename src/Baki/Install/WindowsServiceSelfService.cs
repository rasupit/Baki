using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.ServiceProcess;
using System.Text;
using Baki.Registration;

namespace Baki.Install
{
    public class WindowsServiceSelfService
    {
        private readonly InstallConfig _config;
        private ServiceController _controller;
        private readonly string _assemblyPath;

        public string ServiceName
        {
            get { return _config.DisplayName ?? _config.ServiceName; }
        }
        public string ServiceDescription
        {
            get { return _config.Description; }
        }
        public bool IsInstalled
        {
            get { return (_controller != null); }
        }

        public bool IsStarted
        {
            get
            {
                if (_controller == null)
                    return false;
                return (_controller.Status == ServiceControllerStatus.Running);
            }
        }

        public WindowsServiceSelfService(InstallConfig config) : this (config, null)
        { }

        public WindowsServiceSelfService(InstallConfig config, string assemblyPath)
        {
            _config = config;
            _controller = GetServiceController(config.ServiceName);
            _assemblyPath = assemblyPath ?? GetAssemblyPath();
        }

        public void Install()
        {
            if (IsInstalled)
            {
                Console.WriteLine("Service '{0}' is already installed.", _config.ServiceName);
                return;
            }
            UacHelper.RunWithAdminPrevilage(() =>
            {
                var dummy = new System.Collections.Hashtable();

                using (var ti = new TransactedInstaller())
                {
                    ti.Context = new InstallContext(null, new[] { "/assemblypath=" + _assemblyPath });
                    ti.Installers.Add(new ProjectInstaller(_config));
                    ti.Install(dummy);
                }

                _controller = new ServiceController(_config.ServiceName);

                Console.WriteLine("Service '{0}' installed successfully.", _config.ServiceName);                                                    
            });
        }

        public void Uninstall()
        {
            if (!IsInstalled)
            {
                Console.WriteLine("Service '{0}' is already unistalled.", _config.ServiceName);
                return;
            }
            UacHelper.RunWithAdminPrevilage(() =>
            {
                if (IsStarted)
                    _controller.Stop();
                _controller = null;

                using (var ti = new TransactedInstaller())
                {
                    ti.Context = new InstallContext(null, new[] { "/assemblypath=" + _assemblyPath });
                    ti.Installers.Add(new ProjectInstaller(_config));
                    ti.Uninstall(null);
                }
                Console.WriteLine("Service '{0}' uninstalled successfully.", _config.ServiceName);
            });
        }

        public void Start()
        {
            if (!IsInstalled)
            {
                Console.WriteLine("Service '{0}' is not installed.", _config.ServiceName);
                return;
            }

            if (IsStarted)
            {
                Console.WriteLine("Service '{0}' is already running.", _config.ServiceName);
                return;
            }

            UacHelper.RunWithAdminPrevilage(() =>
            {
                _controller.Start();
                _controller.WaitForStatus(ServiceControllerStatus.Running);

                Console.WriteLine("Service '{0}' is started.", _config.ServiceName);                
            });
        }

        public void Stop()
        {
            if (!IsInstalled)
            {
                Console.WriteLine("Service '{0}' is not installed.", _config.ServiceName);
                return;
            }

            if (!IsStarted)
            {
                Console.WriteLine("Service '{0}' is already stopped", _config.ServiceName);
                return;
            }

            UacHelper.RunWithAdminPrevilage(() =>
            {
                _controller.Stop();
                _controller.WaitForStatus(ServiceControllerStatus.Stopped);

                Console.WriteLine("Service '{0}' is stopped.", _config.ServiceName);
            });
        }

        public void Restart()
        {
            Stop();
            Start();
        }

        private static string GetAssemblyPath()
        {
            var assembly = Assembly.GetEntryAssembly();
            if (assembly == null)
                throw new InvalidOperationException("Self Installer can not find the entry program. Install/Uninstall must be called from a managed application.");

            return assembly.Location;
        }

        private static ServiceController GetServiceController(string serviceName)
        {
            return ServiceController.GetServices()
                .Where(s => string.Equals(s.ServiceName, serviceName))
                .FirstOrDefault();
        }
    }
}
