using System;
using System.Collections.Generic;
using System.Text;

namespace Adventureworks.Core.Service
{
    public class Documents
    {
        public const string Host = "http://172.17.0.8";
        
        public class Create
        {
            private const string api = Host + "/api/documents";
            public const string Upload = api + "/upload";
        }


    }
}
