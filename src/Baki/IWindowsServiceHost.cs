using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baki
{
    public interface IWindowsServiceHost
    {
        void Start();
        void Stop();
    }
}
