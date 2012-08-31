using System;
using Baki;

namespace $rootnamespace$
{
    class Host : IWindowsServiceHost
    {
        public void Start()
        {
			Console.WriteLine("Service Start");
			throw new NotImplementedException();
        }

        public void Stop()
        {
			Console.WriteLine("Service Stop");
        }
    }
}
