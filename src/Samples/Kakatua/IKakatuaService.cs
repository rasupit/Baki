using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Kakatua
{
    [ServiceContract]
    public interface IKakatuaService
    {
        [OperationContract]
        string Say(string phrase);
    }
}
