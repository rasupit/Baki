using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;

namespace Baki
{
    static class UacHelper
    {
        public static bool IsRunAsAdministrator()
        {
            try
            {
                var principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());

                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (ArgumentNullException) { /* nop: so it return false */ }

            return false;
        }

        public static bool RunAsAdministrator(string fileName, string arg, string workingDirectory)
        {
            Console.WriteLine("Rerun under elevated previlege:");
            Console.WriteLine("  {0}{1}", fileName, arg);
            var pi = new ProcessStartInfo(fileName, arg)
            {
                Verb = "runas",
                UseShellExecute = true,
                WorkingDirectory = workingDirectory
            };

            try
            {
                var p = Process.Start(pi);
                p.WaitForExit();
                Console.WriteLine("Successfully ran under elevated privilage.");
                return p.ExitCode == 0;
            }
            catch
            {
                Console.WriteLine("Failed to run under elevated privilage.");
                return false;
            }

        }

        public static bool RerunAsAdministrator()
        {
            var asm = Assembly.GetEntryAssembly();
            if(asm == null)
                throw new InvalidOperationException("Unable to locate the entry program to re-run under admimistative previlage. The entry program must be called from a managed application.");
            
            var fileName = asm.Location;
            var arg = Environment.CommandLine.Replace(Environment.GetCommandLineArgs().First(), "");

            return RunAsAdministrator(fileName, arg, Environment.CurrentDirectory);            
        }

        public static void RunWithAdminPrevilage(Action actionRequireAdminPrevilage)
        {
            if (Environment.OSVersion.Version.Major >= 6 && !IsRunAsAdministrator())
            {
                RerunAsAdministrator();
                return;
            }
            actionRequireAdminPrevilage();
        }
    }
}
