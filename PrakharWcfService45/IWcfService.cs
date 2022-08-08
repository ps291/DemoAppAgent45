using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PrakharWcfService45
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWcfService" in both code and config file together.
    [ServiceContract]
    public interface IWcfService
    {
        [OperationContract]
        List<TR_Agent> AgentData();

        [OperationContract]
        [WebInvoke(UriTemplate = "UpdateAgentData",
           Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UpdateAgentData(string id);
    }
}
