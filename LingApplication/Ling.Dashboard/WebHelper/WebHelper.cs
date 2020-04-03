using Ling.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ling.Dashboard.WebHelper
{
    public class WebHelper
    {
        public static void SetOperationMessage(Controller controller, string message, ALERTTYPE type, ALERTMESSAGETYPE messageType)
        {
            Alert alert = new Alert();
            alert.AlertType = type;
            alert.Message = message;
            alert.MessageType = messageType;
            controller.TempData["OperationMessage"] = JsonConvert.SerializeObject(alert);
        }

        //public static void DeleteFile(string fileName, string fileDirectoryPath = "")
        //{
        //    string filePath = Path.Combine(ServerSettings.WEBPHYSICALUPLOADPATH, fileDirectoryPath, fileName);
        //    if (File.Exists(filePath))
        //    {
        //        File.Delete(filePath);
        //    }
        //}

    }
}
