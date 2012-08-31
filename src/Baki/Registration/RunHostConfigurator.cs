using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baki.Service;

namespace Baki.Registration
{
    public class RunHostConfigurator<TServiceHost> where TServiceHost : class
    {
        readonly ServiceHostConfig<TServiceHost> _config;
        public RunHostConfigurator(ServiceHostConfig<TServiceHost> config)
        {
            _config = config;
        }
        public void Create(Func<TServiceHost> action)
        {
            _config.CreateAction = action;
        }
        public void Start(Action<TServiceHost> action)
        {
            _config.StartAction = action;
        }
        public void Stop(Action<TServiceHost> action)
        {
            _config.StopAction = action;
        }
    }
}
