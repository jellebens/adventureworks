using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public abstract class SystemControllerBase : ControllerBase
    {
        public abstract ActionResult IsLive();


        public abstract ActionResult IsReady();
    }
}
