using Ling.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ling.Dashboard.WebHelper
{
    public class WebHelper
    {
        private AppSettings _appSettings { get; set; }

        public static void SetOperationMessage(Controller controller, string message, ALERTTYPE type, ALERTMESSAGETYPE messageType)
        {
            Alert alert = new Alert();
            alert.AlertType = type;
            alert.Message = message;
            alert.MessageType = messageType;
            controller.TempData["OperationMessage"] = JsonConvert.SerializeObject(alert);
        }

        public static void DeleteFile(string fileName, string filePhysicalPath, string fileDirectoryPath = "")
        {
            string filePath = Path.Combine(filePhysicalPath, fileDirectoryPath, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
<<<<<<< HEAD
=======

>>>>>>> b5d7faea151cbc4f426b02daf9ed1be2956aee56
        public async static Task<HttpStatusCode> ClearWebApplicationCache(string method, string webURL)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(webURL);
                client.DefaultRequestHeaders.Accept.Clear();
<<<<<<< HEAD
                HttpResponseMessage response = await client.GetAsync("Common/" + method); // return URI of the created resource.
                return response.StatusCode;
            }
        }
=======

                HttpResponseMessage response = await client.GetAsync("Common/" + method);

                // return URI of the created resource.
                return response.StatusCode;
            }
        }

>>>>>>> b5d7faea151cbc4f426b02daf9ed1be2956aee56
    }
}
