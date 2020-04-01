using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Adventureworks.Core.Service;
using Adventureworks.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Adventureworks.Web.Controllers
{
    public class DocumentController : Controller
    {
        private readonly ILogger _Logger;

        public DocumentController(ILogger<DocumentController> logger)
        {
            _Logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(CreateDocument document)
        {
            _Logger.LogInformation("Uploading " + document.Upload.FileName);
            try
            {
                var documentsService = new Uri($"{Documents.Create.Upload}");

                HttpClient client = new HttpClient();
                byte[] data;
                using (Stream s = document.Upload.OpenReadStream())
                {
                    using (var br = new BinaryReader(s))
                    {
                        data = br.ReadBytes((int)s.Length);
                    }
                }

                ByteArrayContent bytes = new ByteArrayContent(data, 0, data.Length);


                MultipartFormDataContent postData = new MultipartFormDataContent();

                postData.Add(bytes, "file", document.Upload.FileName);
                var result = await client.PostAsync(documentsService, postData);

                if (!result.IsSuccessStatusCode)
                {
                    _Logger.LogWarning("Failed to call DocumentService because: " + result.ReasonPhrase);

                    return RedirectToAction("Fail");
                }
                else
                {
                    return RedirectToAction("Success");
                }
            }catch (Exception exc){
                _Logger.LogError(exc, "Error posting file");
            }

            return RedirectToAction("Fail");

        }
        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Fail()
        {
            return View();
        }
    }
}