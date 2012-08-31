using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baki.Registration;
using Baki.Service;
using NUnit.Framework;

namespace Baki.Test
{
    [TestFixture]
    public class Test_ServiceHostAdapter
    {
        [TestCase]
        public void Start_Should_Call_CreatAction_Before_Start()
        {
            var cfg = new ServiceHostConfig<TestServiceHost>
            {
                CreateAction = () => new TestServiceHost()
            };

            var svc = new TestServiceHostAdapter<TestServiceHost>(cfg);
            svc.Start();

            Assert.True(svc.Host.IsCreated);
        }

        [TestCase]
        public void Start_Should_Call_StartAction()
        {
            var cfg = new ServiceHostConfig<TestServiceHost>
                          {
                              StartAction = h => h.Start()
                          };

            var svc = new TestServiceHostAdapter<TestServiceHost>(cfg);
            svc.Start();

            Assert.True(svc.Host.IsStarted);
        }

        [TestCase]
        public void Start_Should_Call_IServiceHostStart()
        {
            var cfg = new ServiceHostConfig<ServiceHostImplementIServiceHost>();

            var svc = new TestServiceHostAdapter<ServiceHostImplementIServiceHost>(cfg);

            svc.Start();

            Assert.True(svc.Host.IsStarted);
        }

        [TestCase]
        public void Start_Should_Call_Start_Method_ByConvention()
        {
            var host = new TestServiceHost();
            var cfg = new ServiceHostConfig<TestServiceHost>();

            var svc = new TestServiceHostAdapter<TestServiceHost>(cfg);
            svc.Start();

            Assert.True(svc.Host.IsStarted);            
        }

        [TestCase]
        public void Stop_Should_Call_StopAction()
        {
            var cfg = new ServiceHostConfig<TestServiceHost>
            {
                StopAction = h => h.Stop()
            };

            var svc = new TestServiceHostAdapter<TestServiceHost>(cfg);
            svc.Start();
            svc.Stop();

            Assert.True(svc.Host.IsStoped);
        }
        [TestCase]
        public void Stop_Should_Call_IServiceHostStop()
        {
            var cfg = new ServiceHostConfig<ServiceHostImplementIServiceHost>();

            var svc = new TestServiceHostAdapter<ServiceHostImplementIServiceHost>(cfg);
            svc.Start();
            svc.Stop();

            Assert.True(svc.Host.IsStoped);
        }

        [TestCase]
        public void Stop_Should_Call_Stop_Method_ByConvention()
        {
            var cfg = new ServiceHostConfig<TestServiceHost>();

            var svc = new TestServiceHostAdapter<TestServiceHost>(cfg);
            svc.Start();
            svc.Stop();

            Assert.True(svc.Host.IsStoped);
        }
    }

    internal class TestServiceHostAdapter<TServiceHost> : ServiceHostAdapter<TServiceHost> where TServiceHost : class 
    {
        public TServiceHost Host { get { return _host; } }

        public TestServiceHostAdapter(ServiceHostConfig<TServiceHost> hostConfig)
            : base(hostConfig)
        { }
    }

    internal class TestServiceHost
    {
        public bool IsCreated;
        public bool IsStarted;
        public bool IsStoped;

        public TestServiceHost()
        {
            IsCreated = true;
        }
        public void Start()
        {
            IsStarted = true;
        }
        public void Stop()
        {
            IsStoped = true;
        }
    }

    internal class ServiceHostImplementIServiceHost : IWindowsServiceHost
    {
        public bool IsCreated;
        public bool IsStarted;
        public bool IsStoped;

        public ServiceHostImplementIServiceHost()
        {
            IsCreated = true;
        }
        public void Start()
        {
            IsStarted = true;
        }
        public void Stop()
        {
            IsStoped = true;
        }
    }
}
