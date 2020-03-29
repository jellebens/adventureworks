using System;
using System.Collections.Generic;
using System.Text;

namespace Adventureworks.Core.Service
{
    public static class Orders
    {
        public const string Host = "http://orders.adventureworks";

        public class Vendors{
            private const string api = Host + "/api/orders";
            public const string List = api + "/list/vendors";
        }

        public class Version {
            private const string api = Host + "/api/orders";
            public const string Current = api + "/version";
        }

        public class System {
            public const string Ready = Host + "/api/system/ready";
            public const string Ping = Host + "api/system/ping";
        }
    }
}
