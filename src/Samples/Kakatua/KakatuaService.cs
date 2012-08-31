using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kakatua
{
    public class KakatuaService : IKakatuaService
    {
        public string Say(string phrase)
        {
            Console.WriteLine("{0} - {1}", DateTime.Now, phrase);
            return phrase;
        }
    }
}
