using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventureworks.Core.Mvc
{
    public abstract class PingableControllerBase : ControllerBase
    {
        public abstract ActionResult IsLive();


        public abstract ActionResult IsReady();
    }
}
