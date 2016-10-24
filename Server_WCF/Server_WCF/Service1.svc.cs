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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ShopService : IShopService
    {
        private IShopServiceCallback Callback

        {
            get { return OperationContext.Current.GetCallbackChannel<IShopServiceCallback>(); }
        }


        public void AddNewDevice(string device)

        {
            Callback.NewDeviceArrivedNotify(device);
        }
    }
}
