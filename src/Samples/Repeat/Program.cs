using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Kakatua;

namespace Repeat
{
    class Program
    {

        static void Main(string[] args)
        {
            //Repeat_Using_SyncService();
            Repeat_Using_AsyncService();
        }

        static void Repeat_Using_SyncService()
        {
            var cf = new ChannelFactory<Kakatua.IKakatuaService>("KakatuaServiceEndPoint");
            var svc = cf.CreateChannel();

            while (true)
            {
                Console.Write("You Say: ");
                var phrase = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(phrase))
                    break;

                var resp = svc.Say(phrase);
                Console.WriteLine("Kakatua: {0}", resp);
            }            
        }

        static void Repeat_Using_AsyncService()
        {
            var cf = new ChannelFactory<Kakatua.IKakatuaServiceAsync>("AsyncKakatuaServiceEndPoint");
            var svc = cf.CreateChannel();

            while (true)
            {
                Console.Write("You Say: ");
                var phrase = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(phrase))
                    break;

                var waiter = new ManualResetEvent(false);
                var resp = svc.BeginSay(phrase, (r) =>
                                                    {
                                                        Console.WriteLine("Kakatua: {0}", svc.EndSay(r));

                                                        waiter.Set();
                                                    }, null);

                waiter.WaitOne();
            }            

        }


    }

}
