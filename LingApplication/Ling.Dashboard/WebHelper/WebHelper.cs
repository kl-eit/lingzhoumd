using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Ling.Common.Constants;

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
            controller.TempData["OperationMessage"] = alert;
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
