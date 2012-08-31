using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Baki.Install
{
    class InteractiveCommandService
    {
        private static readonly Dictionary<string, Action<WindowsServiceSelfService>> CommandActions =
            new Dictionary<string, Action<WindowsServiceSelfService>>(StringComparer.OrdinalIgnoreCase)
                {
                    {"install", s => { s.Install();}},
                    {"uninstall", s => { s.Uninstall();}},
                    {"start", s => { s.Start();}},
                    {"stop", s => { s.Stop();}},
                    {"restart", s => { s.Restart();}}
                };

        private readonly WindowsServiceSelfService _serviceSelfService;

        public InteractiveCommandService(InstallConfig config)
            : this(new WindowsServiceSelfService(config))
        { }

        public InteractiveCommandService(WindowsServiceSelfService serviceSelfService)
        {
            _serviceSelfService = serviceSelfService;
        }

        public void Run(string[] args)
        {
            if (args == null || args.Length == 0)
                throw new ArgumentNullException("args");

            var arg = args.First().TrimStart(new char[] { '/', '-' });

            Action<WindowsServiceSelfService> actionMethod;
            if(CommandActions.TryGetValue(arg, out actionMethod))
            {
                actionMethod(_serviceSelfService);
            }
            else
            {
                if(!string.Equals(arg, "help", StringComparison.OrdinalIgnoreCase))
                    Console.WriteLine("Unknown command line option '{0}'", arg);
                PrintUsage();
            }
        }

        private void PrintUsage()
        {
            var assembly = Assembly.GetEntryAssembly();
            string svcName = _serviceSelfService.ServiceName;
            string svcDesc = _serviceSelfService.ServiceDescription;
            string copyright = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)
                                        .Select(a => ((AssemblyCopyrightAttribute)a).Copyright)
                                        .SingleOrDefault();
            string exeName = assembly.GetName().Name;

            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("  " + svcName);
            if (!string.IsNullOrEmpty(svcDesc))
                sb.AppendLine("  " + svcDesc);
            if (!string.IsNullOrEmpty(copyright))
                sb.AppendLine("  " + copyright);
            sb.AppendFormat(
@"------------------------------------------------------------
  Command line options:
  {0}             - without argument, starts as console application
  {0} /install    - install then start the service
  {0} /uninstall  - stops then uninstall the service
  {0} /start      - start installed service
  {0} /stop       - stop installed service
  {0} /restart    - restart the previously started service
", exeName);

            Console.WriteLine(sb.ToString());
        }
    }
}
