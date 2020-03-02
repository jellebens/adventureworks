using System;
using System.Collections.Generic;
using System.Text;

namespace Adventureworks.Core.Service
{
    public static class Orders
    {
        public const string Host = "http://OrderService.Adventureworks";

        public class Vendors{
            private const string api = Host + "/api/orders";
            public const string List = api + "/list/vendors";
        }

        public class System {
            public const string Ready = Host + "/api/system/ready";
            public const string Ping = Host + "api/system/ping";
        }
    }
}
