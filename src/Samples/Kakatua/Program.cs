using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baki;

namespace Kakatua
{
    class Program
    {
        static void Main(string[] args)
        {

            WindowsService.Run<Host>(args, 
                o =>
                {
                    //o.ServiceName("Kakatua Service");
                    //o.DisplayName("Kakatua Service Display Name");
                    o.Description("Kakatua Service Description");
                },
                ro =>
                {
                    ro.Create(() => new Host());
                    ro.Start(h => h.Start());
                    ro.Stop(h => h.Stop());
                }
            );
        }
    }
}
