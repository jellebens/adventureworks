using System;
using System.Collections.Generic;
using System.Text;

namespace Adventureworks.Core.Service
{
    public class Health
    {
        protected Health()
        {

        }

        public string Ready { get; private set; }

        public string Ping { get; private set; }

        public static Health Create(string host)
        {
            return new Health()
            {
                Ready = $"{host}/api/system/ready",
                Ping = $"{host}/api/system/ping"
            };
        }
    }

}
