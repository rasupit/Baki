using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Baki;

namespace Kakatua
{
    class Host : IWindowsServiceHost
    {
        private ServiceHost _serviceHost;

        public void Start()
        {
            if (_serviceHost != null)
                _serviceHost.Close();

            _serviceHost = new ServiceHost(typeof(KakatuaService));

            _serviceHost.Open();
        }

        public void Stop()
        {
            if (_serviceHost != null && _serviceHost.State == CommunicationState.Opened)
                _serviceHost.Close();

            _serviceHost = null;
        }
    }
}
