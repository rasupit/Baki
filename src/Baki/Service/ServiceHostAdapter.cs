using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baki.Registration;

namespace Baki.Service
{
    public class ServiceHostAdapter<TServiceHost> : IWindowsServiceHost where TServiceHost : class
    {
        protected Func<TServiceHost> _createAction;
        protected Action<TServiceHost> _startAction;
        protected Action<TServiceHost> _stopAction;

        protected TServiceHost _host;

        public ServiceHostAdapter(ServiceHostConfig<TServiceHost> hostConfig)
        {
            _createAction = hostConfig.CreateAction ?? DefaultActionCreate;
            _startAction = hostConfig.StartAction ?? DefaultActionStart;
            _stopAction = hostConfig.StopAction ?? DefaultActionStop;
        }

        public void Start()
        {
            if (_host == null)
            {
                _host = _createAction();
            }

            _startAction(_host);
        }

        public void Stop()
        {
            if (_host == null) return;
            _stopAction(_host);
        }

        protected virtual TServiceHost DefaultActionCreate()
        {
            return (TServiceHost)Activator.CreateInstance(typeof(TServiceHost));
        }

        protected virtual void DefaultActionStart(TServiceHost host)
        {
            var svcHost = host as IWindowsServiceHost;
            if (svcHost == null)
            {
                var m = typeof (TServiceHost).GetMethod("Start");
                if (m == null)
                {
                    var msg = string.Format("Host class '{0}' must have 'Start' method.  Other option is to have this class implements Baki.IWindowsServiceHost interface.", host.GetType().Name);
                    throw new MissingMethodException(msg);
                }
                else
                {
                    m.Invoke(host, new object[0]);
                }
            }
            else
            {
                svcHost.Start();
            }
        }

        protected virtual void DefaultActionStop(TServiceHost host)
        {
            var svcHost = host as IWindowsServiceHost;
            if (svcHost == null)
            {
                var m = typeof (TServiceHost).GetMethod("Stop");
                if (m != null)
                {
                    m.Invoke(host, new object[0]);
                }
            }
            else
            {
                svcHost.Stop();
            }
        }
    }
}
