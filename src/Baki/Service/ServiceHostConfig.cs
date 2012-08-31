using System;

namespace Baki.Service
{
    public class ServiceHostConfig<TServiceHost> where TServiceHost : class 
    {
        public Func<TServiceHost> CreateAction { get; set; }
        public Action<TServiceHost> StartAction { get; set; }
        public Action<TServiceHost> StopAction { get; set; }
    }
}
