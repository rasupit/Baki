using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Baki.Install;
using Baki.Registration;
using NUnit.Framework;

namespace Baki.Test
{
    [TestFixture]
    public class Integration
    {
        [TestFixtureSetUp]
        public void Init()
        {
            if(!UacHelper.IsRunAsAdministrator())
                Assert.Ignore("Integration test is ignored unless VS is run under admin previlage.");
        }

        [TestCase]
        public void Install_Should_Install()
        {
            var cfg = new InstallConfig("MyService")
                          {
                              DisplayName = "My DisplayName",
                              Description = "My Description",
                              Account = ServiceAccount.LocalSystem
                          };
            var path = @"C:\Users\Ricky\My Projects\Baki\src\Samples\Kakatua\bin\Debug\Kakatua.exe";
            var svc = new WindowsServiceSelfService(cfg, path);
            svc.Install();

            svc.Uninstall();
        }

        [TestCase]
        public void Install_Should_Install_When_Provide_ServiceName_Only()
        {
            var cfg = new InstallConfig("MyService");
            var path = @"C:\Users\Ricky\My Projects\Baki\src\Samples\Kakatua\bin\Debug\Kakatua.exe";
            var svc = new WindowsServiceSelfService(cfg, path);
            svc.Install();
        }
    }
}
