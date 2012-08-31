using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Kakatua
{
    [ServiceContract(Name = "IKakatuaService")]
    public interface IKakatuaServiceAsync
    {
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginSay(string phrase, AsyncCallback callback, object userState);

        string EndSay(IAsyncResult asyncResult);
    }
}
