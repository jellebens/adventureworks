using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adventureworks.Web.Models
{
    public class CreateDocument
    {
        public IFormFile Upload { get; set; }
    }
}
