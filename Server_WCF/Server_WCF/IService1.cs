using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
//using TEST;

namespace Server_WCF
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IShopServiceCallback))]
    public interface IShopService
    {
        [OperationContract]
        void AddNewDevice(string device);
    }

    public interface IShopServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void NewDeviceArrivedNotify(string device);
    }
}
